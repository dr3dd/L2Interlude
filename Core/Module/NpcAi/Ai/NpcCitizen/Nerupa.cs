using Core.Enums;
using MySqlX.XDevAPI;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class Nerupa : Citizen
{
    public override async Task Talked(Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (_from_choice == false)
        {
            if (MySelf.OwnItemCount(talker, "leaf_of_mothertree") > 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Elf's Tutorial");
            }
            if (MySelf.OwnItemCount(talker, "leaf_of_mothertree") == 0 && MySelf.GetMemoStateEx(talker, 255, "letters_of_love1") > 3)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Elf's Tutorial");
            }
            if (MySelf.OwnItemCount(talker, "leaf_of_mothertree") == 0 && MySelf.GetMemoStateEx(talker, 255, "letters_of_love1") <= 3)
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Elf's Tutorial");
            }
            if (_choiceN > 1)
            {
                await MySelf.ShowChoicePage(talker, 0);
                return;
            }
        }
        if (_from_choice || _choiceN == 1)
        {
            switch (_code)
            {
                case 0:
                    if (_from_choice == false || MySelf.OwnItemCount(talker, "leaf_of_mothertree") > 0)
                    {
                        await MySelf.SetCurrentQuestID("elf_tutorial");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "nerupa001.htm");
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.OwnItemCount(talker, "leaf_of_mothertree") == 0 && MySelf.GetMemoStateEx(talker, 255, "letters_of_love1") > 3))
                    {
                        await MySelf.SetCurrentQuestID("elf_tutorial");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "nerupa004.htm");
                    }
                    break;

                case 2:
                    if (_from_choice == false || (MySelf.OwnItemCount(talker, "leaf_of_mothertree") == 0 && MySelf.GetMemoStateEx(talker, 255, "letters_of_love1") <= 3))
                    {
                        await MySelf.SetCurrentQuestID("elf_tutorial");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "nerupa003.htm");
                    }
                    break;
                default:
                    return;
            }
            return;
        }
        await base.Talked(talker, _from_choice, _code, _choiceN);
    }
    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {

        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "nerupas_favor") == false && MySelf.GetOneTimeQuestFlag(talker, "nerupas_favor") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Nerupa's Request");
            }
            if (MySelf.HaveMemo(talker, "nerupas_favor") == false && MySelf.GetOneTimeQuestFlag(talker, "nerupas_favor") == true)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Nerupa's Request (Complete)");
            }
            if (MySelf.HaveMemo(talker, "nerupas_favor") != false && (MySelf.OwnItemCount(talker, "silvery_spidersilk") != 0 || MySelf.OwnItemCount(talker, "unos_receipt") != 0 || MySelf.OwnItemCount(talker, "cels_ticket") != 0))
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Nerupa's Request (Continue)");
            }
            if (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0 && MySelf.GetOneTimeQuestFlag(talker, "nerupas_favor") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 3;
                MySelf.AddChoice(3, "Nerupa's Request (Continue)");
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
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") == false && MySelf.GetOneTimeQuestFlag(talker, "nerupas_favor") == false))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.GetMemoCount(talker) < 26)
                        {
                            if (talker.Race != 1)
                            {
                                await MySelf.ShowPage(talker, "nerupa_q0311_00.htm");
                            }
                            else if (talker.Level >= 1)
                            {
                                MySelf.FHTML_SetFileName(ref fhtml0, "nerupa_q0311_03.htm");
                                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 160);
                                await MySelf.ShowFHTML(talker, fhtml0);
                            }
                            else
                            {
                                await MySelf.ShowPage(talker, "nerupa_q0311_02.htm");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "fullquest.htm");
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") == false && MySelf.GetOneTimeQuestFlag(talker, "nerupas_favor") == false))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "finishedquest.htm");
                    }
                    break;
                case 2:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") != false && (MySelf.OwnItemCount(talker, "silvery_spidersilk") != 0 || MySelf.OwnItemCount(talker, "unos_receipt") != 0 || MySelf.OwnItemCount(talker, "cels_ticket") != 0)))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "nerupa_q0311_05.htm");
                    }
                    break;
                case 3:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0 && MySelf.GetOneTimeQuestFlag(talker, "nerupas_favor") == false))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            MySelf.DeleteItem1(talker, "nightshade_leaf", MySelf.OwnItemCount(talker, 1029));
                            MySelf.RemoveMemo(talker, "nerupas_favor");
                            MySelf.AddLog(2, talker, 160);
                            await MySelf.SoundEffect(talker, "ItemSound.quest_finish");
                            MySelf.SetOneTimeQuestFlag(talker, "nerupas_favor", true);
                            await MySelf.GiveItem1(talker, "lesser_healing_potion", 5);
                            MySelf.IncrementParam(talker, 0, 1000);
                            await MySelf.ShowPage(talker, "nerupa_q0311_06.htm");
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        await base.TalkSelected(fhtml0, talker, _from_choice, _code, _choiceN);
    }

    public override async Task QuestAccepted(int quest_id, Talker talker)
    {
        if (quest_id == 160)
        {
            await MySelf.SetCurrentQuestID("nerupas_favor");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
            {
                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                MySelf.SetMemo(talker, quest_id);
                MySelf.AddLog(1, talker, 160);
                await MySelf.SoundEffect(talker, "ItemSound.quest_accept");
                if (MySelf.OwnItemCount(talker, "silvery_spidersilk") == 0)
                {
                    await MySelf.GiveItem1(talker, "silvery_spidersilk", 1);
                }
                await MySelf.ShowPage(talker, "nerupa_q0311_04.htm");
                MySelf.SetFlagJournal(talker, 160, 1);
            }
            return;
        }
        await base.QuestAccepted(quest_id, talker);
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == 203)
        {
            await MySelf.SetCurrentQuestID("elf_tutorial");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        else if (ask == 420)
        {
            if (reply == 31 && MySelf.OwnItemCount(talker, "leaf_of_mothertree") > 0)
            {
                if (MySelf.IsInCategory(0, talker.Occupation) && MySelf.OwnItemCount(talker, "soulshot_none_for_rookie") <= 200)
                {
                    MySelf.VoiceEffect(talker, "tutorial_voice_026", 1000);
                    await MySelf.GiveItem1(talker, "soulshot_none_for_rookie", 200);
                    MySelf.IncrementParam(talker, ParameterType.SP, 50);
                }
                if (MySelf.IsInCategory(1, talker.Occupation) && MySelf.OwnItemCount(talker, "soulshot_none_for_rookie") <= 200 && MySelf.OwnItemCount(talker, "spiritshot_none_for_rookie") <= 100)
                {
                    MySelf.VoiceEffect(talker, "tutorial_voice_027", 1000);
                    await MySelf.GiveItem1(talker, "spiritshot_none_for_rookie", 100);
                    MySelf.IncrementParam(talker, ParameterType.SP, 50);
                }
                await MySelf.ShowPage(talker, "nerupa002.htm");
                MySelf.DeleteItem1(talker, "leaf_of_mothertree", 1);
                MySelf.AddTimerEx((MySelf.GetIndexFromCreature(talker) + 1000000), (1000 * 60));
                await MySelf.ShowRadar(talker, 45475, 48359, -3060, RadarPositionType.BOTH);
                if (MySelf.GetMemoStateEx(talker, 255, "letters_of_love1") <= 3)
                {
                    MySelf.SetMemoStateEx(talker, 255, "letters_of_love1", 4);
                }
            }
        }
        else
        {
            await base.MenuSelected(talker, ask, reply, fhtml0);
        }

    }
}