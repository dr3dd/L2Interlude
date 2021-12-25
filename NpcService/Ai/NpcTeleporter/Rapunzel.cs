using System.Collections.Generic;
using Helpers;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcTeleporter
{
    public class Rapunzel : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("The Village of Gludin", -80749, 149834, -3043, 18000, 0 ),
            new("Dark Elf Village", 9716, 15502, -4500, 24000, 0 ),
            new("Dwarven Village", 115120, -178112, -880, 46000, 0 ),
            new("Elven Village", 46890, 51531, -2976, 23000, 0 ),
            new("Orc Village", -45186, -112459, -236, 35000, 0 ),
            new("Elven Ruins", -112367, 234703, -3688, 830, 0 ),
            new("Singing Waterfall", -111728, 244330, -3448, 770, 0 ),
            new("Talking Island, Western Territory (Northern Area)", -106696, 214691, -3424, 1000, 0 ),
            new("Obelisk of Victory", -99586, 237637, -3568, 470, 0 )
        };
        
    }
}
