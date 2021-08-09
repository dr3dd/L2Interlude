using System;

namespace Core.Module.ParserEngine
{
    public class ParseItemPch : IParse
    {
        private readonly IResult _result;

        public ParseItemPch()
        {
            _result = new Result();
        }
        
        public void ParseLine(string line)
        {
            var split = line.RemoveBrackets().Split("=");
            _result.AddItem(split[0].Trim(), Convert.ToInt32(split[1].Trim()));
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}