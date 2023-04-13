using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using L2Logger;

namespace Core.Module.ParserEngine;

public class ParseNpcPosNew : IParse
{
    private readonly IResult _result;
    private readonly IDictionary<string, IList<IDictionary<string, int>>> _territoryCollection;
    private readonly IList<NpcMakerBeginNew> _npcMakerBeginNew;
    private readonly IList<NpcBegin> _npcBegins;
    private string _makerName;
    private NpcMakerBeginNew _npcMakerNew;

    public ParseNpcPosNew(IResult result)
    {
        _result = result;
        _territoryCollection = new Dictionary<string, IList<IDictionary<string, int>>>();
        _npcMakerBeginNew = new List<NpcMakerBeginNew>();
        _npcBegins = new List<NpcBegin>();
    }
    
    public void ParseLine(string line)
    {
        try
        {
            if (line.StartsWith("territory_begin"))
            {
                ParseTerritory(line);
            }
            if (line.StartsWith("npcmaker_begin") || line.StartsWith("npcmaker_ex_begin"))
            {
                ParseNpcMaker(line);
            }
            if (line.StartsWith("npc_begin") || line.StartsWith("npc_ex_begin"))
            {
                ParseNpcBegin(line);
            }
            if (line.StartsWith("npcmaker_end") || line.StartsWith("npcmaker_ex_end"))
            {
                _npcMakerNew.NpcBegins = new List<NpcBegin>(_npcBegins);
                _npcMakerBeginNew.Add(_npcMakerNew);
                _npcBegins.Clear();
            }
        }
        catch (Exception ex)
        {
            LoggerManager.Error(GetType().Name + ": " + ex.Message + " Line: " + line);
        }
    }

    private void ParseNpcBegin(string line)
    {
        var patternName = @"\[(.+?)\]";
        var regexName = new Regex(patternName);
        var matchName = regexName.Match(line);
        var npcName = matchName.Groups[1].Value;

        var patternPos = @"pos=((?<text>\w+)|(?<coord>\{[-\d;]+\}))";
        var match = Regex.Match(line, patternPos);
        var posText = match.Groups["text"].Value;
        var posCoord = match.Groups["coord"].Value;
        
        var tmpLocation = new Dictionary<string, int>();
        var locationAny = new List<IDictionary<string, int>>();
        if (!string.IsNullOrEmpty(posText))
        {
            var listPos = _territoryCollection[_makerName];
            foreach (var listPo in listPos)
            {
                locationAny.Add(new Dictionary<string, int>(listPo));
            }
        } 
        else if (!string.IsNullOrEmpty(posCoord))
        {
            var patternCoords = @"\{(-?\d+);(-?\d+);(-?\d+);(-?\d+)\}";
            var regexCoords = new Regex(patternCoords);
            var matchesCoords = regexCoords.Matches(line);

            foreach (Match matchCoord in matchesCoords)
            {
                var x = int.Parse(matchCoord.Groups[1].Value);
                var y = int.Parse(matchCoord.Groups[2].Value);
                var z = int.Parse(matchCoord.Groups[3].Value);
                var h = int.Parse(matchCoord.Groups[4].Value);

                tmpLocation.Add("X", x);
                tmpLocation.Add("Y", y);
                tmpLocation.Add("Z", z);
                tmpLocation.Add("H", h);
            }
        }

        var patternTotal = @"total=(\d+)";
        var regexTotal = new Regex(patternTotal);
        var matchTotal = regexTotal.Match(line);
        var total = byte.Parse(matchTotal.Groups[1].Value);

        var patternRespawn = @"respawn=([\d\w]+)";
        var regexRespawn = new Regex(patternRespawn);
        var matchRespawn = regexRespawn.Match(line);
        var respawn = matchRespawn.Groups[1].Value;
        
        _npcBegins.Add(new NpcBegin(npcName, tmpLocation, total, locationAny));
    }

    private void ParseNpcMaker(string line)
    {
        var patternName = @"\[(.+?)\]";
        var regexName = new Regex(patternName);
        var matchName = regexName.Match(line);
        _makerName = matchName.Groups[1].Value;

        var patternInitialSpawn = @"initial_spawn=(\w+)";
        var regexInitialSpawn = new Regex(patternInitialSpawn);
        var matchInitialSpawn = regexInitialSpawn.Match(line);
        var initialSpawn = matchInitialSpawn.Groups[1].Value;

        var patternSpawnTime = @"spawn_time=([\w\(\)]+)";
        var regexSpawnTime = new Regex(patternSpawnTime);
        var matchSpawnTime = regexSpawnTime.Match(line);
        var spawnTime = matchSpawnTime.Groups[1].Value;

        var patternMaxNpc = @"maximum_npc=(\d+)";
        var regexMaxNpc = new Regex(patternMaxNpc);
        var matchMaxNpc = regexMaxNpc.Match(line);
        var maxNpc = matchMaxNpc.Groups[1].Value;
        
        var patternEventName = @"event_name=\[(.+?)\]";
        var regexEventName = new Regex(patternEventName);
        var matchEventName = regexEventName.Match(line);
        var eventName = matchEventName.Groups[1].Value;

        _npcMakerNew = new NpcMakerBeginNew(_makerName)
        {
            InitialSpawn = initialSpawn,
            MaxNpc = short.Parse(maxNpc),
            SpawnTime = spawnTime,
            EventName = eventName
        };
    }

    private void ParseTerritory(string line)
    {
        string patternName = @"\[(.+?)\]";
        Regex regexName = new Regex(patternName);
        Match matchName = regexName.Match(line);
        string territoryName = matchName.Groups[1].Value;
        
        string patternCoords = @"\{(-?\d+);(-?\d+);(-?\d+);(-?\d+)\}";
        Regex regexCoords = new Regex(patternCoords);
        MatchCollection matchesCoords = regexCoords.Matches(line);
        
        IList<IDictionary<string, int>> location = new List<IDictionary<string, int>>();
        
        foreach (Match matchCoord in matchesCoords)
        {
            int x = int.Parse(matchCoord.Groups[1].Value);
            int y = int.Parse(matchCoord.Groups[2].Value);
            int z = int.Parse(matchCoord.Groups[3].Value);
            int h = int.Parse(matchCoord.Groups[4].Value);
            
            IDictionary<string, int> tmpLocation = new Dictionary<string, int>();
            tmpLocation.Add("X", x);
            tmpLocation.Add("Y", y);
            tmpLocation.Add("Z", z);
            tmpLocation.Add("H", h);
            location.Add(new Dictionary<string, int>(tmpLocation));
        }
        _territoryCollection.TryAdd(territoryName, location);
    }

    public IResult GetResult()
    {
        _result.AddItem("TerritoryCollection", _territoryCollection);
        _result.AddItem("NpcMakerCollection", _npcMakerBeginNew);
        return _result;
    }
}