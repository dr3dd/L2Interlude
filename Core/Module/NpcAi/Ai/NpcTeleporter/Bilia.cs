using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class Bilia : Teleporter
{
    public override int PrimeHours => 1;

    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Rune Township", 43835, -47749, -792, 10000, 8 ),
        new("Town of Goddard", 147596, -56294, -2776, 10000, 7 ),
        new("Town of Aden", 146705, 25840, -2000, 53000, 5 ),
        new("Town of Oren", 83011, 53207, -1470, 59000, 4 ),
        new("Heine", 111455, 219400, -3546, 100000, 6 ),
        new("The Town of Giran", 83458, 148012, -3400, 87000, 3 ),
        new("The Town of Dion", 15671, 142994, -2704, 88000, 2 ),
        new("The Town of Gludio", -12694, 122776, -3114, 85000, 1 ),
        new("Orc Village", -45158, -112583, -236, 13000, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 4400, 0 ),
        new("Den of Evil", 68693, -110438, -1904, 3000, 0 ),
        new("Plunderous Plains", 111965, -154172, -1528, 1600, 0 ),
        new("Frozen Labyrinth", 113903, -108752, -848, 3500, 0 ),
        new("Crypts of Disgrace", 47692, -115745, -3744, 1900, 0 ),
        new("Pavel Ruins", 91280, -117152, -3928, 2100, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Rune Township", 43835, -47749, -792, 5000, 8 ),
        new("Town of Goddard", 147596, -56294, -2776, 5000, 7 ),
        new("Town of Aden", 146705, 25840, -2000, 26500, 5 ),
        new("Town of Oren", 83011, 53207, -1470, 29500, 4 ),
        new("Heine", 111455, 219400, -3546, 50000, 6 ),
        new("The Town of Giran", 83458, 148012, -3400, 43500, 3 ),
        new("The Town of Dion", 15671, 142994, -2704, 44000, 2 ),
        new("The Town of Gludio", -12694, 122776, -3114, 42500, 1 ),
        new("Orc Village", -45158, -112583, -236, 6500, 0 ),
        new("Dwarven Village", 115120, -178224, -917, 2200, 0 ),
        new("Den of Evil", 68693, -110438, -1904, 1500, 0 ),
        new("Plunderous Plains", 111965, -154172, -1528, 800, 0 ),
        new("Frozen Labyrinth", 113903, -108752, -848, 1750, 0 ),
        new("Crypts of Disgrace", 47692, -115745, -3744, 950, 0 ),
        new("Pavel Ruins", 91280, -117152, -3928, 1050, 0 )
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
        new("The Town of Oren", 83011, 53207, -1470, 1, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Frost Lake", 107577, -122392, -3632, 1, 0 ),
        new("Crypts of Disgrace", 44221, -114232, -2784, 1, 0 ),
        new("Sky Wagon Relic", 121618, -141554, -1496, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemSSQ => new List<TeleportList>
    {
        new("Necropolis of Sacrifice", -41184, 206752, -3357, 1, 0 ),
        new("Heretics Catacomb", 39232, 143568, -3651, 1, 0 ),
        new("Piligrims Necropolis", 45600, 126944, -3686, 1, 0 ),
        new("Catacomb of the Branded", 43200, 170688, -3251, 1, 0 ),
        new("Worshipers Necropolis", 107514, 174329, -3704, 1, 0 ),
        new("Catacomb of the Apostate", 74672, 78032, -3398, 1, 0 ),
        new("Patriots Necropolis", -25472, 77728, -3446, 1, 0 ),
        new("Catacomb of the Witch", 136672, 79328, -3702, 1, 0 ),
        new("Ascetics Necropolis", -56064, 78720, -3011, 1, 0 ),
        new("Martyrs Necropolis", 114496, 132416, -3101, 1, 0 ),
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
        new("The Town of Oren", 83011, 53207, -1470, 1000, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1000, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1000, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1000, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1000, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Frost Lake", 107577, -122392, -3632, 1000, 0 ),
        new("Crypts of Disgrace", 44221, -114232, -2784, 1000, 0 ),
        new("Sky Wagon Relic", 121618, -141554, -1496, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemSSQ => new List<TeleportList>
    {
        new("Necropolis of Sacrifice", -41184, 206752, -3357, 1000, 0 ),
        new("Heretics Catacomb", 39232, 143568, -3651, 1000, 0 ),
        new("Piligrims Necropolis", 45600, 126944, -3686, 1000, 0 ),
        new("Catacomb of the Branded", 43200, 170688, -3251, 1000, 0 ),
        new("Worshipers Necropolis", 107514, 174329, -3704, 1000, 0 ),
        new("Catacomb of the Apostate", 74672, 78032, -3398, 1000, 0 ),
        new("Patriots Necropolis", -25472, 77728, -3446, 1000, 0 ),
        new("Catacomb of the Witch", 136672, 79328, -3702, 1000, 0 ),
        new("Ascetics Necropolis", -56064, 78720, -3011, 1000, 0 ),
        new("Martyrs Necropolis", 114496, 132416, -3101, 1000, 0 ),
        new("Disciples Necropolis", 168560, -17968, -3174, 1000, 0 ),
        new("Saints Necropolis", 79296, 209584, -3709, 1000, 0 ),
        new("Catacomb of Dark Omens", -22480, 13872, -3174, 1000, 0 ),
        new("Catacomb of the Forbidden Path", 110912, 84912, -4816, 1000, 0 )
    };


}