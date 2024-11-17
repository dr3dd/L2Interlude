using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class Karin : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("1st Floor Lobby", 85289, 16225, -3640, 0, 0 ),
        new("2nd Floor Human Wizard Guild", 85336, 16137, -2780, 0, 0 ),
        new("3rd Floor Elven Wizard Guild", 85336, 16137, -2270, 0, 0 ),
        new("4th Floor Dark Wizard Guild", 85336, 16137, -1750, 0, 0 )
    };


}