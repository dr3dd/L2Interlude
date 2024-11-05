using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperPontina : InstantTeleporter
{
    public override async Task TeleportRequested(Talker talker)
    {
        if (Gg.Rand(100) < 50)
        {
            await MySelf.InstantTeleport(talker, 17252, 114121, -3439);
        }
        else
        {
            await MySelf.InstantTeleport(talker, 17253, 114232, -3439);
        }
    }
}