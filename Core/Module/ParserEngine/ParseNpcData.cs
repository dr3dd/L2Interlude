using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Module.ParserEngine
{
    public class ParseNpcData : IParse
    {
        private readonly IResult _result;
        private readonly IDictionary<string, object> _setStats;
        private readonly string _basePattern = @"npc_begin\s+(\w+)\s+(\d+)\s+\[(\w+)\]\s+(.*)\s+npc_end";
        private readonly string _subPattern = @"\s*(\w+)=({.*?}\s|[^;\s]*)"; //split expression by key=value1|value2

        public ParseNpcData()
        {
            _result = new Result();
            _setStats = new Dictionary<string, object>();
        }

        /// <summary>
        /// Parse Line of string
        /// </summary>
        /// <param name="line"></param>
        public void ParseLine(string line)
        {
            Match matchesBase = Regex.Match(line, _basePattern);
            _setStats["npc_type"] = matchesBase.Groups[1].Value;
            _setStats["npc_id"] = matchesBase.Groups[2].Value;
            _setStats["npc_name"] = matchesBase.Groups[3].Value;
            
            if (int.Parse((string) _setStats["npc_id"]) == 29066)
            {
                var d = 1;
            }
            
            MatchCollection matchesSub = Regex.Matches(line, _subPattern);
            foreach (Match match in matchesSub)
            {
                _setStats[match.Groups[1].Value] = match.Groups[2].Value;
            }
            _result.AddItem((string) _setStats["npc_name"], new Dictionary<string, object>(_setStats));
            _setStats.Clear();
        }

        /// <summary>
        /// Return Result
        /// </summary>
        /// <returns></returns>
        public IResult GetResult()
        {
            return _result;
        }
    }
}