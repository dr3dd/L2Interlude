using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperMerian : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Underground Shopping Area", 84872, 15882, -4270, 0, 0 ),
        new("1st Floor Lobby", 85343, 16267, -3640, 0, 0 ),
        new("2nd Floor Human Wizard Guild", 85343, 16267, -2780, 0, 0 ),
        new("3rd Floor Elven Wizard Guild", 85343, 16267, -2270, 0, 0 )
    };
}