using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperElisabeth : Teleporter
{
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
}