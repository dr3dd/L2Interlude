using System.Collections.Generic;
using Core.Module.NpcAi.Ai.NpcType;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterE : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("North Mining Region", 124054, -200170, -3704, 50, 0 ),
        new("Western Mining Region", 136910, -205082, -3664, 50, 0 ),
        new("Eastern Mining Region", 169008, -208272, -3506, 50, 0 ),
        new("Abandoned Coal Mines", 139714, -177456, -1536, 50, 0 ),
        new("Mithril Mines", 171946, -173352, 3440, 50, 0 )
    };
}