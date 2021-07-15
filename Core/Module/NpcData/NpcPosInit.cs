using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.NpcData
{
    public class NpcPosInit : BaseParse
    {
        private readonly IParse _parse;
        private IList<NpcMakerBegin> _makerBegins;

        public NpcPosInit()
        {
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