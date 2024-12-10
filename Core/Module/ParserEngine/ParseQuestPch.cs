using L2Logger;
using System;
using System.Text.RegularExpressions;

namespace Core.Module.ParserEngine
{
    public class ParseQuestPch : IParse
    {
        private readonly IResult _result;
        private readonly Regex regex = new Regex(@"(\S+)(\s+)(\d+)");
        public ParseQuestPch()
        {
            _result = new Result();
        }
        
        public void ParseLine(string line)
        {

            MatchCollection matchCollection = regex.Matches(line.RemoveBrackets());
            if (matchCollection.Count > 0 && matchCollection[0].Groups.Count == 4)
            {
                string questName = matchCollection[0].Groups[1].Value;
                int questId = Convert.ToInt32(matchCollection[0].Groups[3].Value);
                _result.AddItem(questName, questId);
            }
            else
            {
                LoggerManager.Error($"ParseQuestPch line: {line}");
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}