using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperIlyana : Teleporter
{
    public override int PrimeHours => 1;

    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Goddard", 148024, -55281, -2728, 10000, 7 ),
        new("The Town of Gludio", -12694, 122776, -3114, 53000, 1 ),
        new("The Town of Giran", 83314, 148012, -3400, 59000, 3 ),
        new("The Town of Dion", 15671, 142994, -2704, 57000, 2 ),
        new("Heine", 111455, 219400, -3546, 82000, 6 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 10000, 9 ),
        new("Town of Aden", 146705, 25840, -2000, 37000, 5 ),
        new("Town of Oren", 82956, 53162, -1470, 10000, 4 ),
        new("Wild Beast Pastures", 43805, -88010, -2752, 4800, 0 ),
        new("Valley of Saints", 65307, -71445, -3688, 3800, 0 ),
        new("Forest of the Dead", 52107, -54328, -3158, 1200, 0 ),
        new("Swamp of Screams", 69340, -50203, -3314, 3000, 0 ),
        new("Monastery of Silence", 106414, -87799, -2920, 14000, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Town of Goddard", 148024, -55281, -2728, 5000, 7 ),
        new("The Town of Gludio", -12694, 122776, -3114, 26500, 1 ),
        new("The Town of Giran", 83314, 148012, -3400, 29500, 3 ),
        new("The Town of Dion", 15671, 142994, -2704, 28500, 2 ),
        new("Heine", 111455, 219400, -3546, 41000, 6 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 5000, 9 ),
        new("Town of Aden", 146705, 25840, -2000, 18500, 5 ),
        new("Town of Oren", 82956, 53162, -1470, 5000, 4 ),
        new("Wild Beast Pastures", 43805, -88010, -2752, 2400, 0 ),
        new("Valley of Saints", 65307, -71445, -3688, 1900, 0 ),
        new("Forest of the Dead", 52107, -54328, -3158, 600, 0 ),
        new("Swamp of Screams", 69340, -50203, -3314, 1500, 0 ),
        new("Monastery of Silence", 106414, -87799, -2920, 7000, 0 )
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
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("The Center of the Forest of the Dead", 54425, -41692, -3072, 1, 0 ),
        new("The Center of the Valley of Saints", 84092, -80084, -3504, 1, 0 ),
        new("Cursed Village", 57670, -41672, -3154, 1, 0 ),
        new("Stakato Nest", 90355, -44088, -2144, 1, 0 ),
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
        new("The Center of the Forest of the Dead", 54425, -41692, -3072, 1000, 0 ),
        new("The Center of the Valley of Saints", 84092, -80084, -3504, 1000, 0 ),
        new("Cursed Village", 57670, -41672, -3154, 1000, 0 ),
        new("Stakato Nest", 90355, -44088, -2144, 1000, 0 ),
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