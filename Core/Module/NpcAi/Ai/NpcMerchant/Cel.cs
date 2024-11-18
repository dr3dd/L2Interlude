using System.Collections.Generic;
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
}