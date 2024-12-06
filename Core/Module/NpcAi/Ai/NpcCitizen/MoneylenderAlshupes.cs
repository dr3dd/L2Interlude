using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class MoneylenderAlshupes : Citizen
{
    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "collectors_dream") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Collector's Dream");
            }
            if (MySelf.HaveMemo(talker, "collectors_dream") == true)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Collector's Dream (Continue)");
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
                    if (_from_choice == false || MySelf.HaveMemo(talker, "collectors_dream") == false)
                    {
                        await MySelf.SetCurrentQuestID("collectors_dream");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.GetMemoCount(talker) < 26)
                        {
                            if (talker.Level >= 15)
                            {
                                MySelf.FHTML_SetFileName(ref fhtml0, "moneylender_alshupes_q0261_02.htm");
                                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 261);
                                await MySelf.ShowFHTML(talker, fhtml0);
                            }
                            else
                            {
                                await MySelf.ShowPage(talker, "moneylender_alshupes_q0261_01.htm");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "fullquest.htm");
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || MySelf.HaveMemo(talker, "collectors_dream") == true)
                    {
                        await MySelf.SetCurrentQuestID("collectors_dream");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.OwnItemCount(talker, "giant_spider_leg") >= 8)
                        {
                            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                            {
                                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                                await MySelf.GiveItem1(talker, "adena", 1000);
                                MySelf.IncrementParam(talker, 0, 2000);
                                await MySelf.DeleteItem1(talker, "giant_spider_leg", MySelf.OwnItemCount(talker, 1087));
                                await MySelf.ShowPage(talker, "moneylender_alshupes_q0261_05.htm");
                                await MySelf.RemoveMemo(talker, "collectors_dream");
                                MySelf.AddLog(2, talker, 261);
                                await MySelf.SoundEffect(talker, "ItemSound.quest_finish");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "moneylender_alshupes_q0261_04.htm");
                        }
                    }
                    break;
                default:
                    return;
            }
            return;
        }
        await base.TalkSelected(fhtml0, talker, _from_choice, _code, _choiceN);

    }


    public override async Task QuestAccepted(int quest_id, Talker talker)
    {
        if (quest_id == 261)
        {
            await MySelf.SetCurrentQuestID("collectors_dream");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
            {
                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                await MySelf.SetMemo(talker, quest_id);
                MySelf.AddLog(1, talker, 261);
                await MySelf.SoundEffect(talker, "ItemSound.quest_accept");
                await MySelf.SetFlagJournal(talker, 261, 1);
                await MySelf.ShowPage(talker, "moneylender_alshupes_q0261_03.htm");
            }
            return;
        }
        await base.QuestAccepted(quest_id, talker);
    }

}