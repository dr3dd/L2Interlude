using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcWarehouseKeeper;

public class Jud : WarehouseKeeper
{
    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "cels_ticket") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Nerupa's Request (Continue)");
            }
            if (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Nerupa's Request (Continue)");
            }
            if (_choiceN > 1)
            {
                await MySelf.ShowChoicePage(talker, 1);
                return;
            }
        }
        if (_from_choice || _choiceN == 1)
        {
            switch (_code)
            {
                case 0:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "cels_ticket") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_request");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.DeleteItem1(talker, "cels_ticket", MySelf.OwnItemCount(talker, 1028));
                            if (MySelf.OwnItemCount(talker, "nightshade_leaf") == 0)
                            {
                                await MySelf.GiveItem1(talker, "nightshade_leaf", 1);
                                await MySelf.ShowPage(talker, "jud_q0311_01.htm");
                                await MySelf.SetFlagJournal(talker, 160, 4);
                            }
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_request");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "jud_q0311_02.htm");
                    }
                    break;
                default:
                    return;
            }
            return;
        }
        await base.TalkSelected(fhtml0, talker, _from_choice, _code, _choiceN);
    }
}