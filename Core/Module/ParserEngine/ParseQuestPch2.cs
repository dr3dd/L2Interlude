using L2Logger;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.12.2024 10:01:36

namespace Core.Module.ParserEngine
{
    public class ParseQuestPch2 : IParse
    {
        private readonly IResult _result;
        private readonly Regex regex = new Regex(@"\d+");
        public ParseQuestPch2()
        {
            _result = new Result();
        }

        public void ParseLine(string line)
        {

            MatchCollection matchCollection = regex.Matches(line.RemoveBrackets());
            if (matchCollection.Count > 1)
            {
                try
                {
                    int questId = Convert.ToInt32(matchCollection[0].Value);
                    int countItems = Convert.ToInt32(matchCollection[1].Value);
                    if (countItems > 0)
                    {
                        int[] items = new int[countItems];
                        for (int i = 0; i < countItems; i++)
                        {
                            items[i] = Convert.ToInt32(matchCollection[i + 2].Value);
                        }
                        _result.AddItem(questId, items);
                    }
                }
                catch(Exception ex) {
                    LoggerManager.Error($"ParseQuestPch2 line: {line} - {ex.Message}");
                };
            }
            else
            {
                LoggerManager.Error($"ParseQuestPch2 line: {line}");
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
    
}
