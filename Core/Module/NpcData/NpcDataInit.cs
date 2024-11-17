using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Module.CharacterData.Template;
using Core.Module.ParserEngine;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData
{
    public class NpcDataInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, NpcTemplateInit> _npcDataCollection;
        private readonly PcParameterInit _parameterInit;

        public NpcDataInit(IServiceProvider provider) : base(provider)
        {
            _npcDataCollection = new Dictionary<string, NpcTemplateInit>();
            _parameterInit = provider.GetRequiredService<PcParameterInit>();
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
                    var npcTemplateInit = new NpcTemplateInit(value as IDictionary<string, object>, _parameterInit);
                    _npcDataCollection.Add(key.ToString(), npcTemplateInit);
                }
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

        public NpcTemplateInit GetNpcTemplate(int npcId)
        {
            return _npcDataCollection.Values.Where(n => n.GetStat().Id == npcId).FirstOrDefault();
        }

        public IEnumerable<NpcTemplateInit> GetAllNpcTemplate()
        {
            return _npcDataCollection.Values.OfType<NpcTemplateInit>().OrderBy(d => d.GetStat().Id).ToList(); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsNpcTemplateExist(string name)
        {
            return _npcDataCollection.ContainsKey(name);
        }
    }
}