using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class Trishya : Teleporter
{
    public override int PrimeHours => 1;

    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Giran Castle Town", 83336, 147972, -3400, 6800, 3 ),
        new("Heine", 111455, 219400, -3546, 12000, 6 ),
        new("The Town of Gludio", -12787, 122779, -3114, 3400, 1 ),
        new("Town of Goddard", 148024, -55281, -2728, 71000, 7 ),
        new("Rune Township", 43835, -47749, -792, 57000, 8 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 88000, 9 ),
        new("Town of Aden", 146705, 25840, -2000, 52000, 5 ),
        new("Town of Oren", 83011, 53207, -1470, 33000, 4 ),
        new("Cruma Marshlands", 5106, 126916, -3664, 760, 0 ),
        new("Cruma Tower", 17225, 114173, -3440, 2300, 0 ),
        new("Fortress of Resistance", 47382, 111278, -2104, 1700, 0 ),
        new("Plains of Dion", 630, 179184, -3720, 1500, 0 ),
        new("Bee Hive", 34475, 188095, -2976, 2900, 0 ),
        new("Tanor Canyon", 60374, 164301, -2856, 3900, 0 )
    };

    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Giran Castle Town", 83336, 147972, -3400, 3400, 3 ),
        new("Heine", 111455, 219400, -3546, 6000, 6 ),
        new("The Town of Gludio", -12787, 122779, -3114, 1700, 1 ),
        new("Town of Goddard", 148024, -55281, -2728, 35500, 7 ),
        new("Rune Township", 43835, -47749, -792, 29500, 8 ),
        new("Town of Schuttgart", 87018, -143379, -1288, 44000, 9 ),
        new("Town of Aden", 146705, 25840, -2000, 26000, 5 ),
        new("Town of Oren", 83011, 53207, -1470, 16500, 4 ),
        new("Cruma Marshlands", 5106, 126916, -3664, 380, 0 ),
        new("Cruma Tower", 17225, 114173, -3440, 1150, 0 ),
        new("Fortress of Resistance", 47382, 111278, -2104, 850, 0 ),
        new("Plains of Dion", 630, 179184, -3720, 750, 0 ),
        new("Bee Hive", 34475, 188095, -2976, 1450, 0 ),
        new("Tanor Canyon", 60374, 164301, -2856, 1950, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        new("Talking Island Village", -84141, 244623, -3729, 1, 0 ),
        new("The Elven Village", 46951, 51550, -2976, 1, 0 ),
        new("The Dark Elven Village", 9709, 15566, -4500, 1, 0 ),
        new("Orc Village", -45158, -112583, -236, 1, 0 ),
        new("Dwarven Village", 115120, -178112, -880, 1, 0 ),
        new("Gludio Castle Town", -12787, 122779, -3112, 1, 0 ),
        new("Heine", 111455, 219400, -3546, 1, 0 ),
        new("Giran Harbor", 47938, 186864, -3420, 1, 0 ),
        new("Giran Castle Town", 83336, 147972, -3400, 1, 0 ),
        new("The Town of Oren", 83011, 53207, -1470, 1, 0 ),
        new("Hunters Village", 117088, 76931, -2670, 1, 0 ),
        new("Aden Castle Town", 146783, 25808, -2000, 1, 0 ),
        new("Rune Castle Town", 43826, -47688, -792, 1, 0 ),
        new("Goddard Castle Town", 147978, -55408, -2728, 1, 0 ),
        new("Town of Schuttgart", 87478, -142297, -1352, 1, 0 ),
        new("Gludin Harbor Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Coliseum", 146440, 46723, -3400, 1, 0 )
    };

    public override IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        new("Gludin Arena", -87328, 142266, -3640, 1, 0 ),
        new("Giran Arena", 73579, 142709, -3768, 1, 0 ),
        new("Execution Ground", 46165, 150008, -3208, 1, 0 ),
        new("Floran Agricultural Area", 15227, 161866, -3608, 1, 0 ),
        new("The Center of the Cruma Marshlands", 5941, 125455, -3400, 1, 0 ),
        new("Cruma Tower - First Floor", 17724, 113950, -11672, 1, 0 ),
        new("Cruma Tower - Second Floor", 17723, 108284, -9056, 1, 0 ),
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
        new("Gludio Castle Town", -12787, 122779, -3112, 1000, 0 ),
        new("Heine", 111455, 219400, -3546, 1000, 0 ),
        new("Giran Harbor", 47938, 186864, -3420, 1000, 0 ),
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
        new("Execution Ground", 46165, 150008, -3208, 1000, 0 ),
        new("Floran Agricultural Area", 15227, 161866, -3608, 1000, 0 ),
        new("The Center of the Cruma Marshlands", 5941, 125455, -3400, 1000, 0 ),
        new("Cruma Tower - First Floor", 17724, 113950, -11672, 1000, 0 ),
        new("Cruma Tower - Second Floor", 17723, 108284, -9056, 1000, 0 ),
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