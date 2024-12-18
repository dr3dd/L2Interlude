using Core.Module.CharacterData;
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
        private bool _isInitialStartPoint;
        private bool _isStartPoint;
        private string[] _baseClassNames;
        private List<Location> _points;
        private readonly IDictionary<string, List<string>> _initialEquipment;
        private readonly IDictionary<string, List<Location>> _initialStartPoint;

        private readonly IResult _result;

        public ParseSetting(IResult result)
        {
            _result = result;
            _initialEquipment = new Dictionary<string, List<string>>();
            _initialStartPoint = new Dictionary<string, List<Location>>();
            _baseClassNames = new string[0];
            _points = new List<Location>();
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
                case "initial_start_point_begin":
                    _isInitialStartPoint = true;
                    return;
                case "point_begin":
                    _isStartPoint = true;
                    
                    return;
                case "point_end":
                    _isStartPoint = false;
                    if (_baseClassNames.Length > 0 && _points.Count > 0)
                    {
                        foreach (var _baseClassName in _baseClassNames)
                        {
                            _initialStartPoint.Add(_baseClassName, new List<Location>(_points));
                        }
                        _baseClassNames = new string[0];
                        _points.Clear();
                    }
                    return;
                case "initial_start_point_end":
                    _isInitialStartPoint = false;
                    if (_initialStartPoint.Count > 0)
                    {
                        _result.AddItem("initial_start_point", _initialStartPoint);
                    }
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
                if (_isInitialStartPoint && _isStartPoint)
                {
                    MatchCollection pointsCollection = new Regex(@"(\w+)=(\S+)").Matches(line.RemoveBrackets().RemoveSpaces());
                    if (pointsCollection.Count > 0)
                    {
                        var key = pointsCollection[0].Groups[1].Value;
                        var value = pointsCollection[0].Groups[2].Value;
                        if (key == "point")
                        {
                            MatchCollection pointCollection = new Regex(@"(-?\d+);(-?\d+);(-?\d+)").Matches(value);
                            if (pointCollection.Count > 0)
                            {
                                var x = Convert.ToInt32(pointCollection[0].Groups[1].Value);
                                var y = Convert.ToInt32(pointCollection[0].Groups[2].Value);
                                var z = Convert.ToInt32(pointCollection[0].Groups[3].Value);
                                _points.Add(new Location(x, y, z));
                            }
                        }

                        if (key == "class")
                        {
                            _baseClassNames = value.Split(";");
                        }
                    }
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
