using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterH : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Cruma Marshlands", 5941, 125455, -3400, 1000, 0 ),
        new("Partisan Hideaway", 46467, 126885, -3720, 1000, 0 ),
        new("Execution Ground", 46165, 150008, -3208, 1000, 0 ),
        new("Plains of Dion", -716, 178965, -3704, 1000, 0 ),
        new("Floran Agricultural Area", 15227, 161866, -3608, 1000, 0 ),
        new("Beehive", 19050, 186330, -3544, 1000, 0 ),
        new("Tanor Canyon", 33565, 162393, -3600, 1000, 0 ),
        new("Cruma Tower", 17255, 114177, -3440, 1000, 0 ),
        new("Cruma Tower Floor 1", 17724, 113950, -11672, 1000, 0 ),
        new("Cruma Tower Floor 2", 17723, 108284, -9056, 1000, 0 ),
        new("Cruma Tower Floor 3", 17724, 119684, -9064, 1000, 0 )
    };
}