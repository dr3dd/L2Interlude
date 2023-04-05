using System.Collections.Generic;
using Core.Module.NpcAi.Ai.NpcType;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperCecile : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Underground Shopping Area", 84856, 15912, -4270, 0, 0),
        new("1st Floor Lobby", 85336, 16137, -3640, 0, 0 ), 
        new("3rd Floor Elven Wizard Guild", 85391, 16228, -2270, 0, 0 ),
        new("4th Floor Dark Wizard Guild", 85391, 16228, -1750, 0, 0 )
    };
}