using System.Collections.Generic;
using Core.Module.NpcAi.Ai.NpcType;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterA : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Obelisk of Victory", -99843, 237583, -3568, 50, 0 ),
        new("Western territory of Talking Island", -102850, 215932, -3424, 50, 0 ),
        new("Elven Ruins", 49315, 248452, -5960, 50, 0 ),
        new("Elven Ruins entrance", -113686, 235723, -3640, 50, 0 )
    };
}