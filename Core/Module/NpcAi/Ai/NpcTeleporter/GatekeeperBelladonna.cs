using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperBelladonna : Teleporter
{
    public override int PrimeHours => 1;
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Schuttgart", 87018, -143379, -1288, 85000, 9 ),
        new("Heine", 111455, 219400, -3546, 47000, 6 ),
        new("Town of Aden", 146705, 25840, -2000, 56000, 5 ),
        new("Town of Oren", 83011, 53207, -1470, 35000, 4 ),
        new("The Town of Dion", 15671, 142994, -2704, 3400, 2 ),
        new("Town of Goddard", 148024, -55281, -2728, 71000, 7 ),
        new("The Town of Giran", 83314, 148012, -3400, 29000, 3 ),
        new("Rune Township", 43835, -47749, -792, 53000, 8 ),
        new("The Village of Gludin", -80826, 149775, -3043, 7300, 0 ),
        new("Elven Village", 46890, 51531, -2976, 9200, 0 ),
        new("Dark Elf Village", 9716, 15502, -4500, 10000, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 32000, 0 ),
        new("Orc Village", -45186, -112459, -236, 23000, 0 ),
        new("Ruins of Agony", -41248, 122848, -2904, 790, 0 ),
        new("Ruins of Despair", -19120, 136816, -3752, 610, 0 ),
        new("The Ant Nest", -9959, 176184, -4160, 2100, 0 ),
        new("Windawood Manor", -28327, 155125, -3496, 1400, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Town of Schuttgart", 87018, -143379, -1288, 42500, 9 ),
        new("Heine", 111455, 219400, -3546, 23500, 6 ),
        new("Town of Aden", 146705, 25840, -2000, 28000, 5 ),
        new("Town of Oren", 83011, 53207, -1470, 27500, 4 ),
        new("The Town of Dion", 15671, 142994, -2704, 1700, 2 ),
        new("Town of Goddard", 148024, -55281, -2728, 35500, 7 ),
        new("The Town of Giran", 83314, 148012, -3400, 14500, 3 ),
        new("Rune Township", 43835, -47749, -792, 26500, 8 ),
        new("The Village of Gludin", -80826, 149775, -3043, 3650, 0 ),
        new("Elven Village", 46890, 51531, -2976, 4600, 0 ),
        new("Dark Elf Village", 9716, 15502, -4500, 5000, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 16000, 0 ),
        new("Orc Village", -45186, -112459, -236, 11500, 0 ),
        new("Ruins of Agony", -41248, 122848, -2904, 395, 0 ),
        new("Ruins of Despair", -19120, 136816, -3752, 305, 0 ),
        new("The Ant Nest", -9959, 176184, -4160, 1050, 0 ),
        new("Windawood Manor", -28327, 155125, -3496, 700, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1, 0 ),
        new("The Elven Village", 46890, 51531, -2976, 1, 0 ),
        new("The Dark Elven Village", 9716, 15502, -4500, 1, 0 ),
        new("Orc Village", -45186, -112459, -236, 1, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 1, 0 ),
        new("Gludin Village", -80826, 149775, -3043, 1, 0 ),
        new("Dion Castle Town", 15744, 142928, -2696, 1, 0 ),
        new("Heine", 111455, 219400, -3546, 1, 0 ),
        new("Giran Castle Town", 83336, 147972, -3400, 1, 0 ),
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
        new("Evil Hunting Grounds", -6989, 109503, -3040, 1, 0 ),
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
        new("The Elven Village", 46890, 51531, -2976, 1000, 0 ),
        new("The Dark Elven Village", 9716, 15502, -4500, 1000, 0 ),
        new("Orc Village", -45186, -112459, -236, 1000, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 1000, 0 ),
        new("Gludin Village", -80826, 149775, -3043, 1000, 0 ),
        new("Dion Castle Town", 15744, 142928, -2696, 1000, 0 ),
        new("Heine", 111455, 219400, -3546, 1000, 0 ),
        new("Giran Castle Town", 83336, 147972, -3400, 1000, 0 ),
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
        new("Evil Hunting Grounds", -6989, 109503, -3040, 1000, 0 ),
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