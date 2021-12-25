using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                    /*
                    if (npcTemplateInit.GetStat().Type == "guard")
                    {
                        string template = "using System;" + "\n" +
                                          "using NpcService.Ai.NpcType;" + "\n" +
                                          "\n" +
                                          "namespace NpcService.Ai.NpcGuard" + "\n" +
                                          "{" + "\n" +
                                          "    public class #CLASS_NAME# : #EXTEND_NAME#" + "\n" +
                                          "    {" + "\n" +
                                          "    }" + "\n" +
                                          "}";
                        var npcName = npcTemplateInit.GetStat().Name;
                        var spl = npcName.Split("_");
                        var className = spl.Aggregate("", (current, s) => current + (char.ToUpper(s[0]) + s[1..]));

                        var newTemplate = template.Replace("#CLASS_NAME#", className);
                        newTemplate = newTemplate.Replace("#EXTEND_NAME#",
                            char.ToUpper(npcTemplateInit.GetStat().Type[0]) +
                            npcTemplateInit.GetStat().Type.Substring(1));
                        
                        using (StreamWriter sw = new StreamWriter(GetStaticData() + "/Ai/Guard/" + className + ".cs", false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(newTemplate);
                        }
                        
                    }
                    */
                    

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