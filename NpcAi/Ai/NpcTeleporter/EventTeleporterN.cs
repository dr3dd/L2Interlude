using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class EventTeleporterN : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Varka Sillenos Outpost", 125740, -40864, -3736, 1000, 0 ),
            new("Ketra Orc Outpost", 146990, -67128, -3640, 1000, 0 ),
            new("The Four Sepulchers", 178127, -84435, -7215, 1000, 0 ),
            new("Imperial Tomb", 186699, -75915, -2826, 1000, 0 ),
            new("Shrine of the Loyal", 187987, -59566, -2826, 1000, 0 ),
            new("Forge of Gods", 169533, -116228, -2312, 1000, 0 ),
            new("Forge of the Gods Upper Level", 173436, -112725, -3680, 1000, 0 ),
            new("Forge of the Gods Lower Level", 180260, -111913, -5851, 1000, 0 ),
            new("Wall of Argos", 164564, -48145, -3536, 1000, 0 ),
            new("Hot Springs", 144880, -113468, -2560, 1000, 0 ),
            new("Varka Silenos Village", 107929, -52248, -2408, 1000, 0 ),
            new("Ketra Orc Village", 149817, -80053, -5576, 1000, 0 )
        };
    }
}
