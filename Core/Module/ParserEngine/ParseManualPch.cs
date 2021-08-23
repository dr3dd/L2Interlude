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
        
        private bool TryParseHex(string hex, out UInt32 result)
        {
            result = 0;
            try
            {
                result = Convert.ToUInt32(hex, 16);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}