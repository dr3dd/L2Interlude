using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class Mint : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("The Town of Gludio", -12694, 122776, -3114, 9200, 1),
        new("Dwarven Village", 115120, -178112, -880, 23000, 0),
        new("Talking Island Village", -84141, 244623, -3729, 23000, 0),
        new("Orc Village", -45158, -112583, -236, 18000, 0),
        new("Elven Forest", 21362, 51122, -3688, 710, 0),
        new("Elven Fortress", 29294, 74968, -3776, 820, 0),
        new("Neutral Zone", -10612, 75881, -3592, 1700, 0)
    };
}