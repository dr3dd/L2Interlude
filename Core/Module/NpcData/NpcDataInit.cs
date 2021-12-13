using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.NpcData
{
    public class NpcDataInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, NpcTemplateInit> _npcDataCollection;
        private readonly IDictionary<string, string> _dictionary;

        public NpcDataInit(IServiceProvider provider) : base(provider)
        {
            _npcDataCollection = new Dictionary<string, NpcTemplateInit>();
            _dictionary = new Dictionary<string, string>();
            _parse = new ParseNpcData();
        }
        
        public override void Run()
        {
            try
            {
                LoggerManager.Info("NpcData start...");
                IResult result = Parse("npcdata.txt", _parse);

                foreach (var (key, value) in result.GetResult())
                {
                    var npcTemplateInit = new NpcTemplateInit(value as IDictionary<string, object>);
                    _npcDataCollection.Add(key.ToString(), npcTemplateInit);

                    _dictionary.TryAdd(npcTemplateInit.GetStat().Type, "1");
                }

                var d = 1;
            } 
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
            LoggerManager.Info("Loaded NpcData: " + _npcDataCollection.Count);
        }

        public NpcTemplateInit GetNpcTemplate(string name)
        {
            return _npcDataCollection[name];
        }
    }
}