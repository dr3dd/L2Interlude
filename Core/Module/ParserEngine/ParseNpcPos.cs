using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using L2Logger;

namespace Core.Module.ParserEngine
{
    public class ParseNpcPos : IParse
    {
        private readonly IResult _result;
        private readonly IDictionary<string, IList<IDictionary<string, int>>> _territoryCollection;
        private readonly IList<NpcMakerBegin> _makerBegins;
        private readonly IList<NpcBegin> _npcBegins;
        private string _makerName;
        private bool _isNpcMakerBegin;
        public ParseNpcPos(IResult result)
        {
            _result = result;
            _territoryCollection = new Dictionary<string, IList<IDictionary<string, int>>>();
            _makerBegins = new List<NpcMakerBegin>();
            _npcBegins = new List<NpcBegin>();
        }
        public void ParseLine(string line)
        {
            if (line.StartsWith("territory_begin"))
            {
                ParseTerritory(line);
            }
            if (line.StartsWith("npcmaker_begin") || line.StartsWith("npcmaker_ex_begin"))
            {
                _isNpcMakerBegin = true;
                ParseNpcMaker(line);
            }
            if (line.StartsWith("npcmaker_end") || line.StartsWith("npcmaker_ex_end"))
            {
                var makerList = new NpcMakerBegin(_makerName, new List<NpcBegin>(_npcBegins));
                _makerBegins.Add(makerList);
                _npcBegins.Clear();
                _isNpcMakerBegin = false;
            }
            if (line.StartsWith("npc_begin") || line.StartsWith("npc_ex_begin"))
            {
                ParseNpcBegin(line);
            }
        }

        private void ParseNpcBegin(string line)
        {
            try
            {
                var newLine = line.Replace("npc_begin", "").Replace("npc_ex_begin", "");
                bool curlyBracketsOpen = false;
                string tmpItem = "";
                string npcName = "";
                byte npcTotal = 1;
                bool squareBracketClose = false;
                bool pos = false;
                bool total = false;
                bool equal = false;
                bool coma = false;
                bool respawn = false;
                bool curlyBracketsClose = false;
                bool anywhere = false;
                bool nickname = false;
                bool endValue = false;
                var tmpPosLoc = new List<string>();
                IDictionary<string, int> tmpLocation = new Dictionary<string, int>();
                IList<IDictionary<string, int>> tmpLocationAny = new List<IDictionary<string, int>>();

                foreach (var item in newLine)
                {
                    switch (item)
                    {
                        case '[':
                            continue;
                        case ']':
                            squareBracketClose = true;
                            continue;
                        case '=':
                            equal = true;
                            endValue = false;
                            continue;
                        case '{':
                            curlyBracketsOpen = true;
                            continue;
                        case ';':
                            coma = true;
                            break;
                        case '}':
                            curlyBracketsClose = true;
                            break;
                        case '\t':
                        case ' ':
                            endValue = true;
                            continue;
                    }

                    if (squareBracketClose && !equal)
                    {
                        npcName = tmpItem;
                        tmpItem = null;
                        squareBracketClose = false;
                    }

                    if (squareBracketClose && nickname)
                    {
                        //npcName = tmpItem;
                        tmpItem = null;
                        squareBracketClose = false;
                        nickname = false;
                    }

                    if (pos && equal)
                    {
                        if (curlyBracketsOpen && (coma || curlyBracketsClose))
                        {
                            tmpPosLoc.Add(tmpItem);
                            tmpItem = null;
                            coma = false;
                            if (curlyBracketsClose)
                            {
                                pos = false;
                                curlyBracketsClose = false;
                            }

                            continue;
                        }
                    }

                    if (total && equal && endValue)
                    {
                        npcTotal = Convert.ToByte(tmpItem);
                        equal = false;
                        tmpItem = null;
                        total = false;
                        continue;
                    }

                    tmpItem += item;
                    
                    switch (tmpItem)
                    {
                        case "pos":
                            pos = true;
                            tmpItem = null;
                            break;
                        case "total":
                            total = true;
                            tmpItem = null;
                            break;
                        case "respawn":
                            total = true;
                            tmpItem = null;
                            break;
                        case "nickname":
                            nickname = true;
                            tmpItem = null;
                            continue;
                        case "anywhere":
                            pos = false;
                            tmpItem = null;
                            anywhere = true;

                            var listPos = _territoryCollection[_makerName];
                            
                            /*
                            int rnd = Rnd.Next(listPos.Count);
                            var territoryPosition = listPos[rnd];
                            var tmpX = territoryPosition["X"];
                            var tmpY = territoryPosition["Y"];
                            var tmpZ = territoryPosition["Z"];
                            var tmpH = territoryPosition["H"];

                            var rndPosX = Rnd.Next(150);
                            var rndPosY = Rnd.Next(150);
                            var rndPosZ = Rnd.Next(150);
                            var rndPosH = Rnd.Next(150);

                            tmpLocation.TryAdd("X", tmpX + rndPosX);
                            tmpLocation.TryAdd("Y", tmpY + rndPosY);
                            tmpLocation.TryAdd("Z", tmpZ + rndPosZ);
                            tmpLocation.TryAdd("H", tmpH + rndPosH);
                            */
                            foreach (var listPo in listPos)
                            {
                                tmpLocationAny.Add(new Dictionary<string, int>(listPo));
                            }
                            continue;
                        case "ai_parameters": //TODO need develop
                            tmpItem = null;
                            continue;
                    }
                }

                if (tmpPosLoc.Any())
                {
                    tmpLocation.Add("X", Convert.ToInt32(tmpPosLoc[0]));
                    tmpLocation.Add("Y", Convert.ToInt32(tmpPosLoc[1]));
                    tmpLocation.Add("Z", Convert.ToInt32(tmpPosLoc[2]));
                    tmpLocation.Add("H", Convert.ToInt32(tmpPosLoc[3]));
                }
                _npcBegins.Add(new NpcBegin(npcName, tmpLocation, npcTotal, tmpLocationAny));
                tmpItem = null;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }

        }

        private void ParseNpcMaker(string line)
        {
            var newLine = line.Replace("npcmaker_begin", "").Replace("npcmaker_ex_begin", "");
            string tmpItem = "";
            bool squareBracketsOpen = false;
            bool equal = false;
            foreach (var item in newLine)
            {
                switch (item)
                {
                    case '[':
                        squareBracketsOpen = true;
                        continue;
                    case ']' when !equal:
                        _makerName = tmpItem;
                        tmpItem = string.Empty;
                        squareBracketsOpen = false;
                        continue;
                    case '=':
                        equal = true;
                        continue;
                    case ';':
                        continue;
                    case '\t':
                    case ' ':
                        continue;
                }
                tmpItem += item;
            }
        }

        private void ParseTerritory(string line)
        {
            var newLine = line.Replace("territory_begin", "").Replace("territory_end", "");
            string tmpItem = "";
            bool squareBracketsOpen = false;
            bool curlyBracketsOpen = false;
            string territoryName = "";
            IDictionary<string, int> tmpLocation = new Dictionary<string, int>();
            IList<IDictionary<string, int>> location = new List<IDictionary<string, int>>();
            foreach (var item in newLine)
            {
                switch (item)
                {
                    case '[':
                        squareBracketsOpen = true;
                        continue;
                    case ']':
                        territoryName = tmpItem;
                        tmpItem = string.Empty;
                        squareBracketsOpen = false;
                        continue;
                    case '{':
                        curlyBracketsOpen = true;
                        continue;
                    case '}':
                        curlyBracketsOpen = false;
                        if (tmpItem is "")
                        {
                            location.Add(new Dictionary<string, int>(tmpLocation));
                            tmpLocation.Clear();
                            continue;
                        }
                            
                        var tmp = tmpItem.Split(";");
                        tmpLocation.Add("X", Convert.ToInt32(tmp[0]));
                        tmpLocation.Add("Y", Convert.ToInt32(tmp[1]));
                        tmpLocation.Add("Z", Convert.ToInt32(tmp[2]));
                        tmpLocation.Add("H", Convert.ToInt32(tmp[3]));
                        tmpItem = string.Empty;
                        continue;
                    case ';' when !curlyBracketsOpen:
                        location.Add(new Dictionary<string, int>(tmpLocation));
                        tmpLocation.Clear();
                        continue;
                    case '\t':
                    case ' ':
                        continue;
                }
                tmpItem += item;
            }
            _territoryCollection.TryAdd(territoryName, location);
        }

        public IResult GetResult()
        {
            _result.AddItem("TerritoryCollection", _territoryCollection);
            _result.AddItem("NpcMakerCollection", _makerBegins);
            return _result;
        }
    }
}