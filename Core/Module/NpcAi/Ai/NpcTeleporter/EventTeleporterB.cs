using System.Collections.Generic;
using Core.Module.NpcAi.Ai.NpcType;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterB : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Elven Forest", 21362, 51122, -3688, 50, 0 ),
        new("Neutral Zone", -10612, 75881, -3592, 50, 0 ),
        new("Elven Fortress", 29074, 74958, -3776, 50, 0 )
    };
}