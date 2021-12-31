using System.Collections.Generic;
using Helpers;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcTeleporter
{
    public class EventTeleporterI : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Dragon Valley", 122824, 110836, -3727, 1000, 0 ),
            new("Death Pass", 73024, 118485, -3720, 1000, 0 ),
            new("Breka's Stronghold", 85389, 131366, -3707, 1000, 0 ),
            new("Gorgon's Flower Garden", 113553, 134813, -3668, 1000, 0 ),
            new("Giran Territory", 69373, 155208, -3746, 1000, 0 ),
            new("Devil's Isle", 43408, 206881, -3752, 1000, 0 ),
            new("Lair of Antaras Entrance", 131131, 114597, -3720, 1000, 0 ),
            new("Lair of Antaras 1st station", 147071, 120156, -4520, 1000, 0 ),
            new("Lair of Antaras 2nd station", 151689, 112615, -5520, 1000, 0 ),
            new("Lair of Antaras Bridge position", 146425, 109898, -3424, 1000, 0 ),
            new("Lair of Antaras: Heart of warding", 154396, 121235, -3808, 1000, 0 )
        };
    }
}
