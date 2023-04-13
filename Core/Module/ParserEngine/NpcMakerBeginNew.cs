using System.Collections.Generic;

namespace Core.Module.ParserEngine;

public class NpcMakerBeginNew
{
    public string Name { get; }
    public IList<NpcBegin> NpcBegins { get; set; }
    public string InitialSpawn { get; set; }
    public string SpawnTime { get; set; }
    public short MaxNpc { get; set; }
    public string EventName { get; set; }

    public NpcMakerBeginNew(string name)
    {
        Name = name;
    }
}