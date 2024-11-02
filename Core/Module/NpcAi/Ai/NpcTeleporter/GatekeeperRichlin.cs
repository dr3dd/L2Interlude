using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperRichlin : Teleporter
{
    public override int PrimeHours => 1;
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("The Town of Gludio", -12694, 122776, -3114, 7300, 1 ),
        new("Talking Island Village", -84141, 244623, -3729, 9400, 0 ),
        new("Elven Village", 46890, 51531, -2976, 16000, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 38000, 0 ),
        new("Orc Village", -45186, -112459, -236, 26000, 0 ),
        new("Langk Lizardman Dwellings", -44763, 203497, -3592, 1800, 0 ),
        new("Windmill Hill", -75437, 168800, -3632, 550, 0 ),
        new("Fellmere Harvesting Grounds", -63736, 101522, -3552, 1400, 0 ),
        new("Forgotten Temple", -53001, 191425, -3568, 2000, 0 ),
        new("Orc Barracks", -89763, 105359, -3576, 1800, 0 ),
        new("Windy Hill", -88539, 83389, -2864, 2600, 0 ),
        new("Abandoned Camp", -49853, 147089, -2784, 1200, 0 ),
        new("Wastelands", -16526, 208032, -3664, 3400, 0 ),
        new("Red Rock Ridge", -42256, 198333, -2800, 3700, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("The Town of Gludio", -12694, 122776, -3114, 3650, 1 ),
        new("Talking Island Village", -84141, 244623, -3729, 4700, 0 ),
        new("Elven Village", 46890, 51531, -2976, 8000, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 19000, 0 ),
        new("Orc Village", -45186, -112459, -236, 13000, 0 ),
        new("Langk Lizardman Dwellings", -44763, 203497, -3592, 900, 0 ),
        new("Windmill Hill", -75437, 168800, -3632, 275, 0 ),
        new("Fellmere Harvesting Grounds", -63736, 101522, -3552, 700, 0 ),
        new("Forgotten Temple", -53001, 191425, -3568, 1000, 0 ),
        new("Orc Barracks", -89763, 105359, -3576, 900, 0 ),
        new("Windy Hill", -88539, 83389, -2864, 1300, 0 ),
        new("Abandoned Camp", -49853, 147089, -2784, 600, 0 ),
        new("Wastelands", -16526, 208032, -3664, 1700, 0 ),
        new("Red Rock Ridge", -42256, 198333, -2800, 1850, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1, 0 ),
        new("The Elven Village", 46951, 51550, -2976, 1, 0 ),
        new("The Dark Elven Village", 9709, 15566, -4500, 1, 0 ),
        new("Orc Village", -45158, -112583, -236, 1, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 1, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3114, 1, 0 ),
        new("Dion Castle Town", 15744, 142928, -2704, 1, 0 ),
        new("Heine", 111455, 219400, -3546, 1, 0 ),
        new("Giran Castle Town", 83336, 147972, -3404, 1, 0 ),
        new("The Town of Oren", 83011, 53207, -1470, 1, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Center of the Forgotten Temple", -54026, 179504, -4650, 1, 0 ),
        new("Wastelands,  Western Region", -47506, 179572, -3632, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemSSQ => new List<TeleportList>
    {
        new("Necropolis of Sacrifice", -41184, 206752, -3357, 1, 0 ),
        new("Heretics Catacomb", 39232, 143568, -3651, 1, 0 ),
        new("Pilgrims Necropolis", 45600, 126944, -3686, 1, 0 ),
        new("Catacomb of the Branded", 43200, 170688, -3251, 1, 0 ),
        new("Worshipers Necropolis", 107514, 174329, -3704, 1, 0 ),
        new("Catacomb of the Apostate", 74672, 78032, -3398, 1, 0 ),
        new("Patriots Necropolis", -25472, 77728, -3446, 1, 0 ),
        new("Catacomb of the Witch", 136672, 79328, -3702, 1, 0 ),
        new("Ascetics Necropolis", -56064, 78720, -3011, 1, 0 ),
        new("Martyr's Necropolis", 114496, 132416, -3101, 1, 0 ),
        new("Disciples Necropolis", 168560, -17968, -3174, 1, 0 ),
        new("Saints Necropolis", 79296, 209584, -3709, 1, 0 ),
        new("Catacomb of Dark Omens", -22480, 13872, -3174, 1, 0 ),
        new("Catacomb of the Forbidden Path", 110912, 84912, -4816, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1000, 0 ),
        new("The Elven Village", 46951, 51550, -2976, 1000, 0 ),
        new("The Dark Elven Village", 9709, 15566, -4500, 1000, 0 ),
        new("Orc Village", -45158, -112583, -236, 1000, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 1000, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3114, 1000, 0 ),
        new("Dion Castle Town", 15744, 142928, -2704, 1000, 0 ),
        new("Heine", 111455, 219400, -3546, 1000, 0 ),
        new("Giran Castle Town", 83336, 147972, -3404, 1000, 0 ),
        new("The Town of Oren", 83011, 53207, -1470, 1000, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1000, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1000, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1000, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1000, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1000, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Center of the Forgotten Temple", -54026, 179504, -4650, 1000, 0 ),
        new("Wastelands,  Western Region", -47506, 179572, -3632, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemSSQ => new List<TeleportList>
    {
        new("Necropolis of Sacrifice", -41184, 206752, -3357, 1000, 0 ),
        new("Heretics Catacomb", 39232, 143568, -3651, 1000, 0 ),
        new("Pilgrims Necropolis", 45600, 126944, -3686, 1000, 0 ),
        new("Catacomb of the Branded", 43200, 170688, -3251, 1000, 0 ),
        new("Worshipers Necropolis", 107514, 174329, -3704, 1000, 0 ),
        new("Catacomb of the Apostate", 74672, 78032, -3398, 1000, 0 ),
        new("Patriots Necropolis", -25472, 77728, -3446, 1000, 0 ),
        new("Catacomb of the Witch", 136672, 79328, -3702, 1000, 0 ),
        new("Ascetics Necropolis", -56064, 78720, -3011, 1000, 0 ),
        new("Martyr's Necropolis", 114496, 132416, -3101, 1000, 0 ),
        new("Disciples Necropolis", 168560, -17968, -3174, 1000, 0 ),
        new("Saints Necropolis", 79296, 209584, -3709, 1000, 0 ),
        new("Catacomb of Dark Omens", -22480, 13872, -3174, 1000, 0 ),
        new("Catacomb of the Forbidden Path", 110912, 84912, -4816, 1000, 0 )
    };


}