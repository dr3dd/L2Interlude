using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class GatekeeperRichlin : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new ("The Town of Gludio", -12694, 122776, -3114, 7300, 1 ),
            new ("Talking Island Village", -84141, 244623, -3729, 9400, 0 ),
            new ("Elven Village", 46890, 51531, -2976, 16000, 0 ),
            new ("Dwarven Village", 115120, -178112, -880, 38000, 0 ),
            new ("Orc Village", -45186, -112459, -236, 26000, 0 ),
            new ("Langk Lizardman Dwellings", -44763, 203497, -3592, 1800, 0 ),
            new ("Windmill Hill", -75437, 168800, -3632, 550, 0 ),
            new ("Fellmere Harvesting Grounds", -63736, 101522, -3552, 1400, 0 ),
            new ("Forgotten Temple", -53001, 191425, -3568, 2000, 0 ),
            new ("Orc Barracks", -89763, 105359, -3576, 1800, 0 ),
            new ("Windy Hill", -88539, 83389, -2864, 2600, 0 ),
            new ("Abandoned Camp", -49853, 147089, -2784, 1200, 0 ),
            new ("Wastelands", -16526, 208032, -3664, 3400, 0 ),
            new ("Red Rock Ridge", -42256, 198333, -2800, 3700, 0 )	
        };
    }
}
