using Core.Module.NpcAi.Ai;
using Core.Module.NpcAi.Ai.NpcType;

namespace Core.Module.NpcData;

public class NpcAiDefault
{
    public static void SetDefaultAiParams(DefaultNpc defaultNpc, NpcAiData npcAiData)
    {
        var fnHi = npcAiData.FnHi;
        var fnNobless = npcAiData.FnNobless;
        var fnNoNobless = npcAiData.FnNoNobless;
        var fnNoNoblessItem = npcAiData.FnNoNoblessItem;
        var fnYouAreChaotic = npcAiData.FnYouAreChaotic;
        
        if (defaultNpc is Citizen citizen)
        {
            citizen.FnHi = fnHi;
        }
        if (defaultNpc is Teleporter teleporter)
        {
            teleporter.FnHi = fnHi;
        }
        if (defaultNpc is Guard guard)
        {
            guard.FnHi = fnHi;
        }

        defaultNpc.MoveAroundSocial = npcAiData.MoveAroundSocial;
        defaultNpc.MoveAroundSocial1 = npcAiData.MoveAroundSocial1;
        defaultNpc.MoveAroundSocial2 = npcAiData.MoveAroundSocial2;
    }
}