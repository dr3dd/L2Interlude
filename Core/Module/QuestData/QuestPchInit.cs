using Core.Module.ParserEngine;
using L2Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 21.11.2024 11:06:05

namespace Core.Module.QuestData
{
    public class QuestPchInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, int> _quests;

        public QuestPchInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseQuestPch();
            _quests = new Dictionary<string, int>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("QuestPch start...");
                IResult result = Parse("quest_pch.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    _quests.Add(key.ToString(), (int)value);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public int GetQuestIdByName(string name)
        {
            try
            {
                return _quests[name];
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"QuestPchInit: {name} not found");
                return 0;
            }
        }

        public IDictionary<string, int> GetQuests()
        {
            return _quests;
        }
    }
}
