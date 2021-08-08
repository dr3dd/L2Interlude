using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.AreaData
{
    public sealed class AreaDataInit : BaseParse
    {
        private readonly IDictionary<string, string> _defaultSettings;
        private readonly IDictionary<string, Type> _areaCollection;
        private readonly IList<BaseArea> _areas;
        
        public AreaDataInit(IServiceProvider provider) : base(provider)
        {
            _defaultSettings = new Dictionary<string, string>();
            _areaCollection = new Dictionary<string, Type> {{"water", typeof(WaterArea)}};
            _areas = new List<BaseArea>();
            Run();
        }

        public override void Run()
        {
            LoggerManager.Info("AreaData start...");
            using StreamReader sr = new StreamReader(GetStaticData() + "/" + "areadata.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                ParseFile(line);
            }
            LoggerManager.Info("Loaded Areas: " + _areas.Count);
        }

        private void ParseFile(string line)
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
                        var mapNo = data["map_no"].Split(";");
                        var type = _areaCollection["water"];
                        var waterRange= ParseWaterRange(data["water_range"]);
                        var waterZone = (BaseArea) Activator.CreateInstance(type, data["name"],
                            Convert.ToByte(mapNo[0]), Convert.ToByte(mapNo[1]), waterRange);
                        _areas.Add(waterZone);
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
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
    }
}