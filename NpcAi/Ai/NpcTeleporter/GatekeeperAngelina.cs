using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class GatekeeperAngelina : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Giran Castle Town", 83336, 147972, -3400, 5200, 3),
            new("Heine", 111333, 219345, -3546, 7100, 6)
        };
    }
}
