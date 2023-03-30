using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class EventTeleporterM : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Ancient Battleground", 106517, -2871, -3454, 1000, 0 ),
            new("Northern Pathway of the Enchanted Valley", 104426, 33746, -3825, 1000, 0 ),
            new("Southern Pathway of the Enchanted Valley", 124904, 61992, -3973, 1000, 0 ),
            new("South of Hunter Village", 114306, 86573, -3112, 1000, 0 ),
            new("Entrance to the Forest of Mirrors", 142065, 81300, -3000, 1000, 0 ),
            new("The Front of Anghel Waterfall", 163341, 91374, -3320, 1000, 0 ),
            new("Plains of Glory", 135580, 19467, -3424, 1000, 0 ),
            new("Blazing Swamp", 159455, -12931, -2872, 1000, 0 ),
            new("Plains of Fierce Battle", 156898, 11217, -4032, 1000, 0 ),
            new("Fields of Massacre", 183543, -14974, -2768, 1000, 0 ),
            new("The Cemetery", 167047, 20304, -3328, 1000, 0 ),
            new("The Forbidden Gateway", 185319, 20218, -3264, 1000, 0 ),
            new("Forsaken Plains", 167285, 37109, -4008, 1000, 0 ),
            new("Silent Valley", 170838, 55776, -5240, 1000, 0 ),
            new("Tower of Insolence", 114649, 11115, -5100, 1000, 0 ),
            new("Tower of Insolence 3rd Floor", 110848, 16154, -2120, 1000, 0 ),
            new("Tower of Insolence 5th Floor", 118404, 15988, 832, 1000, 0 ),
            new("Tower of Insolence 7th Floor", 115064, 12181, 2960, 1000, 0 ),
            new("Tower of Insolence 10th Floor", 118525, 16455, 5984, 1000, 0 ),
            new("Tower of Insolence 13th Floor", 115384, 16820, 9000, 1000, 0 )
        };
    }
}
