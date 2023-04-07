using System.Collections.Generic;
using Helpers;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class EventTeleporterG : Teleporter
{
    public override IList<TeleportList> Position => new List<TeleportList>
    {
        new("Looting Area of Evil Spirits", -6536, 106065, -3152, 100, 0 ),
        new("Maille Lizardman Barraks", -27511, 101577, -3320, 100, 0 ),
        new("Neutral Zone", -10672, 75960, -3595, 100, 0 ),
        new("Ruins of Agony", -41925, 122539, -3032, 100, 0 ),
        new("Fellmere Lake", -50174, 129303, -2912, 100, 0 ),
        new("Ruins of Despair", -23990, 143971, -3840, 100, 0 ),
        new("The Ruins Bend", -36652, 135591, -3160, 100, 0 ),
        new("Abandoned Camp", -46932, 140883, -2936, 100, 0 ),
        new("Windy Hill", -93453, 89814, -3240, 100, 0 ),
        new("Orc Barraks", -85329, 106369,  -3603, 100, 0 ),
        new("Fellmere Harvest Grounds", -70387, 115501, -3472, 100, 0 ),
        new("Winawood Manor", -28257, 156638, -3472, 100, 0 ),
        new("Windmill Hill", -72152, 173347, -3576, 100, 0 ),
        new("Wasteland", -44994, 188099, -3256, 100, 0 ),
        new("The Ant Nest", -14913, 170017, -2893, 100, 0 ),
        new("East of Forgotten Temple", -47506, 179572, -3669, 100, 0 ),
        new("Southern Pathway to the Wastelands", -16730, 209417, -3691, 100, 0 ),
        new("Langk Lizardman Dwelling", -45210, 202654, -3592, 100, 0 ),
        new("South of Forgotten Temple", -53001, 191425, -3568, 100, 0 ),
        new("Forgotten Temple", -54026, 179504, -4650, 100, 0 )
    };
}