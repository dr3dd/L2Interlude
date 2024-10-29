using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class Valentina : Teleporter
{
    public override int PrimeHours => 1;
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Aden", 146783, 25808, -2000, 6900, 5),
        new("The Town of Giran", 83314, 148012, -3400, 9400, 3),
        new("Rune Township", 43835, -47749, -792, 10000, 8),
        new("Town of Goddard", 148024, -55281, -2728, 37000, 7),
        new("Heine", 111455, 219400, -3546, 50000, 6),
        new("The Town of Dion", 15671, 142994, -2704, 33000, 2),
        new("Town of Schuttgart", 87018, -143379, -1288, 59000, 9),
        new("The Town of Gludio", -12694, 122776, -3114, 35000, 1),
        new("Ivory Tower", 85391, 16228, -3640, 3700, 4),
        new("Hunters Village", 117110, 76883, -2670, 4100, 0),
        new("Hardin's Private Academy", 105884, 109744, -3170, 6100, 3),
        new("Skyshadow Meadow", 89914, 46276, -3616, 780, 0),
        new("Plains of the Lizardmen", 87252, 85514, -3056, 1900, 0),
        new("Outlaw Forest", 91539, -12204, -2440, 5200, 0),
        new("Sea of Spores", 64328, 26803, -3768, 2500, 0),

    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Town of Aden", 146783, 25808, -2000, 3450, 5 ),
        new("The Town of Giran", 83314, 148012, -3400, 4700, 3 ),
        new("Rune Township", 43835, -47749, -792, 5000, 8 ),
        new("Town of Goddard", 148024, -55281, -2728, 18500, 7 ),
        new("Heine", 111455, 219400, -3546, 25000, 6 ),
        new("The Town of Dion", 15671, 142994, -2704, 16500, 2 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 29500, 9 ),
        new("The Town of Gludio", -12694, 122776, -3114, 17500, 1 ),
        new("Ivory Tower", 85391, 16228, -3640, 1850, 4 ),
        new("Hunters Village", 117110, 76883, -2670, 2050, 0 ),
        new("Hardin's Private Academy", 105884, 109744, -3170, 3050, 3 ),
        new("Skyshadow Meadow", 89914, 46276, -3616, 390, 0 ),
        new("Plains of the Lizardmen", 87252, 85514, -3056, 950, 0 ),
        new("Outlaw Forest", 91539, -12204, -2440, 1600, 0 ),
        new("Sea of Spores", 64328, 26803, -3768, 1250, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1, 0 ),
        new("The Elven Village", 46951, 51550, -2976, 1, 0 ),
        new("The Dark Elven Village", 9709, 15566, -4500, 1, 0 ),
        new("Orc Village", -45158, -112583, -236, 1, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 1, 0 ),
        new("Gludin Village", -80826, 149775, -3043, 1, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3112, 1, 0 ),
        new("Dion Castle Town", 15671, 142994, -2696, 1, 0 ),
        new("Heine", 111455, 219400, -3546, 1, 0 ),
        new("Giran Castle Town", 83314, 148012, -3400, 1, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Ivory Tower", 85391, 16228, -3640, 1, 0 ),
        new("Hardin's Academy", 105884, 109744, -3170, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Forest of Evil", 93218, 16969, -3904, 1, 0 ),
        new("Timak Outpost", 67097, 68815, -3648, 1, 0 ),
        new("Altar of Rites", -44566, 77508, -3736, 1, 0 ),
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
        new("Dwarven Village", 115120, -178112, -880, 1000, 0 ),
        new("Gludin Village", -80826, 149775, -3043, 1000, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3112, 1000, 0 ),
        new("Dion Castle Town", 15671, 142994, -2696, 1000, 0 ),
        new("Heine", 111455, 219400, -3546, 1000, 0 ),
        new("Giran Castle Town", 83314, 148012, -3400, 1000, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1000, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1000, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1000, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1000, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1000, 0 ),
        new("Ivory Tower", 85391, 16228, -3640, 1000, 0 ),
        new("Hardin's Academy", 105884, 109744, -3170, 1000, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };
    public override IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Forest of Evil", 93218, 16969, -3904, 1000, 0 ),
        new("Timak Outpost", 67097, 68815, -3648, 1000, 0 ),
        new("Altar of Rites", -44566, 77508, -3736, 1000, 0 ),
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