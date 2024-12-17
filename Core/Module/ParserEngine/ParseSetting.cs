using L2Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 17.12.2024 1:29:11

namespace Core.Module.ParserEngine
{
    public class ParseSetting : IParse
    {
        private bool _isInitialEquipment;
        private readonly IDictionary<string, List<string>> _initialEquipment;

        private readonly IResult _result;

        public ParseSetting(IResult result)
        {
            _result = result;
            _initialEquipment = new Dictionary<string, List<string>>();
        }

        public void ParseLine(string line)
        {
            if (line.StartsWith("//") || line.Equals(string.Empty))
            {
                return;
            }

            switch (line)
            {
                case "initial_equipment_begin":
                    _isInitialEquipment = true;
                    return;
                case "initial_equipment_end":
                    _isInitialEquipment = false;
                    _result.AddItem("initial_equipment", _initialEquipment);
                    return;
            }
            PrepareData(line);
        }

        private void PrepareData(string line)
        {
            try { 
                if (_isInitialEquipment)
                {
                    MatchCollection matchCollection = new Regex(@"(\w+)=(\w\S+)").Matches(line.RemoveBrackets());
                    var base_class = matchCollection[0].Groups[1].Value;
                    var equip = matchCollection[0].Groups[2].Value.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    _initialEquipment.Add(base_class, equip.ToList());
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"Setting PrepareData line: {line} - {ex.Message}");
            };
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}
