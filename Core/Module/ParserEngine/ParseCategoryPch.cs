using System;
using System.Text.RegularExpressions;

namespace Core.Module.ParserEngine
{
    public class ParseCategoryPch : IParse
    {
        private readonly IResult _result;
        public ParseCategoryPch()
        {
            _result = new Result();
        }
        
        public void ParseLine(string line)
        {
            var match = Regex.Match(line, @"\[(\w+)\]\s*=\s*(\d+)");
            var key = match.Groups[1].Value;
            var value = match.Groups[2].Value;
            _result.AddItem(key, value);
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}