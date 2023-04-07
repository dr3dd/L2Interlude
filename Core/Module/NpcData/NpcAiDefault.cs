using Core.Module.NpcAi.Ai;

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

        if (defaultNpc is Doorkeeper doorkeeper)
        {
            doorkeeper.DoorName1 = npcAiData.DoorName1;
            doorkeeper.DoorName2 = npcAiData.DoorName2;
        }

        defaultNpc.MoveAroundSocial = npcAiData.MoveAroundSocial;
        defaultNpc.MoveAroundSocial1 = npcAiData.MoveAroundSocial1;
        defaultNpc.MoveAroundSocial2 = npcAiData.MoveAroundSocial2;
    }
}