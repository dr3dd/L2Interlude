using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class Rizraell : Citizen
{

    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "catch_the_wind") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Catch the Wind");
            }
            if (MySelf.HaveMemo(talker, "catch_the_wind") != false && MySelf.OwnItemCount(talker, "wind_shard") == 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Catch the Wind (Continue)");
            }
            if (MySelf.HaveMemo(talker, "catch_the_wind") != false && MySelf.OwnItemCount(talker, "wind_shard") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Catch the Wind (Continue)");
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
                    if (_from_choice == false || MySelf.HaveMemo(talker, "catch_the_wind") == false)
                    {
                        await MySelf.SetCurrentQuestID("catch_the_wind");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.GetMemoCount(talker) < 26)
                        {
                            if (talker.Level >= 18)
                            {
                                MySelf.FHTML_SetFileName(ref fhtml0, "rizraell_q0317_03.htm");
                                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 317);
                                await MySelf.ShowFHTML(talker, fhtml0);
                            }
                            else
                            {
                                await MySelf.ShowPage(talker, "rizraell_q0317_02.htm");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "fullquest.htm");
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "catch_the_wind") != false && MySelf.OwnItemCount(talker, "wind_shard") == 0))
                    {
                        await MySelf.SetCurrentQuestID("catch_the_wind");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "rizraell_q0317_05.htm");
                    }
                    break;
                case 2:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "catch_the_wind") != false && MySelf.OwnItemCount(talker, "wind_shard") != 0))
                    {
                        await MySelf.SetCurrentQuestID("catch_the_wind");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "rizraell_q0317_07.htm");
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
        if (quest_id == 317)
        {
            await MySelf.SetCurrentQuestID("catch_the_wind");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
            {
                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                await MySelf.SetMemo(talker, quest_id);
                MySelf.AddLog(1, talker, 317);
                await MySelf.SetFlagJournal(talker, 317, 1);
                await MySelf.SoundEffect(talker, "ItemSound.quest_accept");
                await MySelf.ShowPage(talker, "rizraell_q0317_04.htm");
            }
            return;
        }
        await base.QuestAccepted(quest_id, talker);
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == 317)
        {
            await MySelf.SetCurrentQuestID("catch_the_wind");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        if (ask == 317)
        {
            if (reply == 2)
            {
                if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                {
                    talker.quest_last_reward_time = MySelf.GetCurrentTick();
                    if (MySelf.OwnItemCount(talker, "wind_shard") > 0)
                    {
                        if (MySelf.OwnItemCount(talker, "wind_shard") >= 10)
                        {
                            await MySelf.GiveItem1(talker, "adena", (2988 + (40 * MySelf.OwnItemCount(talker, 1078))));
                        }
                        else
                        {
                            await MySelf.GiveItem1(talker, "adena", (40 * MySelf.OwnItemCount(talker, 1078)));
                        }
                    }
                    //?? i0 = MySelf.OwnItemCount(talker, "wind_shard");
                    await MySelf.DeleteItem1(talker, "wind_shard", MySelf.OwnItemCount(talker, 1078));
                    await MySelf.RemoveMemo(talker, "catch_the_wind");
                    MySelf.AddLog(2, talker, 317);
                    await MySelf.SoundEffect(talker, "ItemSound.quest_finish");
                    await MySelf.ShowPage(talker, "rizraell_q0317_08.htm");
                }
            }
            else if (reply == 3)
            {
                if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                {
                    talker.quest_last_reward_time = MySelf.GetCurrentTick();
                    if (MySelf.OwnItemCount(talker, "wind_shard") > 0)
                    {
                        if (MySelf.OwnItemCount(talker, "wind_shard") >= 10)
                        {
                            await MySelf.GiveItem1(talker, "adena", (2988 + (40 * MySelf.OwnItemCount(talker, 1078))));
                        }
                        else
                        {
                            await MySelf.GiveItem1(talker, "adena", (40 * MySelf.OwnItemCount(talker, 1078)));
                        }
                    }
                    //?? i0 = MySelf.OwnItemCount(talker, "wind_shard");
                    await MySelf.DeleteItem1(talker, "wind_shard", MySelf.OwnItemCount(talker, 1078));
                    await MySelf.ShowPage(talker, "rizraell_q0317_09.htm");
                    MySelf.AddLog(3, talker, 317);
                }
            }
        }
        await base.MenuSelected(talker, ask, reply, fhtml0);
    }
}