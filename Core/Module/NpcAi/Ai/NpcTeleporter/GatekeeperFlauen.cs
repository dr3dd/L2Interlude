using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperFlauen : Teleporter
{
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
}