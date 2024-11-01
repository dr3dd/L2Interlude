using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperElisabeth : Teleporter
{
    public override int PrimeHours => 1;
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Town of Goddard", 148024, -55281, -2728, 8100, 7 ),
        new("Town of Oren", 82971, 53207, -1470, 6900, 4 ),
        new("The Town of Giran", 83314, 148012, -3400, 13000, 3 ),
        new("Heine", 111455, 219400, -3546, 59000, 6 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 53000, 9 ),
        new("The Town of Dion", 15671, 142994, -2704, 52000, 2 ),
        new("The Town of Gludio", -12694, 122776, -3114, 56000, 1 ),
        new("Rune Township", 43835, -47749, -792, 37000, 8 ),
        new("Hunters Village", 117110, 76883, -2670, 5900, 0 ),
        new("Coliseum", 146440, 46723, -3400, 2000, 0 ),
        new("Forsaken Plains", 168217, 37990, -4072, 1900, 0 ),
        new("Seal of Shilen", 184742, 19745, -3168, 3000, 0 ),
        new("Forest of Mirrors", 142065, 81300, -3000, 4400, 0 ),
        new("Blazing Swamp", 155310, -16339, -3320, 6800, 0 ),
        new("Fields of Massacre", 183543, -14974, -2776, 6500, 0 ),
        new("Ancient Battleground", 106517, -2871, -3416, 5900, 0 ),
        new("Silent Valley", 170838, 55776, -5240, 6100, 0 ),
        new("Tower of Insolence", 114649, 11115, -5120, 4200, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Town of Goddard", 148024, -55281, -2728, 4050, 7 ),
        new("Town of Oren", 82971, 53207, -1470, 3450, 4 ),
        new("The Town of Giran", 83314, 148012, -3400, 6500, 3 ),
        new("Heine", 111455, 219400, -3546, 29500, 6 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 26500, 9 ),
        new("The Town of Dion", 15671, 142994, -2704, 26000, 2 ),
        new("The Town of Gludio", -12694, 122776, -3114, 28000, 1 ),
        new("Rune Township", 43835, -47749, -792, 18500, 8 ),
        new("Hunters Village", 117110, 76883, -2670, 2950, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 ),
        new("Forsaken Plains", 168217, 37990, -4072, 850, 0 ),
        new("Seal of Shilen", 184742, 19745, -3168, 1500, 0 ),
        new("Forest of Mirrors", 142065, 81300, -3000, 2200, 0 ),
        new("Blazing Swamp", 155310, -16339, -3320, 3400, 0 ),
        new("Fields of Massacre", 183543, -14974, -2776, 3450, 0 ),
        new("Ancient Battleground", 106517, -2871, -3416, 2850, 0 ),
        new("Silent Valley", 170838, 55776, -5240, 3050, 0 ),
        new("Tower of Insolence", 114649, 11115, -5120, 2100, 0 )
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
        new("Giran Castle Town", 83458, 148012, -3400, 1, 0 ),
        new("The Town of Oren", 82956, 53162, -1470, 1, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Ivory Tower", 85343, 16267, -3640, 1, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Entrance to The Giant's Cave", 181737, 46469, -4352, 1, 0 ),
        new("Plains of Glory", 135580, 19467, -3424, 1, 0 ),
        new("War-Torn Plains", 156898, 11217, -4032, 1, 0 ),
        new("Tower of Insolence,  3rd Floor", 110848, 16154, -2120, 1, 0 ),
        new("Tower of Insolence,  5th Floor", 118404, 15988, 832, 1, 0 ),
        new("Tower of Insolence,  7th Floor", 115064, 12181, 2960, 1, 0 ),
        new("Tower of Insolence,  10th Floor", 118525, 16455, 5984, 1, 0 ),
        new("Tower of Insolence,  13th Floor", 115384, 16820, 9000, 1, 0 ),
        new("Hunters Valley", 114306, 86573, -3112, 1, 0 ),
        new("Anghel Waterfall", 166182, 91560, -3168, 1, 0 ),
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
        new("Giran Castle Town", 83458, 148012, -3400, 1000, 0 ),
        new("The Town of Oren", 82956, 53162, -1470, 1000, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1000, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1000, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1000, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1000, 0 ),
        new("Ivory Tower", 85343, 16267, -3640, 1000, 0 ),
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1000, 0 )
    };

    public override IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1000, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1000, 0 ),
        new("Entrance to The Giant's Cave", 181737, 46469, -4352, 1000, 0 ),
        new("Plains of Glory", 135580, 19467, -3424, 1000, 0 ),
        new("War-Torn Plains", 156898, 11217, -4032, 1000, 0 ),
        new("Tower of Insolence,  3rd Floor", 110848, 16154, -2120, 1000, 0 ),
        new("Tower of Insolence,  5th Floor", 118404, 15988, 832, 1000, 0 ),
        new("Tower of Insolence,  7th Floor", 115064, 12181, 2960, 1000, 0 ),
        new("Tower of Insolence,  10th Floor", 118525, 16455, 5984, 1000, 0 ),
        new("Tower of Insolence,  13th Floor", 115384, 16820, 9000, 1000, 0 ),
        new("Hunters Valley", 114306, 86573, -3112, 1000, 0 ),
        new("Anghel Waterfall", 166182, 91560, -3168, 1000, 0 ),
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