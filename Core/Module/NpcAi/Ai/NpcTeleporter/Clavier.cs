using Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class Clavier : Teleporter
{
    public override int PrimeHours => 1;

    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Oren", 83011, 53207, -1470, 9400, 4 ),
        new("Heine", 111455, 219400, -3546, 7600, 6 ),
        new("The Town of Dion", 15671, 142994, -2704, 6800, 2 ),
        new("Town of Goddard", 148024, -55281, -2728, 63000, 7 ),
        new("Rune Township", 43835, -47749, -792, 59000, 8 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 87000, 9 ),
        new("The Town of Gludio", -12694, 122776, -3114, 29000, 1 ),
        new("Town of Aden", 146705, 25840, -2000, 13000, 5 ),
        new("Giran Harbor", 47938, 186864, -3480, 5200, 0 ),
        new("Hardin's Private Academy", 105918, 109759, -3170, 4400, 3 ),
        new("Dragon Valley", 73024, 118485, -3688, 1800, 0 ),
        new("Antharas' Lair", 131557, 114509, -3712, 7000, 0 ),
        new("Devil's Isle", 43408, 206881, -3752, 5700, 0 ),
        new("Breka's Stronghold", 85546, 131328, -3672, 1000, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Town of Oren", 83011, 53207, -1470, 4700, 4 ),
        new("Heine", 111455, 219400, -3546, 3800, 6 ),
        new("The Town of Dion", 15671, 142994, -2704, 3400, 2 ),
        new("Town of Goddard", 148024, -55281, -2728, 31500, 7 ),
        new("Rune Township", 43835, -47749, -792, 29500, 8 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 43500, 9 ),
        new("The Town of Gludio", -12694, 122776, -3114, 14500, 1 ),
        new("Town of Aden", 146705, 25840, -2000, 6500, 5 ),
        new("Giran Harbor", 47938, 186864, -3480, 2600, 0 ),
        new("Hardin's Private Academy", 105918, 109759, -3170, 2200, 3 ),
        new("Dragon Valley", 73024, 118485, -3688, 900, 0 ),
        new("Antharas' Lair", 131557, 114509, -3712, 3500, 0 ),
        new("Devil's Isle", 43408, 206881, -3752, 2850, 0 ),
        new("Breka's Stronghold", 85546, 131328, -3672, 500, 0 )
    };
    public override IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1, 0 ),
        new("The Elven Village", 46951, 51550, -2976, 1, 0 ),
        new("The Dark Elven Village", 9709, 15566, -4500, 1, 0 ),
        new("Orc Village", -45158, -112583, -236, 1, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 0, 0 ),
        new("Gludin Village", -80826, 149775, -3043, 1, 0 ),
        new("Gludio Castle Town", -12694, 122776, -3112, 1, 0 ),
        new("Dion Castle Town", 15671, 142994, -2696, 1, 0 ),
        new("Heine", 111455, 219400, -3546, 1, 0 ),
        new("The Town of Oren", 83011, 53207, -1470, 1, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Hardin's Academy", 105918, 109759, -3170, 1, 0 ),
        new("Giran Harbor", 47935, 186810, -3420, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };
    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Gorgon Flower Garden", 113553, 134813, -3668, 1, 0 ),
        new("Antharas' Lair - 1st Level", 147071, 120156, -4520, 1, 0 ),
        new("Antharas' Lair - 2nd Level", 151689, 112615, -5520, 1, 0 ),
        new("Antharas' Lair - Magic Force Field Bridge", 146425, 109898, -3424, 1, 0 ),
        new("The Heart of Antharas' Lair", 154396, 121235, -3808, 1, 0 ),
        new("The Center of Dragon Valley", 122824, 110836, -3727, 1, 0 ),
        new("Hardin's Private Academy", 105918, 109759, -3170, 1, 0 ),
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
        new("The Town of Oren", 83011, 53207, -1470, 1000, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1000, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1000, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1000, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1000, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1000, 0 ),
        new("Hardin's Academy", 105918, 109759, -3170, 1000, 0 ),
        new("Giran Harbor", 47935, 186810, -3420, 1000, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };
    public override IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Gorgon Flower Garden", 113553, 134813, -3668, 1000, 0 ),
        new("Antharas' Lair - 1st Level", 147071, 120156, -4520, 1000, 0 ),
        new("Antharas' Lair - 2nd Level", 151689, 112615, -5520, 1000, 0 ),
        new("Antharas' Lair - Magic Force Field Bridge", 146425, 109898, -3424, 1000, 0 ),
        new("The Heart of Antharas' Lair", 154396, 121235, -3808, 1000, 0 ),
        new("The Center of Dragon Valley", 122824, 110836, -3727, 1000, 0 ),
        new("Hardin's Private Academy", 105918, 109759, -3170, 1000, 0 ),
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