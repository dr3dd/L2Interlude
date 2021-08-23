using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.ManualData
{
    public class ManualPchInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<string, int> _manualPch; 
        public ManualPchInit(IServiceProvider provider) : base(provider)
        {
            _parse = new ParseManualPch();
            _manualPch = new Dictionary<string, int>();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("ManualPch start...");
                IResult result = Parse("manual_pch.txt", _parse);
                foreach (var (key, value) in result.GetResult())
                {
                    if (TryParseHex(value.ToString(), out var output))
                    {
                        _manualPch.Add(key.ToString(), output);
                        continue;
                    }
                    _manualPch.Add(key.ToString(), Convert.ToInt32(value.ToString()));
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }
        
        private bool TryParseHex(string hex, out Int32 result)
        {
            result = 0;
            try
            {
                result = Convert.ToInt32(hex, 16);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        
        public int GetManualIdByName(string name)
        {
            return _manualPch[name];
        }
    }
}