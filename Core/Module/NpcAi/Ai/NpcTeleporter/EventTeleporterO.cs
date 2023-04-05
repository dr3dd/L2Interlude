using System.Collections.Generic;
using Core.Module.NpcAi.Ai.NpcType;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterO : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Entrance to the Forest of the Dead", 52107, -54328, -3158, 1000, 0 ),
        new("Western Entrace to the Swamp of Screams", 69340, -50203, -3314, 1000, 0 ),
        new("Beast Farm", 43805, -88010, -2780, 1000, 0 ),
        new("Valley of Saints", 81274, -74539, -3640, 1000, 0 ),
        new("Cursed Village", 57670, -41672, -3154, 1000, 0 )
    };
}