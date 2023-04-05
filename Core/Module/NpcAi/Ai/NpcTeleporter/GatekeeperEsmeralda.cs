using System.Collections.Generic;
using Core.Module.NpcAi.Ai.NpcType;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperEsmeralda : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Aden", 146705, 25840, -2000, 5900, 5 ),
        new("Town of Oren", 82956, 53162, -1470, 4100, 4 ),
        new("Hardin's Private Academy", 105846, 109762, -3170, 3400, 3 ),
        new("Enchanted Valley, Southern Region", 124904, 61992, -3952, 1300, 0 ),
        new("Enchanted Valley, Northern Region", 104426, 33746, -3800, 3600, 0 ),
        new("Forest of Mirrors", 142065, 81300, -3000, 2000, 0 )
    };
}