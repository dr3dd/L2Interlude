using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperMariell : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Underground Shopping Area", 84814, 15926, -4270, 0, 0 ),
        new("1st Floor Lobby", 85391, 16228, -3640, 0, 0 ),
        new("2nd Floor Human Wizard Guild", 85391, 16228, -2780, 0, 0 ),
        new("4th Floor Dark Wizard Guild", 85343, 16267, -1750, 0, 0 )
    };

}