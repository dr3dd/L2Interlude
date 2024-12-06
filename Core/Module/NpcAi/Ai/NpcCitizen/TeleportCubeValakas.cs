using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class TeleportCubeValakas : AiBoss07TeleportCube
{
    public override async Task TeleportRequested(Talker talker)
    {
        int i1 = (150037 + Gg.Rand(500));
        int i2 = (-57720 + Gg.Rand(500));
        await MySelf.InstantTeleport(talker, i1, i2, -2976);
    }
}