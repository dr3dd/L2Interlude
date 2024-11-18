using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Denkus : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
		// Accessory (Magic Rings)
		new("necklace_of_wisdom", 10, 0.000000, 0),  // Necklace of Wisdom
		new("blue_diamond_necklace", 10, 0.000000, 0),  // Blue Diamond Necklace
		new("necklace_of_devotion", 10, 0.000000, 0),  // Necklace of Devotion
		new("enchanted_necklace", 10, 0.000000, 0),  // Enchanted Necklace
		new("near_forest_necklace", 10, 0.000000, 0),  // Near Forest Necklace
		new("elven_necklace", 10, 0.000000, 0),  // Elven Necklace
		new("cat_seye_earing", 10, 0.000000, 0),  // Cat's Eye Earring
		new("coral_earing", 10, 0.000000, 0),  // Coral Earring
		new("red_cresent_earing", 10, 0.000000, 0),  // Red Crescent Earring
		new("enchanted_earing", 10, 0.000000, 0),  // Enchanted Earring
		new("tiger_seye_earing", 10, 0.000000, 0),  // Tiger's Eye Earring
		new("elven_earing", 10, 0.000000, 0),  // Elven Earring
		new("ring_of_wisdom", 10, 0.000000, 0),  // Ring of Wisdom
		new("blue_coral_ring", 10, 0.000000, 0),  // Blue Coral Ring
		new("ring_of_devotion", 10, 0.000000, 0),  // Ring of Devotion
		new("enchanted_ring", 10, 0.000000, 0),  // Enchanted Ring
		new("black_pearl_ring", 10, 0.000000, 0),  // Black Pearl Ring
		new("elven_ring", 10, 0.000000, 0)  // Elven Ring
	
	};
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
		// SpellBooks,  Amulets
		new("sb_adv_defence_power1", 10, 0.000000, 0),  // Spellbook: Defense Aura
		new("sb_advanced_attack_power1", 10, 0.000000, 0),  // Spellbook: Attack Aura
		new("sb_might1", 10, 0.000000, 0),  // Spellbook: Might
		new("sb_battle_heal1", 10, 0.000000, 0),  // Spellbook: Battle Heal
		new("sb_vampiric_touch1", 10, 0.000000, 0),  // Spellbook: Vampiric Touch
		new("sb_ice_bolt1", 10, 0.000000, 0),  // Spellbook: Ice Bolt
		new("sb_heal1", 10, 0.000000, 0),  // Spellbook: Flame Strike
		new("sb_group_heal1", 10, 0.000000, 0),  // Spellbook: Group Heal
		new("sb_shield1", 10, 0.000000, 0),  // Spellbook: Shield
		new("sb_breeze1", 10, 0.000000, 0),  // Spellbook: Wind Shackle
		new("sb_wind_walk1", 10, 0.000000, 0),  // Spellbook: Wind Walk
		new("sb_curse:weakness", 10, 0.000000, 0),  // Spellbook: Curse: Weakness
		new("sb_curse:poison1", 10, 0.000000, 0),  // Spellbook: Curse: Poison
		new("sb_cure_poison1", 10, 0.000000, 0),  // Spellbook: Cure Poison
		new("sb_flame_strike1", 10, 0.000000, 0),  // Spellbook: Flame Strike
		new("sb_drain_energy1", 10, 0.000000, 0),  // Spellbook: Drain Health
		new("sb_elemental_heal1", 10, 0.000000, 0),  // Spellbook: Elemental Heal
		new("sb_disrupt_undead1", 10, 0.000000, 0),  // Spellbook: Disrupt Undead
		new("sb_resurrection1", 10, 0.000000, 0),  // Spellbook: Resurrection
		new("sb_blaze1", 10, 0.000000, 0),  // Spellbook: Blaze
		new("sb_summon_shadow1", 10, 0.000000, 0),  // Spellbook: Summon Shadow
		new("sb_summon_silhouette1", 10, 0.000000, 0),  // Spellbook: Summon Silhouette
		new("sb_summon_unicorn_boxer1", 10, 0.000000, 0),  // Spellbook: Summon Boxer the Unicorn
		new("sb_summon_blackcat1", 10, 0.000000, 0),  // Spellbook: Summon Kat the Cat
		new("sb_servitor_heal1", 10, 0.000000, 0),  // Spellbook: Servitor Heal
		new("sb_aqua_swirl1", 10, 0.000000, 0),  // Spellbook: Aqua Swirl
		new("sb_arcane_acumen1", 10, 0.000000, 0),  // Spellbook: Acumen
		new("sb_energy_bolt1", 10, 0.000000, 0),  // Spellbook: Energy Bolt
		new("sb_aura_burn1", 10, 0.000000, 0),  // Spellbook: Aura Burn
		new("sb_charm11", 10, 0.000000, 0),  // Spellbook: Charm
		new("sb_concentration1", 10, 0.000000, 0),  // Spellbook: Concentration
		new("sb_water_breathing", 10, 0.000000, 0),  // Spellbook: Kiss of Eva
		new("sb_twister1", 10, 0.000000, 0),  // Spellbook: Twister
		new("sb_poison1", 10, 0.000000, 0),  // Spellbook: Poison
		new("sb_poison_recovery1", 10, 0.000000, 0),  // Spellbook: Poison Recovery
		new("sb_confusion1", 10, 0.000000, 0),  // Spellbook: Confusion
		new("sb_cure_bleeding1", 10, 0.000000, 0),  // Spellbook: Cure Bleeding
		new("sb_dryad_root1", 10, 0.000000, 0),  // Spellbook: Dryad Root
		new("sb_mental_shield1", 10, 0.000000, 0),  // Spellbook: Mental Shield
		new("sb_body_to_mind1", 10, 0.000000, 0),  // Spellbook: Body To Mind
		new("sb_shadow_spark1", 10, 0.000000, 0),  // Spellbook: Shadow Spark
		new("sb_surrender_to_earth1", 10, 0.000000, 0),  // Spellbook: Surrender To Earth
		new("sb_surrender_to_fire1", 10, 0.000000, 0),  // Spellbook: Surrender To Fire
		new("sb_surrender_to_poison1", 10, 0.000000, 0),  // Spellbook: Surrender To Poison
		new("sb_summon_cuti_cat1", 10, 0.000000, 0),  // Spellbook: Summon Mew the Cat
		new("sb_summon_unicorn_mirage1", 10, 0.000000, 0),  // Spellbook: Summon Mirage the Unicorn
		new("sb_servitor_mana_charge1", 10, 0.000000, 0),  // Spellbook: Servitor Recharge
		new("sb_solar_spark1", 10, 0.000000, 0),  // Spellbook: Solar Spark
		new("sb_agility1", 10, 0.000000, 0),  // Spellbook: Agility
		new("sb_empower1", 10, 0.000000, 0),  // Spellbook: Empower
		new("sb_poison_cloud1", 10, 0.000000, 0),  // Spellbook: Poisonous Cloud
		new("sb_focus1", 10, 0.000000, 0),  // Spellbook: Focus
		new("sb_holy_weapon1", 10, 0.000000, 0),  // Spellbook: Holy Weapon
		new("sb_touch_of_god1", 10, 0.000000, 0),  // Spellbook: Divine Heal
		new("sb_fire_resist1", 10, 0.000000, 0),  // Spellbook: Resist Fire
		new("sb_recharge1", 10, 0.000000, 0),  // Spellbook: Recharge
		new("sb_vampiric_rage1", 10, 0.000000, 0),  // Spellbook: Vampiric Rage
		new("sb_sleep1", 10, 0.000000, 0),  // Spellbook: Sleep
		new("sb_corpse_life_drain1", 10, 0.000000, 0),  // Spellbook: Corpse Life Drain
		new("sb_decrease_weight1", 10, 0.000000, 0),  // Spellbook: Decrease Weight
		new("sb_auqa_resist1", 10, 0.000000, 0),  // Spellbook: Resist Aqua
		new("sb_wind_resist1", 10, 0.000000, 0),  // Spellbook: Resist Wind
		new("sb_resist_poison1", 10, 0.000000, 0),  // Spellbook: Resist Poison
		new("sb_regeneration1", 10, 0.000000, 0),  // Spellbook: Regeneration
		new("sb_mighty_servitor1", 10, 0.000000, 0),  // Spellbook: Mighty Servitor
		new("sb_berserker_spirit1", 10, 0.000000, 0),  // Spellbook: Berserker Spirit
		new("sb_bright_servitor1", 10, 0.000000, 0),  // Spellbook: Servitor Magic Boost
		new("sb_slow1", 10, 0.000000, 0),  // Spellbook: Slow
		new("sb_curse_bleary1", 10, 0.000000, 0),  // Spellbook: Curse of Chaos
		new("sb_fast_servitor1", 10, 0.000000, 0),  // Spellbook: Servitor Wind Walk
		new("sb_erase_hostility1", 10, 0.000000, 0),  // Spellbook: Peace
		new("sb_speed_walk1", 10, 0.000000, 0),  // Spellbook: Sprint
		new("sb_zero_g1", 10, 0.000000, 0),  // Spellbook: Entangle
		new("sb_power_break1", 10, 0.000000, 0),  // Spellbook: Power Break
		new("sb_freezing_strike1", 10, 0.000000, 0),  // Spellbook: Freezing Strike
		new("sb_night_murmur1", 10, 0.000000, 0),  // Amulet: Dreaming Spirit
		new("sb_blood_lust1", 10, 0.000000, 0),  // Amulet: Life Drain
		new("sb_pain_thorn1", 10, 0.000000, 0),  // Amulet: Venom
		new("sb_devotioin_of_shine1", 10, 0.000000, 0),  // Amulet: Soul Shield
		new("sb_chill_flame1", 10, 0.000000, 0),  // Amulet: Chill Flame
		new("sb_mass_frenzy1", 10, 0.000000, 0),  // Amulet: Chant of Battle
		new("sb_external_fear1", 10, 0.000000, 0),  // Amulet: Fear
		new("sb_entice_madness1", 10, 0.000000, 0),  // Amulet: Madness
		new("sb_pain_edge1", 10, 0.000000, 0),  // Amulet: Seal of Poison
		new("sb_inspire_life_force1", 10, 0.000000, 0),  // Amulet: Chant of Life
		new("sb_devotioin_of_soul1", 10, 0.000000, 0),  // Amulet: Chant of Shielding
		new("sb_burning_spirit1", 10, 0.000000, 0),  // Amulet: Chant of Fire
		new("sb_blaze_quake1", 10, 0.000000, 0),  // Amulet: Frost Flame
		new("sb_eternal_flame1", 10, 0.000000, 0),  // Amulet: Blaze Quake
		new("sb_bind_will1", 10, 0.000000, 0),  // Amulet: Seal of Binding
		new("sb_aura_sway1", 10, 0.000000, 0),  // Amulet: Aura Sink
		new("sb_engrave_seal_of_timid1", 10, 0.000000, 0),  // Amulet: Seal of Chaos
		new("sb_pure_inspiration1", 10, 0.000000, 0),  // Amulet: Flame Chant
		new("sb_power_of_paagrio1", 10, 0.000000, 0),  // Amulet: Pa'agrio's Gift
		new("sb_blessing_of_paagrio1", 10, 0.000000, 0),  // Amulet: Pa'agrio's Blessing
		new("sb_engrave_seal_of_lazy1", 10, 0.000000, 0),  // Amulet: Seal of Slow
		new("sb_summon_mechanic_golem1", 10, 0.000000, 0)  // Blueprint: Summon Mechanic Golem
	
	};
}