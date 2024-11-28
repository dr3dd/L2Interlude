using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class Andellria : Citizen
{
    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "fruit_of_the_mothertree") == false && MySelf.GetOneTimeQuestFlag(talker, "fruit_of_the_mothertree") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Fruit of the Mothertree");
            }
            if (MySelf.HaveMemo(talker, "fruit_of_the_mothertree") == false && MySelf.GetOneTimeQuestFlag(talker, "fruit_of_the_mothertree") == true)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Fruit of the Mothertree (Complete)");
            }
            if (MySelf.HaveMemo(talker, "fruit_of_the_mothertree"))
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Fruit of the Mothertree (Continue)");
            }
            if (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 12)
            {
                _choiceN = (_choiceN + 1);
                _code = 3;
                MySelf.AddChoice(3, "Trial of the Pilgrim (Continue)");
            }
            if (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 13)
            {
                _choiceN = (_choiceN + 1);
                _code = 4;
                MySelf.AddChoice(4, "Trial of the Pilgrim (Continue)");
            }
            if (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 14)
            {
                _choiceN = (_choiceN + 1);
                _code = 5;
                MySelf.AddChoice(5, "Trial of the Pilgrim (Continue)");
            }
            if (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 15 && MySelf.OwnItemCount(talker, "book_of_darkness") > 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 6;
                MySelf.AddChoice(6, "Trial of the Pilgrim (Continue)");
            }
            if (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 16)
            {
                _choiceN = (_choiceN + 1);
                _code = 7;
                MySelf.AddChoice(7, "Trial of the Pilgrim (Continue)");
            }
            if (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 15 && MySelf.OwnItemCount(talker, "book_of_darkness") == 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 8;
                MySelf.AddChoice(8, "Trial of the Pilgrim (Continue)");
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
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "fruit_of_the_mothertree") == false && MySelf.GetOneTimeQuestFlag(talker, "fruit_of_the_mothertree") == false))
                    {
                        await MySelf.SetCurrentQuestID("fruit_of_the_mothertree");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.GetMemoCount(talker) < 26)
                        {
                            if (talker.Race != 1)
                            {
                                await MySelf.ShowPage(talker, "andellria_q0312_00.htm");
                            }
                            else if (talker.Level >= 3)
                            {
                                MySelf.FHTML_SetFileName(ref fhtml0, "andellria_q0312_03.htm");
                                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 161);
                                await MySelf.ShowFHTML(talker, fhtml0);
                            }
                            else
                            {
                                await MySelf.ShowPage(talker, "andellria_q0312_02.htm");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "fullquest.htm");
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "fruit_of_the_mothertree") == false && MySelf.GetOneTimeQuestFlag(talker, "fruit_of_the_mothertree") == true))
                    {
                        await MySelf.SetCurrentQuestID("fruit_of_the_mothertree");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "finishedquest.htm");
                    }
                    break;
                case 2:
                    if (_from_choice == false || MySelf.HaveMemo(talker, "fruit_of_the_mothertree"))
                    {
                        await MySelf.SetCurrentQuestID("fruit_of_the_mothertree");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.OwnItemCount(talker, "andellrias_letter") == 1 && MySelf.OwnItemCount(talker, "mothertree_fruit") == 0)
                        {
                            await MySelf.ShowPage(talker, "andellria_q0312_05.htm");
                        }
                        else if (MySelf.OwnItemCount(talker, "mothertree_fruit") == 1 && MySelf.GetOneTimeQuestFlag(talker, "fruit_of_the_mothertree") == false)
                        {
                            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                            {
                                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                                await MySelf.ShowPage(talker, "andellria_q0312_06.htm");
                                await MySelf.GiveItem1(talker, "adena", 1000);
                                MySelf.IncrementParam(talker, 0, 1000);
                                await MySelf.DeleteItem1(talker, "mothertree_fruit", 1);
                                await MySelf.RemoveMemo(talker, "fruit_of_the_mothertree");
                                MySelf.AddLog(2, talker, 161);
                                await MySelf.SoundEffect(talker, "ItemSound.quest_finish");
                                MySelf.SetOneTimeQuestFlag(talker, "fruit_of_the_mothertree", true);
                            }
                        }
                    }
                    break;
                case 3:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 12))
                    {
                        await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (talker.Level >= 36)
                        {
                            await MySelf.ShowPage(talker, "andellria_q0215_01.htm");
                            await MySelf.SetMemoState(talker, "trial_of_the_pilgrim", 13);
                            await MySelf.SetFlagJournal(talker, 215, 13);
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "andellria_q0215_01a.htm");
                        }
                    }
                    break;
                case 4:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 13))
                    {
                        await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "andellria_q0215_02.htm");
                    }
                    break;
                case 5:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 14))
                    {
                        await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "andellria_q0215_02a.htm");
                    }
                    break;
                case 6:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 15 && MySelf.OwnItemCount(talker, "book_of_darkness") > 0))
                    {
                        await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "andellria_q0215_03.htm");
                    }
                    break;
                case 7:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 16))
                    {
                        await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "andellria_q0215_06.htm");
                    }
                    break;
                case 8:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "trial_of_the_pilgrim") == true && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 15 && MySelf.OwnItemCount(talker, "book_of_darkness") == 0))
                    {
                        await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "andellria_q0215_07.htm");
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
        if (quest_id == 161)
        {
            await MySelf.SetCurrentQuestID("fruit_of_the_mothertree");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
            {
                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                await MySelf.ShowPage(talker, "andellria_q0312_04.htm");
                await MySelf.GiveItem1(talker, "andellrias_letter", 1);
                await MySelf.SetMemo(talker, "fruit_of_the_mothertree");
                MySelf.AddLog(1, talker, 161);
                await MySelf.SoundEffect(talker, "ItemSound.quest_accept");
                await MySelf.SetFlagJournal(talker, 161, 1);
            }
            return;
        }
        await base.QuestAccepted(quest_id, talker);
    }
    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == 215)
        {
            await MySelf.SetCurrentQuestID("trial_of_the_pilgrim");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        if (ask == 215)
        {
            if (reply == 1 && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 15 && MySelf.OwnItemCount(talker, "book_of_darkness") > 0)
            {
                if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                {
                    talker.quest_last_reward_time = MySelf.GetCurrentTick();
                    await MySelf.ShowPage(talker, "andellria_q0215_05.htm");
                    await MySelf.DeleteItem1(talker, "book_of_darkness", MySelf.OwnItemCount(talker, 2731));
                    await MySelf.SetMemoState(talker, "trial_of_the_pilgrim", 16);
                    await MySelf.SetFlagJournal(talker, 215, 16);
                }
            }
            else if (reply == 2 && MySelf.GetMemoState(talker, "trial_of_the_pilgrim") == 15 && MySelf.OwnItemCount(talker, "book_of_darkness") > 0)
            {
                await MySelf.ShowPage(talker, "andellria_q0215_04.htm");
                await MySelf.SetMemoState(talker, "trial_of_the_pilgrim", 16);
                await MySelf.SetFlagJournal(talker, 215, 16);
            }
        }
        await base.MenuSelected(talker, ask, reply, fhtml0);
    }
}