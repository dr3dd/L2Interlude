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
        
        if (defaultNpc is NewbieGuide newbieGuide)
        {
            newbieGuide.FnHighLevel = npcAiData.FnHighLevel;
            newbieGuide.FnRaceMisMatch = npcAiData.FnRaceMisMatch;
            newbieGuide.FnGuideF05 = npcAiData.FnGuideF05;
            newbieGuide.FnGuideF10 = npcAiData.FnGuideF10;
            newbieGuide.FnGuideF15 = npcAiData.FnGuideF15;
            newbieGuide.FnGuideF20 = npcAiData.FnGuideF20;
            newbieGuide.FnGuideM07 = npcAiData.FnGuideM07;
            newbieGuide.FnGuideM14 = npcAiData.FnGuideM14;
            newbieGuide.FnGuideM20 = npcAiData.FnGuideM20;
        }

        defaultNpc.MoveAroundSocial = npcAiData.MoveAroundSocial;
        defaultNpc.MoveAroundSocial1 = npcAiData.MoveAroundSocial1;
        defaultNpc.MoveAroundSocial2 = npcAiData.MoveAroundSocial2;
    }
}