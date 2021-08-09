using System.Collections.Generic;

namespace Core.Module.ParserEngine
{
    public class ParseNpcData : IParse
    {
        private readonly IResult _result;
        private readonly IDictionary<string, object> _setStats;

        public ParseNpcData()
        {
            _result = new Result();
            _setStats = new Dictionary<string, object>();
        }
        
        public void ParseLine(string line)
        {
            var split = line.Split("\t");
            for (int i = 0; i < split.Length; i++)
            {
                var item = split[i];
                if (item.Contains("npc_begin"))
                    continue;
                if (item.Contains("npc_end"))
                {
                    _result.AddItem((string) _setStats["npc_name"], new Dictionary<string, object>(_setStats));
                    _setStats.Clear();
                    continue;
                }
                var parseItem = item.Split("=");
                switch (parseItem.Length)
                {
                    case 2 when IsDropList(parseItem):
                        _setStats.Add(parseItem[0], parseItem[1]);
                        continue;
                    case 2:
                        _setStats.Add(parseItem[0], parseItem[1].RemoveBrackets());
                        break;
                    case > 2:
                    {
                        var tmpItem = item.Split(";{");
                        var fieldName = "";
                        List<object> fieldValue = new List<object>();
                        for (int j = 0; j < tmpItem.Length; j++)
                        {
                            if (j == 0)
                            {
                                var parseItem1 = tmpItem[0].Split("=");
                                fieldName = parseItem1[0].RemoveBrackets();
                                fieldValue.Add(parseItem1[1].RemoveBrackets());
                                continue;
                            }
                            fieldValue.Add(tmpItem[j].RemoveBrackets());
                        }
                        _setStats.Add(fieldName, fieldValue);
                        break;
                    }
                    default:
                    {
                        switch (i)
                        {
                            case 1:
                                _setStats.Add("npc_type", item);
                                break;
                            case 2:
                                _setStats.Add("npc_id", item);
                                break;
                            case 3:
                                _setStats.Add("npc_name", item.Replace("[", "").Replace("]", ""));
                                break;
                        }
                        break;
                    }
                }
            }
        }
        
        private bool IsDropList(string[] parseItem)
        {
            return parseItem[0] == "corpse_make_list" ||
                   parseItem[0] == "additional_make_list" ||
                   parseItem[0] == "additional_make_multi_list";
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}