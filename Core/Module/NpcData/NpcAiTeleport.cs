using System.Threading.Tasks;
using Core.Module.NpcAi;
using Core.Module.Player;

namespace Core.Module.NpcData;

public class NpcAiTeleport
{
    private readonly NpcAi _npcAi;
    public NpcAiTeleport(NpcAi npcAi)
    {
        _npcAi = npcAi;
    }
    
    public async Task TeleportRequested(PlayerInstance playerInstance)
    {
        var talker = new Talker(playerInstance);
        await _npcAi.GetDefaultNpc().TeleportRequested(talker);
    }
}