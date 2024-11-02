using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperTatiana : Teleporter
{
    public override int PrimeHours => 1;

    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("The Town of Gludio", -12694, 122776, -3114, 71000, 1 ),
        new("The Town of Giran", 83314, 148012, -3400, 63000, 3 ),
        new("The Town of Dion", 15671, 142994, -2704, 71000, 2 ),
        new("Rune Township", 43835, -47749, -792, 10000, 8 ),
        new("Heine", 111455, 219400, -3546, 83000, 6 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 10000, 9 ),
        new("Town of Aden", 146705, 25840, -2000, 8100, 5 ),
        new("Town of Oren", 82971, 53207, -1488, 37000, 4 ),
        new("Varka Silenos Stronghold", 125740, -40864, -3736, 4200, 0 ),
        new("Ketra Orc Outpost", 146990, -67128, -3640, 1800, 0 ),
        new("Hot Springs", 144880, -113468, -2560, 9300, 0 ),
        new("Wall of Argos", 164564, -48145, -3536, 2200, 0 ),
        new("Monastery of Silence", 106414, -87799, -2920, 10000, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("The Town of Gludio", -12694, 122776, -3114, 35500, 1 ),
        new("The Town of Giran", 83314, 148012, -3400, 31500, 3 ),
        new("The Town of Dion", 15671, 142994, -2704, 35500, 2 ),
        new("Rune Township", 43835, -47749, -792, 5000, 8 ),
        new("Heine", 111455, 219400, -3546, 41500, 6 ),
        new("Town of Schuttgard", 87478, -142297, -1352, 5000, 9 ),
        new("Town of Aden", 146705, 25840, -2000, 4050, 5 ),
        new("Town of Oren", 82971, 53207, -1488, 18500, 4 ),
        new("Varka Silenos Stronghold", 125740, -40864, -3736, 2100, 0 ),
        new("Ketra Orc Outpost", 146990, -67128, -3640, 900, 0 ),
        new("Hot Springs", 144880, -113468, -2560, 4650, 0 ),
        new("Wall of Argos", 164564, -48145, -3536, 1100, 0 ),
        new("Monastery of Silence", 106414, -87799, -2920, 5000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1, 0 ),
        new("The Elven Village", 46951, 51550, -2976, 1, 0 ),
        new("The Dark Elven Village", 9709, 15566, -4500, 1, 0 ),
        new("Orc Village", -45158, -112583, -236, 1, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 1, 0 ),
        new("Gludin Village", -80826, 149775, -3043, 1, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3114, 1, 0 ),
        new("Dion Castle Town", 15671, 142994, -2704, 1, 0 ),
        new("Heine", 111455, 219400, -3546, 1, 0 ),
        new("Giran Castle Town", 83458, 148012, -3400, 1, 0 ),
        new("Oren Castle Town", 82956, 53162, -1470, 1, 0 ),
        new("Aden Castle Town", 146705, 25840, -2000, 1, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Varka Silenos Village", 107929, -52248, -2408, 1, 0 ),
        new("Ketra Orc Village", 149817, -80053, -5576, 1, 0 ),
        new("Devil's Pass", 106349, -61870, -2904, 1, 0 ),
        new("Garden of Wild Beasts", 132997, -60608, -2960, 1, 0 ),
        new("The Center of the Hot Springs", 144625, -101291, -3384, 1, 0 ),
        new("The Center of the Wall of Argos", 183140, -53307, -1896, 1, 0 ),
        new("Shrine of Loyalty", 187987, -59566, -2826, 1, 0 ),
        new("Four Sepulchers", 178127, -84435, -7215, 1, 0 ),
        new("Imperial Tomb", 186699, -75915, -2826, 1, 0 ),
        new("Entrance to the Forge of the Gods", 169533, -116228, -2312, 1, 0 ),
        new("Forge of the Gods - Top Level", 173436, -112725, -3680, 1, 0 ),
        new("Forge of the Gods - Lower Level", 180260, -111913, -5851, 1, 0 ),
        new("Monastery of Silence", 106414, -87799, -2920, 1, 0 ),
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
        new("Gludin Village", -80826, 149775, -3043, 1000, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3114, 1000, 0 ),
        new("Dion Castle Town", 15671, 142994, -2704, 1000, 0 ),
        new("Heine", 111455, 219400, -3546, 1000, 0 ),
        new("Giran Castle Town", 83458, 148012, -3400, 1000, 0 ),
        new("Oren Castle Town", 82956, 53162, -1470, 1000, 0 ),
        new("Aden Castle Town", 146705, 25840, -2000, 1000, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1000, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1000, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1000, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Varka Silenos Village", 107929, -52248, -2408, 1000, 0 ),
        new("Ketra Orc Village", 149817, -80053, -5576, 1000, 0 ),
        new("Devil's Pass", 106349, -61870, -2904, 1000, 0 ),
        new("Garden of Wild Beasts", 132997, -60608, -2960, 1000, 0 ),
        new("The Center of the Hot Springs", 144625, -101291, -3384, 1000, 0 ),
        new("The Center of the Wall of Argos", 183140, -53307, -1896, 1000, 0 ),
        new("Shrine of Loyalty", 187987, -59566, -2826, 1000, 0 ),
        new("Four Sepulchers", 178127, -84435, -7215, 1000, 0 ),
        new("Imperial Tomb", 186699, -75915, -2826, 1000, 0 ),
        new("Entrance to the Forge of the Gods", 169533, -116228, -2312, 1000, 0 ),
        new("Forge of the Gods - Top Level", 173436, -112725, -3680, 1000, 0 ),
        new("Forge of the Gods - Lower Level", 180260, -111913, -5851, 1000, 0 ),
        new("Monastery of Silence", 106414, -87799, -2920, 1000, 0 ),
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