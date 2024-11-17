using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperMozzarella : InstantTeleporter
{
    public override async Task TeleportRequested(Talker talker)
    {
        if (Gg.Rand(100) < 50)
        {
            await MySelf.InstantTeleport(talker, 17776, 113968, -11671);
        }
        else
        {
            await MySelf.InstantTeleport(talker, 17680, 113968, -11671);
        }
    }
}