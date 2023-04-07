using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterD : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Northern of Immortal Plateau", -8804, -114748, -3030, 50, 0 ),
        new("Southern of Immortal Plateau", -17870, -90980, -2528, 50, 0 ),
        new("Path to Cave of Trials", 8209, -93425, -2321, 50, 0 ),
        new("Frozen Waterfalls", 7603, -138871, -934, 50, 0 ),
        new("Cave of Trials", 9340, -112509, -2536, 50, 0 )
    };
}