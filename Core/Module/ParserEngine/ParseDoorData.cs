using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Module.ParserEngine;

public class ParseDoorData : IParse
{
    private readonly IResult _result;
    private readonly IDictionary<string, object> _setStats;
    public ParseDoorData()
    {
        _result = new Result();
        _setStats = new Dictionary<string, object>();
    }
    public void ParseLine(string line)
    {
        if (!line.StartsWith("door_begin"))
        {
            return;
        }

        var pattern = @"(?<field>\w+)=(?<value>[^{}\t]+|{[^{}]+})";
        MatchCollection matches = Regex.Matches(line, pattern);
        
        Match matchName = Regex.Match(line, @"door_begin\t\[(\w+)\]");
        var doorName = matchName.Groups[1].Value;
        _setStats["door_name"] = doorName;

        foreach (Match match in matches)
        {
            var field = match.Groups["field"].Value.ToLower();
            var value = match.Groups["value"].Value;
            switch (field)
            {
                case "type":
                    _setStats["type"] = value;
                    break;
                case "editor_id":
                    _setStats["editor_id"] = value;
                    break;
                case "open_method":
                    _setStats["open_method"] = value;
                    break;
                case "height":
                    _setStats["height"] = value;
                    break;
                case "hp":
                    _setStats["hp"] = value;
                    break;
                case "physical_defence":
                    _setStats["physical_defence"] = value;
                    break;
                case "magical_defence":
                    _setStats["magical_defence"] = value;
                    break;
                case "level":
                    _setStats["level"] = value;
                    break;
                case "pos":
                    _setStats["pos"] = value.Trim('{', '}').Split(';').Select(int.Parse).ToArray();
                    break;
            }
        }
        _result.AddItem((string) _setStats["door_name"], new Dictionary<string, object>(_setStats));
        _setStats.Clear();
    }

    public IResult GetResult()
    {
        return _result;
    }
}