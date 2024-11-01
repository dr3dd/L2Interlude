using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperFlauen : Teleporter
{
    public override int PrimeHours => 1;
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("The Town of Giran", 83314, 148012, -3400, 7600, 3 ),
        new("Town of Oren", 83011, 53207, -1470, 50000, 4 ),
        new("The Town of Dion", 15472, 142880, -2699, 12000, 2 ),
        new("Town of Aden", 146705, 25840, -2000, 59000, 5 ),
        new("Town of Goddard", 147596, -56294, -2776, 83000, 7 ),
        new("Rune Township", 43835, -47749, -792, 82000, 8 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 100000, 9 ),
        new("The Town of Gludio", -12694, 122776, -3114, 47000, 1 ),
        new("Giran Harbor", 47935, 186810, -3420, 7100, 3 ),
        new("Field of Silence", 91088, 182384, -3192, 2500, 0 ),
        new("Field of Whispers", 74592, 207656, -3032, 2300, 0 ),
        new("Alligator Island", 115583, 192261, -3488, 2100, 0 ),
        new("Garden of Eva", 84413, 234334, -3656, 2400, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("The Town of Giran", 83314, 148012, -3400, 3800, 3 ),
        new("Town of Oren", 83011, 53207, -1470, 25000, 4 ),
        new("The Town of Dion", 15472, 142880, -2699, 6000, 2 ),
        new("Town of Aden", 146705, 25840, -2000, 28500, 5 ),
        new("Town of Goddard", 147596, -56294, -2776, 41500, 7 ),
        new("Rune Township", 43835, -47749, -792, 41000, 8 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 50000, 9 ),
        new("The Town of Gludio", -12694, 122776, -3114, 23500, 1 ),
        new("Giran Harbor", 47935, 186810, -3420, 3550, 3 ),
        new("Field of Silence", 91088, 182384, -3192, 1750, 0 ),
        new("Field of Whispers", 74592, 207656, -3032, 1150, 0 ),
        new("Alligator Island", 115583, 192261, -3488, 1050, 0 ),
        new("Garden of Eva", 84413, 234334, -3656, 1200, 0 )
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
        new("Giran Castle Town", 83458, 148012, -3400, 1, 0 ),
        new("The Town of Oren", 82956, 53162, -1470, 1, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1, 0 ),
        new("Aden Castle Town", 146705, 25840, -2000, 1, 0 ),
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
        new("Field of Silence (Western Section)", 69748, 186111, -2872, 1, 0 ),
        new("Field of Whispers (Eastern Section)", 97786, 209303, -3040, 1, 0 ),
        new("The Center of Alligator Island", 113708, 178387, -3232, 1, 0 ),
        new("Inside the Garden of Eva", 82693, 242220, -6712, 1, 0 ),
        new("Garden of Eva - 2nd level", 79248, 247390, -8816, 1, 0 ),
        new("Garden of Eva - 3rd level", 77868, 250400, -9328, 1, 0 ),
        new("Garden of Eva - 4th level", 78721, 253309, -9840, 1, 0 ),
        new("Garden of Eva - 5th level", 82570, 252464, -10592, 1, 0 ),
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
        new("Giran Castle Town", 83458, 148012, -3400, 1000, 0 ),
        new("The Town of Oren", 82956, 53162, -1470, 1000, 0 ),
        new("Hunters Village", 117110, 76883, -2670, 1000, 0 ),
        new("Aden Castle Town", 146705, 25840, -2000, 1000, 0 ),
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
        new("Field of Silence (Western Section)", 69748, 186111, -2872, 1000, 0 ),
        new("Field of Whispers (Eastern Section)", 97786, 209303, -3040, 1000, 0 ),
        new("The Center of Alligator Island", 113708, 178387, -3232, 1000, 0 ),
        new("Inside the Garden of Eva", 82693, 242220, -6712, 1000, 0 ),
        new("Garden of Eva - 2nd level", 79248, 247390, -8816, 1000, 0 ),
        new("Garden of Eva - 3rd level", 77868, 250400, -9328, 1000, 0 ),
        new("Garden of Eva - 4th level", 78721, 253309, -9840, 1000, 0 ),
        new("Garden of Eva - 5th level", 82570, 252464, -10592, 1000, 0 ),
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