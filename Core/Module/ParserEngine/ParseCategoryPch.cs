using System;

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
            var newLine = line.Replace("[", "")
                .Replace("]", "")
                .Replace(" ", " ").Replace("\t", "").Split("=");
            _result.AddItem(newLine[0], Convert.ToByte(newLine[1]));
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}