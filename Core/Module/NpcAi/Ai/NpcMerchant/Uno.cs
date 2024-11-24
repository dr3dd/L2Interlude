using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.NpcAi.Ai.NpcCitizen;
using Core.Module.NpcAi.Models;
using MySqlX.XDevAPI;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Uno : MerchantForNewbie
{
    /// <summary>
    /// Warrior Weapon
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(1, 15, 0.000000, 0), // Short Sword
        new(4, 15, 0.000000, 0), // Club
        new(11, 15, 0.000000, 0), // Bone Dagger
        new(13, 15, 0.000000, 0), // Short Bow
        new(3, 15, 0.000000, 0), // Broadsword
        new(152, 15, 0.000000, 0), // Heavy Chisel
        new(12, 15, 0.000000, 0), // Knife
        new(215, 15, 0.000000, 0), // Doom Dagger
        new(14, 15, 0.000000, 0), // Bow
        new(5, 15, 0.000000, 0), // Mace
        new(153, 15, 0.000000, 0), // Sickle
        new(1333, 15, 0.000000, 0), // Brandish
        new(66, 15, 0.000000, 0), // Gladius
        new(67, 15, 0.000000, 0), // Orcish Sword
        new(122, 15, 0.000000, 0), // Handmade Sword
        new(154, 15, 0.000000, 0), // Dwarven Mace
        new(216, 15, 0.000000, 0), // Dirk
        new(271, 15, 0.000000, 0), // Hunting Bow
        new(2, 15, 0.000000, 0), // Long Sword
        new(218, 15, 0.000000, 0), // Throwing Knife
        new(272, 15, 0.000000, 0), // Forest Bow
        new(15, 15, 0.000000, 0), // Short Spear
        new(5284, 15, 0.000000, 0) // Zweihander
    };
    
    /// <summary>
    /// Mystic Weapon
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(7, 15, 0.000000, 0), // Apprentice's Rod
        new(308, 15, 0.000000, 0), // Buffalo's Horn
        new(8, 15, 0.000000, 0), // Willow Staff
        new(99, 15, 0.000000, 0), // Apprentice's Spellbook
        new(9, 15, 0.000000, 0), // Cedar Staff
        new(176, 15, 0.000000, 0), // Journeyman's Staff
        new(310, 15, 0.000000, 0), // Relic of The Saints
    };

    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        if (!_from_choice)
        {
            if (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "silvery_spidersilk") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Nerupa's Request");
            }
            if (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "unos_receipt") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Nerupa's Request");
            }
            if (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Nerupa's Request");
            }
            if (MySelf.HaveMemo(talker, "curse_of_fortress") == false && MySelf.GetOneTimeQuestFlag(talker, "curse_of_fortress") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 3;
                MySelf.AddChoice(3, "Curse of the Underground Fortress");
            }
            if (MySelf.HaveMemo(talker, "curse_of_fortress") == false && MySelf.GetOneTimeQuestFlag(talker, "curse_of_fortress") == true)
            {
                _choiceN = (_choiceN + 1);
                _code = 4;
                MySelf.AddChoice(4, "Curse of the Underground Fortress");
            }
            if (MySelf.HaveMemo(talker, "curse_of_fortress") == true && (MySelf.OwnItemCount(talker, "elf_skull") + MySelf.OwnItemCount(talker, "bone_fragment3")) < 13)
            {
                _choiceN = (_choiceN + 1);
                _code = 5;
                MySelf.AddChoice(5, "Curse of the Underground Fortress");
            }
            if (MySelf.HaveMemo(talker, "curse_of_fortress") == true && (MySelf.OwnItemCount(talker, "elf_skull") + MySelf.OwnItemCount(talker, "bone_fragment3")) >= 13 && MySelf.GetOneTimeQuestFlag(talker, "curse_of_fortress") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 6;
                MySelf.AddChoice(6, "Curse of the Underground Fortress");
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
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "silvery_spidersilk") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        MySelf.DeleteItem1(talker, "silvery_spidersilk", MySelf.OwnItemCount(talker, 1026));
                        if (MySelf.OwnItemCount(talker, "unos_receipt") == 0)
                        {
                            await MySelf.GiveItem1(talker, "unos_receipt", 1);
                        }
                        await MySelf.ShowPage(talker, "uno_q0311_01.htm");
                        MySelf.SetFlagJournal(talker, 160, 2);
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "unos_receipt") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "uno_q0311_02.htm");
                    }
                    break;
                case 2:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_favor") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_favor");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "uno_q0311_03.htm");
                    }
                    break;
                case 3:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "curse_of_fortress") == false && MySelf.GetOneTimeQuestFlag(talker, "curse_of_fortress") == false))
                    {
                        await MySelf.SetCurrentQuestID("curse_of_fortress");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.GetMemoCount(talker) < 26)
                        {
                            if (talker.Race == 2)
                            {
                                await MySelf.ShowPage(talker, "uno_q0314_00.htm");
                            }
                            else if (talker.Level >= 12)
                            {
                                await MySelf.ShowPage(talker, "uno_q0314_02.htm");
                            }
                            else
                            {
                                await MySelf.ShowPage(talker, "uno_q0314_01.htm");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "fullquest.htm");
                        }
                    }
                    break;
                case 4:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "curse_of_fortress") == false && MySelf.GetOneTimeQuestFlag(talker, "curse_of_fortress") == true))
                    {
                        await MySelf.SetCurrentQuestID("curse_of_fortress");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "finishedquest.htm");
                    }
                    break;
                case 5:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "curse_of_fortress") == true && (MySelf.OwnItemCount(talker, "elf_skull") + MySelf.OwnItemCount(talker, "bone_fragment3")) < 13))
                    {
                        await MySelf.SetCurrentQuestID("curse_of_fortress");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "uno_q0314_05.htm");
                    }
                    break;
                case 6:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "curse_of_fortress") == true && (MySelf.OwnItemCount(talker, "elf_skull") + MySelf.OwnItemCount(talker, "bone_fragment3")) >= 13 && MySelf.GetOneTimeQuestFlag(talker, "curse_of_fortress") == false))
                    {
                        await MySelf.SetCurrentQuestID("curse_of_fortress");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                        {
                            talker.quest_last_reward_time = MySelf.GetCurrentTick();
                            await MySelf.ShowPage(talker, "uno_q0314_06.htm");
                            await MySelf.GiveItem1(talker, "bone_shield", 1);
                            await MySelf.GiveItem1(talker, "adena", 24000);
                            MySelf.DeleteItem1(talker, "elf_skull", MySelf.OwnItemCount(talker, 1159));
                            MySelf.DeleteItem1(talker, "bone_fragment3", MySelf.OwnItemCount(talker, 1158));
                            MySelf.RemoveMemo(talker, "curse_of_fortress");
                            MySelf.AddLog(2, talker, 162);
                            await MySelf.SoundEffect(talker, "ItemSound.quest_finish");
                            MySelf.SetOneTimeQuestFlag(talker, "curse_of_fortress", true);
                        }
                    }
                    break;
                default:
                    return;
            }
        }
        await base.TalkSelected(fhtml0, talker, _from_choice, _code, _choiceN);
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == 162)
        {
            await MySelf.SetCurrentQuestID("curse_of_fortress");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        if (ask == 162)
        {
            if (reply == 1)
            {
                MySelf.FHTML_SetFileName(ref fhtml0, "uno_q0314_03.htm");
                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 162);
                await MySelf.ShowFHTML(talker, fhtml0);
            }
        }
        else
        {
            await base.MenuSelected(talker, ask, reply);
        }
    }
}