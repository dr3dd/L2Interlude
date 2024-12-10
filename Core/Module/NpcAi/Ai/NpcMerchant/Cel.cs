using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.NpcAi.Ai.NpcCitizen;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Cel : MerchantForNewbie
{
    /// <summary>
    /// Accessory (Magic Rings)
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new("necklace_of_magic", 15, 0.000000, 0),  // Necklace of Magic
		new("necklace_of_knowledge", 15, 0.000000, 0),  // Necklace of Knowledge
		new("necklace_of_anguish", 15, 0.000000, 0),  // Necklace of Anguish
		new("necklace_of_wisdom", 15, 0.000000, 0),  // Necklace of Wisdom
		new("apprentice_s_earing", 15, 0.000000, 0),  // Apprentice's Earring
		new("mage_earing", 15, 0.000000, 0),  // Mystic's Earring
		new("earing_of_strength", 15, 0.000000, 0),  // Earring of Strength
		new("earing_of_wisdom", 15, 0.000000, 0),  // Earring of Wisdom
		new("cat_seye_earing", 15, 0.000000, 0),  // Cat's Eye Earring
		new("magic_ring", 15, 0.000000, 0),  // Magic Ring
		new("ring_of_knowledge", 15, 0.000000, 0),  // Ring of Knowledge
		new("ring_of_anguish", 15, 0.000000, 0),  // Ring of Anguish
		new("ring_of_wisdom", 15, 0.000000, 0),  // Ring of Wisdom
    };

    /// <summary>
    /// SpellBooks, Amulets
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new("sb_adv_defence_power1", 15, 0.000000, 0),  // Spellbook: Defense Aura
		new("sb_advanced_attack_power1", 15, 0.000000, 0),  // Spellbook: Attack Aura
		new("sb_might1", 15, 0.000000, 0),  // Spellbook: Might
		new("sb_battle_heal1", 15, 0.000000, 0),  // Spellbook: Battle Heal
		new("sb_vampiric_touch1", 15, 0.000000, 0),  // Spellbook: Vampiric Touch
		new("sb_ice_bolt1", 15, 0.000000, 0),  // Spellbook: Ice Bolt
		new("sb_heal1", 15, 0.000000, 0),  // Spellbook: Flame Strike
		new("sb_group_heal1", 15, 0.000000, 0),  // Spellbook: Group Heal
		new("sb_shield1", 15, 0.000000, 0),  // Spellbook: Shield
		new("sb_breeze1", 15, 0.000000, 0),  // Spellbook: Wind Shackle
		new("sb_wind_walk1", 15, 0.000000, 0),  // Spellbook: Wind Walk
		new("sb_curse:weakness", 15, 0.000000, 0),  // Spellbook: Curse: Weakness
		new("sb_curse:poison1", 15, 0.000000, 0),  // Spellbook: Curse: Poison
		new("sb_cure_poison1", 15, 0.000000, 0),  // Spellbook: Cure Poison
		new("sb_flame_strike1", 15, 0.000000, 0),  // Spellbook: Flame Strike
		new("sb_drain_energy1", 15, 0.000000, 0),  // Spellbook: Drain Health
		new("sb_elemental_heal1", 15, 0.000000, 0),  // Spellbook: Elemental Heal
		new("sb_disrupt_undead1", 15, 0.000000, 0),  // Spellbook: Disrupt Undead
		new("sb_resurrection1", 15, 0.000000, 0),  // Spellbook: Resurrection
		new("sb_blaze1", 15, 0.000000, 0),  // Spellbook: Blaze
		new("sb_summon_shadow1", 15, 0.000000, 0),  // Spellbook: Summon Shadow
		new("sb_summon_silhouette1", 15, 0.000000, 0),  // Spellbook: Summon Silhouette
		new("sb_summon_unicorn_boxer1", 15, 0.000000, 0),  // Spellbook: Summon Boxer the Unicorn
		new("sb_summon_blackcat1", 15, 0.000000, 0),  // Spellbook: Summon Kat the Cat
		new("sb_servitor_heal1", 15, 0.000000, 0),  // Spellbook: Servitor Heal
		new("sb_aqua_swirl1", 15, 0.000000, 0),  // Spellbook: Aqua Swirl
		new("sb_arcane_acumen1", 15, 0.000000, 0),  // Spellbook: Acumen
		new("sb_energy_bolt1", 15, 0.000000, 0),  // Spellbook: Energy Bolt
		new("sb_aura_burn1", 15, 0.000000, 0),  // Spellbook: Aura Burn
		new("sb_charm11", 15, 0.000000, 0),  // Spellbook: Charm
		new("sb_concentration1", 15, 0.000000, 0),  // Spellbook: Concentration
		new("sb_water_breathing", 15, 0.000000, 0),  // Spellbook: Kiss of Eva
		new("sb_twister1", 15, 0.000000, 0),  // Spellbook: Twister
		new("sb_poison1", 15, 0.000000, 0),  // Spellbook: Poison
		new("sb_poison_recovery1", 15, 0.000000, 0),  // Spellbook: Poison Recovery
    };

    public override async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
	{
        if (_from_choice == false)
        {
            if (MySelf.HaveMemo(talker, "blood_fiend") == false && MySelf.GetOneTimeQuestFlag(talker, "blood_fiend") == false)
            {
                _choiceN = (_choiceN + 1);
                _code = 0;
                MySelf.AddChoice(0, "Blood Fiend");
            }
            if (MySelf.HaveMemo(talker, "blood_fiend") == false && MySelf.GetOneTimeQuestFlag(talker, "blood_fiend") == true)
            {
                _choiceN = (_choiceN + 1);
                _code = 1;
                MySelf.AddChoice(1, "Blood Fiend (Complete)");
            }
            if (MySelf.HaveMemo(talker, "blood_fiend"))
            {
                _choiceN = (_choiceN + 1);
                _code = 2;
                MySelf.AddChoice(2, "Blood Fiend (Continue)");
            }
            if (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "unos_receipt") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 3;
                MySelf.AddChoice(3, "Nerupa's Request (Continue)");
            }
            if (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "cels_ticket") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 4;
                MySelf.AddChoice(4, "Nerupa's Request (Continue)");
            }
            if (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0)
            {
                _choiceN = (_choiceN + 1);
                _code = 5;
                MySelf.AddChoice(5, "Nerupa's Request (Continue)");
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
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "blood_fiend") == false && MySelf.GetOneTimeQuestFlag(talker, "blood_fiend") == false))
                    {
                        await MySelf.SetCurrentQuestID("blood_fiend");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.GetMemoCount(talker) < 26)
                        {
                            if (talker.Race != 1 && talker.Race != 3 && talker.Race != 4 && talker.Race != 0)
                            {
                                await MySelf.ShowPage(talker, "cel_q0318_00.htm");
                            }
                            else if (talker.Level >= 21)
                            {
                                MySelf.FHTML_SetFileName(ref fhtml0, "cel_q0318_03.htm");
                                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 164);
                                await MySelf.ShowFHTML(talker, fhtml0);
                            }
                            else
                            {
                                await MySelf.ShowPage(talker, "cel_q0318_02.htm");
                            }
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "fullquest.htm");
                        }
                    }
                    break;
                case 1:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "blood_fiend") == false && MySelf.GetOneTimeQuestFlag(talker, "blood_fiend") == true))
                    {
                        await MySelf.SetCurrentQuestID("blood_fiend");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "finishedquest.htm");
                    }
                    break;
                case 2:
                    if (_from_choice == false || MySelf.HaveMemo(talker, "blood_fiend"))
                    {
                        await MySelf.SetCurrentQuestID("blood_fiend");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        if (MySelf.OwnItemCount(talker, "kirunak_skull") < 1)
                        {
                            await MySelf.ShowPage(talker, "cel_q0318_05.htm");
                        }
                        else if (MySelf.OwnItemCount(talker, "kirunak_skull") >= 1 && MySelf.GetOneTimeQuestFlag(talker, "blood_fiend") == false)
                        {
                            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
                            {
                                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                                await MySelf.ShowPage(talker, "cel_q0318_06.htm");
                                await MySelf.GiveItem1(talker, "adena", 42130);
                                await MySelf.DeleteItem1(talker, "kirunak_skull", 1);
                                await MySelf.RemoveMemo(talker, "blood_fiend");
                                MySelf.AddLog(2, talker, 164);
                                await MySelf.SoundEffect(talker, "ItemSound.quest_finish");
                                MySelf.SetOneTimeQuestFlag(talker, "blood_fiend", true);
                            }
                        }
                    }
                    break;
                case 3:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "unos_receipt") != 0))
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
                            await MySelf.DeleteItem1(talker, "unos_receipt", MySelf.OwnItemCount(talker, 1027));
                            if (MySelf.OwnItemCount(talker, "cels_ticket") == 0)
                            {
                                await MySelf.GiveItem1(talker, "cels_ticket", 1);
                            }
                            await MySelf.ShowPage(talker, "cel_q0311_01.htm");
                            await MySelf.SetFlagJournal(talker, 160, 3);
                        }
                    }
                    break;
                case 4:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "cels_ticket") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_request");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "cel_q0311_02.htm");
                    }
                    break;
                case 5:
                    if (_from_choice == false || (MySelf.HaveMemo(talker, "nerupas_request") != false && MySelf.OwnItemCount(talker, "nightshade_leaf") != 0))
                    {
                        await MySelf.SetCurrentQuestID("nerupas_request");
                        if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
                        {
                            await MySelf.ShowSystemMessage(talker, 1118);
                            return;
                        }
                        await MySelf.ShowPage(talker, "cel_q0311_03.htm");
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
        if (quest_id == 164)
        {
            await MySelf.SetCurrentQuestID("blood_fiend");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
            if ((MySelf.GetCurrentTick() - talker.quest_last_reward_time) > 1)
            {
                talker.quest_last_reward_time = MySelf.GetCurrentTick();
                await MySelf.ShowPage(talker, "cel_q0318_04.htm");
                await MySelf.SetMemo(talker, "blood_fiend");
                MySelf.AddLog(1, talker, 164);
                await MySelf.SoundEffect(talker, "ItemSound.quest_accept");
                await MySelf.SetFlagJournal(talker, 164, 1);
            }
            return;
        }
        await base.QuestAccepted(quest_id, talker);
    }
}