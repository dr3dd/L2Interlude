using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class Thalya : Citizen
{
    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "fruit_of_the_mothertree") == true)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Fruit of the Mothertree (Continue)");
            }
            if (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "appetizing_apple") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Path to an Elven Wizard (Continue)");
            }
            if (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "sap_of_world_tree") != 0 && MySelf.OwnItemCount(talker, "gold_leaves") < 5)
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Path to an Elven Wizard (Continue)");
            }
            if (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "sap_of_world_tree") != 0 && MySelf.OwnItemCount(talker, "gold_leaves") >= 5)
            {
                _choiceN = (_choiceN + 1);
                _code = 3;
                MySelf.AddChoice(3, "Path to an Elven Wizard (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "hierarchs_letter") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 4;
                MySelf.AddChoice(4, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "grail_diagram") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 5;
                MySelf.AddChoice(5, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "pushkins_list") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 6;
                MySelf.AddChoice(6, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "pure_mithril_cup") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 7;
                MySelf.AddChoice(7, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "thalyas_letter1") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 8;
                MySelf.AddChoice(8, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "arkenias_contract") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 9;
                MySelf.AddChoice(9, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "stardust") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 10;
                MySelf.AddChoice(10, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "thalyas_instructions") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 11;
                MySelf.AddChoice(11, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "thalyas_letter2") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 12;
                MySelf.AddChoice(12, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "isaels_instructions") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 13;
                MySelf.AddChoice(13, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "talins_spear") >= 1 && MySelf.OwnItemCount(talker, "isaels_letter") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 14;
                MySelf.AddChoice(14, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "talins_spear") >= 1 && MySelf.OwnItemCount(talker, "grail_of_purity") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 15;
                MySelf.AddChoice(15, "Testimony of Life (Continue)");
            }
            if (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "tears_of_unicorn") >= 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 16;
                MySelf.AddChoice(16, "Testimony of Life (Continue)");
            }
            if ((MySelf.HaveMemo(talker, "testimony_of_life") == true && (MySelf.OwnItemCount(talker, "camomile_charm") >= 1 || MySelf.OwnItemCount(talker, "water_of_life") >= 1)) && MySelf.OwnItemCount(talker, "moonflower_charm") == 1)
            {
                _choiceN = (_choiceN + 1);
                _code = 17;
                MySelf.AddChoice(17, "Testimony of Life (Continue)");
            }
            if (_choiceN > 1)
            {
                await MySelf.ShowChoicePage(talker, 1);
                return;
            }
        }
        if (_from_choice || _choiceN == 1)
        {
            switch(_code) {
                case 0:
                    if (_from_choice == false || MySelf.HaveMemo(talker, "fruit_of_the_mothertree") == true)
                    {
                        await MySelf.SetCurrentQuestID("fruit_of_the_mothertree");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.OwnItemCount(talker, "andellrias_letter") == 1)
                        {
                            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                            {
                                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                                await MySelf.ShowPage(talker, "thalya_q0312_01.htm");
                                await MySelf.GiveItem1(talker, "mothertree_fruit", 1);
                                await MySelf.DeleteItem1(talker, "andellrias_letter", 1);
                                await MySelf.SetFlagJournal(talker, 161, 2);
                            }
                        }
                        else if (MySelf.OwnItemCount(talker, "mothertree_fruit") == 1)
                        {
                            await MySelf.ShowPage(talker, "thalya_q0312_02.htm");
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "appetizing_apple") != 0))
                    {
                        await MySelf.SetCurrentQuestID("path_to_an_elven_wizard");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0408_01.htm");
                    }
                    break;
                case 2:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "sap_of_world_tree") != 0 && MySelf.OwnItemCount(talker, "gold_leaves") < 5))
                    {
                        await MySelf.SetCurrentQuestID("path_to_an_elven_wizard");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0408_03.htm");
                    }
                    break;
                case 3:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "sap_of_world_tree") != 0 && MySelf.OwnItemCount(talker, "gold_leaves") >= 5))
                    {
                        await MySelf.SetCurrentQuestID("path_to_an_elven_wizard");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.DeleteItem1(talker, "gold_leaves", MySelf.OwnItemCount(talker, 1223));
                            await MySelf.DeleteItem1(talker, "sap_of_world_tree", MySelf.OwnItemCount(talker, 1273));
                            if (MySelf.OwnItemCount(talker, "pure_aquamarine") == 0)
                            {
                                await MySelf.GiveItem1(talker, "pure_aquamarine", 1);
                            }
                            await MySelf.ShowPage(talker, "thalya_q0408_04.htm");
                        }
                    }
                    break;
                case 4:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "hierarchs_letter") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_01.htm");
                    }
                    break;
                case 5:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "grail_diagram") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_04.htm");
                    }
                    break;
                case 6:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "pushkins_list") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_05.htm");
                    }
                    break;
                case 7:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "pure_mithril_cup") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.ShowPage(talker, "thalya_q0218_06.htm");
                            await MySelf.DeleteItem1(talker, "pure_mithril_cup", 1);
                            await MySelf.GiveItem1(talker, "thalyas_letter1", 1);
                            await MySelf.SetFlagJournal(talker, 218, 7);
                        }
                    }
                    break;
                case 8:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "thalyas_letter1") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_07.htm");
                    }
                    break;
                case 9:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "arkenias_contract") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_08.htm");
                    }
                    break;
                case 10:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "stardust") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_09.htm");
                    }
                    break;
                case 11:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "thalyas_instructions") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (talker.Level < 38)
                        {
                            await MySelf.ShowPage(talker, "thalya_q0218_12.htm");
                        }
                        else if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.ShowPage(talker, "thalya_q0218_13.htm");
                            await MySelf.DeleteItem1(talker, "thalyas_instructions", 1);
                            await MySelf.GiveItem1(talker, "thalyas_letter2", 1);
                            await MySelf.SetFlagJournal(talker, 218, 14);
                        }
                    }
                    break;
                case 12:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "thalyas_letter2") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_14.htm");
                    }
                    break;
                case 13:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "isaels_instructions") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_15.htm");
                    }
                    break;
                case 14:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "talins_spear") >= 1 && MySelf.OwnItemCount(talker, "isaels_letter") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.ShowPage(talker, "thalya_q0218_16.htm");
                            await MySelf.DeleteItem1(talker, "isaels_letter", 1);
                            await MySelf.GiveItem1(talker, "grail_of_purity", 1);
                            await MySelf.SetFlagJournal(talker, 218, 18);
                        }
                    }
                    break;
                case 15:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "talins_spear") >= 1 && MySelf.OwnItemCount(talker, "grail_of_purity") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_17.htm");
                    }
                    break;
                case 16:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "testimony_of_life") == true && MySelf.OwnItemCount(talker, "moonflower_charm") >= 1 && MySelf.OwnItemCount(talker, "tears_of_unicorn") >= 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.ShowPage(talker, "thalya_q0218_18.htm");
                            await MySelf.DeleteItem1(talker, "tears_of_unicorn", 1);
                            await MySelf.GiveItem1(talker, "water_of_life", 1);
                            await MySelf.SetFlagJournal(talker, 218, 20);
                        }
                    }
                    break;
                case 17:
                    if (_from_choice == false || ((MySelf.HaveMemo(talker, "testimony_of_life") == true && (MySelf.OwnItemCount(talker, "camomile_charm") >= 1 || MySelf.OwnItemCount(talker, "water_of_life") >= 1)) && MySelf.OwnItemCount(talker, "moonflower_charm") == 1))
                    {
                        await MySelf.SetCurrentQuestID("testimony_of_life");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "thalya_q0218_19.htm");
                    }
                    break;
                default:
                    return;
            }
            return;
        }
        await base.TalkSelected(fhtml0, talker, _from_choice, _code, _choiceN);
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0) {
        if (ask == 408)
        {
            await MySelf.SetCurrentQuestID("path_to_an_elven_wizard");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        if (ask == 408 && reply == 5)
        {
            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
            {
                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                if (MySelf.HaveMemo(talker, "path_to_an_elven_wizard") != false && MySelf.OwnItemCount(talker, "appetizing_apple") != 0)
                {
                    await MySelf.DeleteItem1(talker, "appetizing_apple", MySelf.OwnItemCount(talker, 1222));
                    if (MySelf.OwnItemCount(talker, "sap_of_world_tree") == 0)
                    {
                        await MySelf.GiveItem1(talker, "sap_of_world_tree", 1);
                    }
                    await MySelf.ShowPage(talker, "thalya_q0408_02.htm");
                }
            }
        }
        if (ask == 218)
        {
            await MySelf.SetCurrentQuestID("testimony_of_life");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        if (ask == 218)
        {
            if (reply == 1)
            {
                await MySelf.ShowPage(talker, "thalya_q0218_02.htm");
            }
            if (reply == 2)
            {
                if (MySelf.OwnItemCount(talker, "hierarchs_letter") >= 1)
                {
                    if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                    {
                        talker.quest_last_reward_time = MySelf.GetCurrentTick();
                        await MySelf.ShowPage(talker, "thalya_q0218_03.htm");
                        await MySelf.DeleteItem1(talker, "hierarchs_letter", 1);
                        await MySelf.GiveItem1(talker, "grail_diagram", 1);
                        await MySelf.SetFlagJournal(talker, 218, 3);
                    }
                }
            }
            if (reply == 3)
            {
                if (talker.Level < 38)
                {
                    if (MySelf.OwnItemCount(talker, "stardust") >= 1)
                    {
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.ShowPage(talker, "thalya_q0218_10.htm");
                            await MySelf.DeleteItem1(talker, "stardust", 1);
                            await MySelf.GiveItem1(talker, "thalyas_instructions", 1);
                            await MySelf.SetFlagJournal(talker, 218, 13);
                        }
                    }
                }
                else if (MySelf.OwnItemCount(talker, "stardust") >= 1)
                {
                    if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                    {
                        talker.quest_last_reward_time = MySelf.GetCurrentTick();
                        await MySelf.ShowPage(talker, "thalya_q0218_11.htm");
                        await MySelf.DeleteItem1(talker, "stardust", 1);
                        await MySelf.GiveItem1(talker, "thalyas_letter2", 1);
                        await MySelf.SetFlagJournal(talker, 218, 14);
                    }
                }
            }
        }
        await base.MenuSelected(talker, ask, reply, fhtml0);
    }
}