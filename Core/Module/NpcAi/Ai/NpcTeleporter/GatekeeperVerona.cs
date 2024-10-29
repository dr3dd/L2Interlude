using Helpers;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperVerona : TeleporterMultiList
{
    public override int PrimeHours => 1;
    public override IList<TeleportList> Position1 => new List<TeleportList>
    {
        new("Town of Oren", 82971, 53207, -1470, 3700, 4 ),
        new("Hunters Village", 117156, 76878, -2670, 6800, 0 ),
        new("Town of Aden", 146768, 25856, -2000, 6200, 5 )
    };
    public override IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        new("Town of Oren", 82971, 53207, -1470, 1850, 4 ),
        new("Hunters Village", 117156, 76878, -2670, 3400, 0 ),
        new("Town of Aden", 146768, 25856, -2000, 3100, 5 )
    };
    public override IList<TeleportList> Position2 => new List<TeleportList>
    {
        new("Underground Shopping Area", 84852, 15863, -4270, 0, 0 ),
        new("2nd Floor Human Wizard Guild", 85289, 16225, -2780, 0, 0 ),
        new("3rd Floor Elven Wizard Guild", 85289, 16225, -2270, 0, 0 ),
        new("4th Floor Dark Wizard Guild", 85289, 16225, -1750, 0, 0 )
    };
}