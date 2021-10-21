using System;
using System.Collections.Generic;
using System.Globalization;
using L2Logger;

namespace Core.Module.ParserEngine
{
    public class ParseAreaData : IParse
    {
        private readonly IResult _result;
        private readonly IDictionary<string, IDictionary<string, double>> _waterRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _motherTreeRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _peaceZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _battleZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _damageZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _swampZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _poisonZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _instantSkillZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _noRestartZoneRange;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _ssqZoneRange;
        
        public ParseAreaData()
        {
            _result = new Result();
            _waterRange = new Dictionary<string, IDictionary<string, double>>();
            _motherTreeRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _peaceZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _battleZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _damageZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _swampZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _poisonZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _instantSkillZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _noRestartZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
            _ssqZoneRange = new Dictionary<string, IList<IDictionary<string, int>>>();
        }
        
        public void ParseLine(string line)
        {
            try
            {
                if (line.StartsWith("default_setting_begin") && line.EndsWith("default_setting_end"))
                {
                    ParseDefaultSetting(line);
                }

                if (!line.StartsWith("area_begin") || !line.EndsWith("area_end")) return;
                var data = ParseArea(line);
                switch (data["type"])
                {
                    case "water":
                        var waterRange= ParseWaterRange(data["water_range"]);
                        _waterRange.Add(data["name"], waterRange);
                        _result.AddItem("water", _waterRange);
                        break;
                    case "mother_tree":
                        var motherThreeRange= ParseBaseRange(data["range"]);
                        _motherTreeRange.Add(data["name"], motherThreeRange);
                        _result.AddItem("mother_tree", _motherTreeRange);
                        break;
                    case "peace_zone":
                        var peaceZoneRange= ParseBaseRange(data["range"]);
                        _peaceZoneRange.Add(data["name"], peaceZoneRange);
                        _result.AddItem("peace_zone", _peaceZoneRange);
                        break;
                    case "battle_zone":
                        var battleZoneRange= ParseBaseRange(data["range"]);
                        _battleZoneRange.Add(data["name"], battleZoneRange);
                        _result.AddItem("battle_zone", _battleZoneRange);
                        break;
                    case "damage":
                        var damageZoneRange= ParseBaseRange(data["range"]);
                        _damageZoneRange.Add(data["name"], damageZoneRange);
                        _result.AddItem("damage", _damageZoneRange);
                        break;
                    case "swamp":
                        var swampZoneRange= ParseBaseRange(data["range"]);
                        _swampZoneRange.Add(data["name"], swampZoneRange);
                        _result.AddItem("swamp", _swampZoneRange);
                        break;
                    case "poison":
                        var poisonZoneRange= ParseBaseRange(data["range"]);
                        _poisonZoneRange.Add(data["name"], poisonZoneRange);
                        _result.AddItem("poison", _poisonZoneRange);
                        break;
                    case "instant_skill":
                        var instantSkillZoneRange= ParseBaseRange(data["range"]);
                        _instantSkillZoneRange.Add(data["name"], instantSkillZoneRange);
                        _result.AddItem("instant_skill", _instantSkillZoneRange);
                        break;
                    case "no_restart":
                        var noRestartZoneRange= ParseBaseRange(data["range"]);
                        _noRestartZoneRange.Add(data["name"], noRestartZoneRange);
                        _result.AddItem("no_restart", _noRestartZoneRange);
                        break;
                    case "ssq_zone":
                        var ssqZoneRange= ParseBaseRange(data["range"]);
                        _ssqZoneRange.Add(data["name"], ssqZoneRange);
                        _result.AddItem("ssq_zone", _ssqZoneRange);
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }

        private IList<IDictionary<string, int>> ParseBaseRange(string range)
        {
            var split = range.Split(";");
            var cnt = split.Length;
            IList<IDictionary<string, int>> list = new List<IDictionary<string, int>>();
            for (int i = 0; i<cnt; i+=4)
            {
                if (i > cnt)
                    break;
                int x = Convert.ToInt32(split[i + 0]);
                int y = Convert.ToInt32(split[i + 1]);
                int zMin = Convert.ToInt32(split[i + 2]);
                int zMax = Convert.ToInt32(split[i + 3]);

                IDictionary<string, int> rangeData = new Dictionary<string, int>();
                rangeData.Add("X", x);
                rangeData.Add("Y", y);
                rangeData.Add("MinZ", zMin);
                rangeData.Add("MaxZ", zMax);
                list.Add(rangeData);
            }
            return list;
        }

        private IDictionary<string, string> ParseArea(string line)
        {
            IDictionary<string, string> areaData = new Dictionary<string, string>();
            try
            {
                var newLine = line.Replace("area_begin", "")
                    .Replace("area_end", "")
                    .Replace("\t", " ");
                var tmpItem = "";
                IList<string> fieldList = new List<string>();
                IList<string> valueList = new List<string>();
                bool isFoundField = false;
                bool brackedOpen = false;
                foreach (var item in newLine)
                {
                    switch (item)
                    {
                        case '=':
                            fieldList.Add(tmpItem);
                            tmpItem = string.Empty;
                            isFoundField = true;
                            continue;
                        case ' ' when isFoundField && !brackedOpen:
                            if (tmpItem is "")
                            {
                                continue;
                            }
                            valueList.Add(tmpItem);
                            tmpItem = string.Empty;
                            isFoundField = false;
                            continue;
                        case '{':
                            brackedOpen = true;
                            continue;
                        case '}':
                            brackedOpen = false;
                            continue;
                    }

                    if ((item is ' ') && !brackedOpen)
                        continue;
                    tmpItem += item;
                }
                for (int i = 0; i < fieldList.Count; i++)
                {
                    areaData.Add(fieldList[i], valueList[i]);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
            return areaData;
        }
        
        private IDictionary<string, double> ParseWaterRange(string waterRange)
        {
            var tmpItem = "";
            IList<string> fieldList = new List<string>();
            IList<double> valueList = new List<double>();
            IDictionary<string, double> waterRangeData = new Dictionary<string, double>();
            
            bool isBracketsOpened = false;
            foreach (var item in waterRange)
            {
                switch (item)
                {
                    case ':':
                        fieldList.Add(tmpItem);
                        tmpItem = string.Empty;
                        continue;
                    case ' ' when isBracketsOpened:
                        valueList.Add(Convert.ToDouble(tmpItem, CultureInfo.InvariantCulture.NumberFormat));
                        tmpItem = string.Empty;
                        continue;
                    case '(':
                        isBracketsOpened = true;
                        continue;
                    case ')':
                        valueList.Add(Convert.ToDouble(tmpItem, CultureInfo.InvariantCulture.NumberFormat));
                        tmpItem = string.Empty;
                        isBracketsOpened = false;
                        continue;
                }
                if (item is ' ')
                    continue;
                tmpItem += item;
            }
            waterRangeData.Add("MinX", valueList[0]);
            waterRangeData.Add("MinY", valueList[1]);
            waterRangeData.Add("MinZ", valueList[2]);
            
            waterRangeData.Add("MaxX", valueList[3]);
            waterRangeData.Add("MaxY", valueList[4]);
            waterRangeData.Add("MaxZ", valueList[5]);

            return waterRangeData;
        }
        
        private void ParseDefaultSetting(string line)
        {
            try
            {
                var newLine = line.Replace("default_setting_begin", "").Replace("default_setting_end", "");
                var tmpItem = "";
                IList<string> fieldList = new List<string>();
                IList<string> valueList = new List<string>();
                foreach (var item in newLine)
                {
                    switch (item)
                    {
                        case '=':
                            fieldList.Add(tmpItem);
                            tmpItem = string.Empty;
                            continue;
                        case ' ' when tmpItem.Length > 0:
                            valueList.Add(tmpItem);
                            tmpItem = string.Empty;
                            continue;
                    }

                    if (item is ' ')
                        continue;
                    tmpItem += item;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}