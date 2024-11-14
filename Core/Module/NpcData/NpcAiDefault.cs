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
            if (fnHi != null)
            {
                citizen.FnHi = fnHi;
            }
        }
        if (defaultNpc is Teleporter teleporter)
        {
            if (fnHi != null)
            {
                teleporter.FnHi = fnHi;
            }
            teleporter.FnNobless = fnNobless;
            teleporter.FnNoNobless = fnNoNobless;
            teleporter.FnNoNoblessItem = fnNoNoblessItem;
            teleporter.FnYouAreChaotic = fnYouAreChaotic;
        }
        if (defaultNpc is Guard guard)
        {
            if (fnHi != null)
            {
                guard.FnHi = fnHi;
            }
        }

        if (defaultNpc is Doorkeeper doorkeeper)
        {
            doorkeeper.DoorName1 = npcAiData.DoorName1;
            doorkeeper.DoorName2 = npcAiData.DoorName2;
            doorkeeper.PosX01 = npcAiData.PosX01;
            doorkeeper.PosY01 = npcAiData.PosY01;
            doorkeeper.PosZ01 = npcAiData.PosZ01;
            doorkeeper.PosX02 = npcAiData.PosX02;
            doorkeeper.PosY02 = npcAiData.PosY02;
            doorkeeper.PosZ02 = npcAiData.PosZ02;
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