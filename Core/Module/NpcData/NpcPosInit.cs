using System;
using System.Collections.Generic;
using System.Linq;
using Core.Module.ParserEngine;
using Core.Module.WorldData;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData
{
    public class NpcPosInit : BaseParse
    {
        private readonly IParse _parse;
        private IList<NpcMakerBeginNew> _makerBegins;
        private readonly ObjectIdInit _objectIdInit;
        private readonly NpcDataInit _npcDataInit;
        private readonly IServiceProvider _serviceProvider;

        public NpcPosInit(IServiceProvider provider) : base(provider)
        {
            _serviceProvider = provider;
            _objectIdInit = provider.GetRequiredService<ObjectIdInit>();
            _npcDataInit = provider.GetRequiredService<NpcDataInit>();
            _parse = new ParseNpcPosNew(new Result());
            _makerBegins = new List<NpcMakerBeginNew>();
        }
        
        public override void Run()
        {
            try
            {
                LoggerManager.Info("NpcPos start...");
                IResult result = Parse("npcpos.txt", _parse);
                _makerBegins = result.GetResult()["NpcMakerCollection"] as List<NpcMakerBeginNew>;

                /*
                var testOren = _makerBegins
                    .Where(l => l.Name is "oren04_npc2119_013" or "oren04_npc2119_017" or "oren02_qm2119_00" or "oren04_npc2019_01" or "oren04_npc2119_wp1");
                    */
                foreach (var npcMakerBegin in _makerBegins)
                {
                    var name = npcMakerBegin.Name;
                    if (npcMakerBegin.EventName != string.Empty)
                    {
                        //skip events
                        continue;
                    }
                    LoggerManager.Info("Test Spawn Territory " + name);
                    foreach (var npcBegin in npcMakerBegin.NpcBegins)
                    {
                        //LoggerManager.Info("Test Spawn NPC " + npcBegin.Name);
                        
                        var npcTemplate = _npcDataInit.GetNpcTemplate(npcBegin.Name);
                        if (npcBegin.Pos.Count > 0)
                        {
                            var x = npcBegin.Pos["X"];
                            var y = npcBegin.Pos["Y"];
                            var z = npcBegin.Pos["Z"];
                            var h = npcBegin.Pos["H"];
                            
                            if (npcTemplate.GetStat().Type == "citizen" || npcTemplate.GetStat().Type == "teleporter" ||
                                npcTemplate.GetStat().Type == "guard" || npcTemplate.GetStat().Type == "guild_coach")
                            {
                                try
                                {
                                    var npcInstance = new NpcInstance(_objectIdInit.NextObjectId(), npcTemplate,
                                        _serviceProvider);
                                    npcInstance.OnSpawn(x, y, z, h);
                                }
                                catch (Exception ex)
                                {
                                    LoggerManager.Error(ex.Message);
                                }
                            }
                        }

                        if (npcBegin.PosAny.Count > 0)
                        {
                            if (npcBegin.Name == "tutorial_gremlin")
                            {

                                var maxX = npcBegin.PosAny.Aggregate((x1, x2) => x1["X"] > x2["X"] ? x1 : x2)["X"];
                                var minX = npcBegin.PosAny.Aggregate((x1, x2) => x1["X"] < x2["X"] ? x1 : x2)["X"];

                                var maxY = npcBegin.PosAny.Aggregate((y1, y2) => y1["Y"] > y2["Y"] ? y1 : y2)["Y"];
                                var minY = npcBegin.PosAny.Aggregate((y1, y2) => y1["Y"] < y2["Y"] ? y1 : y2)["Y"];
                                
                                var radiusX = 50;
                                var radiusY = 50;

                                var z = npcBegin.PosAny[0]["H"];

                                Polygon.Point[] polygon = new Polygon.Point[npcBegin.PosAny.Count];

                                for (var index = 0; index < npcBegin.PosAny.Count; index++)
                                {
                                    var dictionary = npcBegin.PosAny[index];
                                    polygon[index] = new Polygon.Point(dictionary["X"], dictionary["Y"]);
                                }

                                var n = polygon.Length;
                                var lst = new List<Dictionary<string, int>>();
                                for (var y = minY; y<=maxY; y++)
                                {
                                    for (var x = minX; x<=maxX; x++)
                                    {
                                        var p = new Polygon.Point(x, y);
                                        if (Polygon.isInside(polygon, n, p))
                                        {
                                            lst.Add(new Dictionary<string, int>
                                                {
                                                    {"x", x},
                                                    {"y", y},
                                                    {"z", z}
                                                }
                                            );
                                            //Console.WriteLine("X: {0}, Y:{1}", x, y);
                                        }
                                        x += radiusX;
                                    }
                                    y += radiusY;
                                }
                                
                                //npcBegin.Total
                                foreach (var item in lst.OrderBy(x => Rnd.Next()).Take(npcBegin.Total))
                                {
                                    var npcInstance = new NpcInstance(_objectIdInit.NextObjectId(), npcTemplate, _serviceProvider);
                                    npcInstance.CharacterStatus().CurrentHp = npcTemplate.GetStat().OrgHp;
                                    npcInstance.SpawnX = item["x"];
                                    npcInstance.SpawnY = item["y"];
                                    npcInstance.SpawnZ = item["z"];
                                    npcInstance.OnSpawn(item["x"], item["y"], item["z"], Rnd.Next(61794));
                                    //InitNpcInNpcService(npcInstance);
                                }
                            }
                        }
                    }
                }
                /*
                List<NpcMakerBegin> res = result.GetResult()["NpcMakerCollection"] as List<NpcMakerBegin>;
                var i = res.Where(l => l.Name == "raidboss_2017_uruka");
                */
            } 
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public IList<NpcMakerBeginNew> GetMakerList()
        {
            return _makerBegins;
        }
    }
}