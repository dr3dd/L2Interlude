using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperBelladonna : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new ("Town of Schuttgart", 87018, -143379, -1288, 85000, 9 ),
        new ("Heine", 111455, 219400, -3546, 47000, 6 ),
        new ("Town of Aden", 146705, 25840, -2000, 56000, 5 ),
        new ("Town of Oren", 83011, 53207, -1470, 35000, 4 ),
        new ("The Town of Dion", 15671, 142994, -2704, 3400, 2 ),
        new ("Town of Goddard", 148024, -55281, -2728, 71000, 7 ),
        new ("The Town of Giran", 83314, 148012, -3400, 29000, 3 ),
        new ("Rune Township", 43835, -47749, -792, 53000, 8 ),
        new ("The Village of Gludin", -80826, 149775, -3043, 7300, 0 ),
        new ("Elven Village", 46890, 51531, -2976, 9200, 0 ),
        new ("Dark Elf Village", 9716, 15502, -4500, 10000, 0 ),
        new ("Dwarven Village", 115120, -178112, -880, 32000, 0 ),
        new ("Orc Village", -45186, -112459, -236, 23000, 0 ),
        new ("Ruins of Agony", -41248, 122848, -2904, 790, 0 ),
        new ("Ruins of Despair", -19120, 136816, -3752, 610, 0 ),
        new ("The Ant Nest", -9959, 176184, -4160, 2100, 0 ),
        new ("Windawood Manor", -28327, 155125, -3496, 1400, 0 )
    };
}