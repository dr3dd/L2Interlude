using Core.Module.ParserEngine;
using L2Logger;
using System;
using System.Collections.Generic;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.12.2024 9:58:33

namespace Core.Module.QuestData
{
    public class QuestPch2Init : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<int, int[]> _questItems;

        public QuestPch2Init(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseQuestPch2();
            _questItems = new Dictionary<int, int[]>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("QuestPch2 start...");
                IResult result = Parse("quest_pch2.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    _questItems.Add((int)key, (int[])value);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public int[] GetQuestItemsById(int questId)
        {
            try
            {
                return _questItems[questId];
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"QuestPch2Init: {questId} not found");
                return new int[0];
            }
        }

        public IDictionary<int, int[]> GetQuestItems()
        {
            return _questItems;
        }
    }
}
