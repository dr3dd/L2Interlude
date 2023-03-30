using Helpers;

namespace NpcAi.Ai.NpcCitizen
{
    public class GuideHumanCnacelot : NewbieGuide
    {
        public override IList<TeleportList> NewbieTokenTeleports => new List<TeleportList>
        {
            {new("Dark Elf Village", 9716, 15502, -4500, 1, 0 )},
            {new("Dwarven Village", 115120, -178112, -880, 1, 0 )},
            {new("Elven Village",46890, 51531, -2976, 1, 0 )},
            {new("Orc Village", -45186, -112459, -236, 1, 0 )},
        };
    }
}
