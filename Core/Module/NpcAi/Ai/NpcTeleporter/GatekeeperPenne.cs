using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcTeleporter;

public class GatekeeperPenne : InstantTeleporter
{
    public override async Task TeleportRequested(Talker talker)
    {
        if (Gg.Rand(100) < 50)
        {
            await MySelf.InstantTeleport(talker, 17776, 108288, -9057);
        }
        else
        {
            await MySelf.InstantTeleport(talker, 17664, 108288, -9057);
        }
    }
}