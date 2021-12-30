using System;
using L2Logger;

namespace Core.Module.ParserEngine
{
    public class ParseManualPch : IParse
    {
        private readonly IResult _result;

        public ParseManualPch()
        {
            _result = new Result();
        }
        public void ParseLine(string line)
        {
            try
            {
                var split = line.RemoveBrackets().Replace("\t", "").Split("=");
                if (split.Length <= 1)
                    return;
                var key = split[0].Trim();
                var value = split[1].Trim();
                _result.AddItem(key, value);
               
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }
        
        public IResult GetResult()
        {
            return _result;
        }
    }
}