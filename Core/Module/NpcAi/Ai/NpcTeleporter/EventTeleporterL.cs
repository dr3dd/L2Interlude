using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterL : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
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
        new("Silent Valley", 170838, 55776, -5240, 1000, 0 )
    };
}