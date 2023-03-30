using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class EventTeleporterJ : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Timak Outpost", 67097, 68815, -3648, 1000, 0 ),
            new("Plains of the Lizardmen", 87252, 85514, -3103, 1000, 0 ),
            new("Skyshadow Meadow", 89963, 46639, -3568, 1000, 0 ),
            new("Sea of Spores", 64328, 26803, -3768, 1000, 0 ),
            new("Forest of Evil Spirit", 93218, 16969, -3904, 1000, 0 ),
            new("Forest of Outlaw", 91539, -12204, -2440, 1000, 0 ),
            new("Ancient Battleground", 106517, -2871, -3454, 1000, 0 )
        };
    }
}
