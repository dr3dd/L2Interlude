using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperVerona : TeleporterMultiList
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Oren", 82956, 53162, -1470, 6200, 4 ),
        new("Hunters Village", 117110, 76883, -2670, 6800, 0 ),
        new("Town of Aden", 146705, 25840, -2000, 3700, 5 )
    };
}