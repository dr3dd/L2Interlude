using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperCapellini : InstantTeleporter
{
    public override async Task TeleportRequested(Talker talker)
    {
        if (Gg.Rand(100) < 50)
        {
            await MySelf.InstantTeleport(talker, 17792, 107760, -11849);
        }
        else
        {
            await MySelf.InstantTeleport(talker, 17648, 107760, -11849);
        }
    }
}