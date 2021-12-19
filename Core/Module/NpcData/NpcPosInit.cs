using System;
using System.Collections.Generic;
using System.Linq;
using Core.Module.ParserEngine;
using Core.Module.WorldData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData
{
    public class NpcPosInit : BaseParse
    {
        private readonly IParse _parse;
        private IList<NpcMakerBegin> _makerBegins;
        private readonly ObjectIdInit _objectIdInit;
        private readonly NpcDataInit _npcDataInit;

        public NpcPosInit(IServiceProvider provider) : base(provider)
        {
            _objectIdInit = provider.GetRequiredService<ObjectIdInit>();
            _npcDataInit = provider.GetRequiredService<NpcDataInit>();
            _parse = new ParseNpcPos(new Result());
            _makerBegins = new List<NpcMakerBegin>();
        }
        
        public override void Run()
        {
            try
            {
                LoggerManager.Info("NpcPos start...");
                IResult result = Parse("npcpos.txt", _parse);
                _makerBegins = result.GetResult()["NpcMakerCollection"] as List<NpcMakerBegin>;

                var testOren = _makerBegins
                    .Where(l => l.Name is "oren04_npc2119_013" or "oren04_npc2119_017" or "oren02_qm2119_00" or "oren04_npc2019_01" or "oren04_npc2119_wp1");
                foreach (var npcMakerBegin in testOren)
                {
                    var name = npcMakerBegin.Name;
                    LoggerManager.Info("Test Spawn Territory " + name);
                    foreach (var npcBegin in npcMakerBegin.NpcBegins)
                    {
                        LoggerManager.Info("Test Spawn NPC " + npcBegin.Name);
                        var x = npcBegin.Pos["X"];
                        var y = npcBegin.Pos["Y"];
                        var z = npcBegin.Pos["Z"];
                        var h = npcBegin.Pos["H"];

                        var npcTemplate = _npcDataInit.GetNpcTemplate(npcBegin.Name);
                        var npcInstance = new NpcInstance(_objectIdInit.NextObjectId(), npcTemplate);
                        npcInstance.OnSpawn(x, y, z, h);
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

        public IList<NpcMakerBegin> GetMakerList()
        {
            return _makerBegins;
        }
    }
}