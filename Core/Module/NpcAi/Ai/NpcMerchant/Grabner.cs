using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Grabner : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
		// Warrior Weapons
		new("long_sword", 10, 0.000000, 0),  // Long Sword
		new("throw_knife", 10, 0.000000, 0),  // Throwing Knife
		new("bow_of_forest", 10, 0.000000, 0),  // Forest Bow
		new("short_spear", 10, 0.000000, 0),  // Short Spear
		new("falchion", 10, 0.000000, 0),  // Falchion
		new("sword_breaker", 10, 0.000000, 0),  // Sword Breaker
		new("composition_bow", 10, 0.000000, 0),  // Composition Bow
		new("buzdygan", 10, 0.000000, 0),  // Buzdygan
		new("iron_hammer", 10, 0.000000, 0),  // Iron Hammer
		new("long_spear", 10, 0.000000, 0),  // Long Spear
		new("saber", 10, 0.000000, 0),  // Saber
		new("iron_sword", 10, 0.000000, 0),  // Steel Sword
		new("handiwork_dagger", 10, 0.000000, 0),  // Crafted Dagger
		new("assassin_knife", 10, 0.000000, 0),  // Assassin Knife
		new("strengthening_bow", 10, 0.000000, 0),  // Strengthened Bow
		new("hand_axe", 10, 0.000000, 0),  // Hand Axe
		new("heavy_mace", 10, 0.000000, 0),  // Heavy Mace
		new("work_hammer", 10, 0.000000, 0),  // Work Hammer
		new("trident", 10, 0.000000, 0),  // Trident
		new("bastard_sword", 10, 0.000000, 0),  // Bastard Sword
		new("artisan_s_sword", 10, 0.000000, 0),  // Artisan's Sword
		new("poniard_dagger", 10, 0.000000, 0),  // Poniard Dagger
		new("long_bow", 10, 0.000000, 0),  // Long Bow
		new("elven_bow", 10, 0.000000, 0),  // Elven Bow
		new("dark_elven_bow", 10, 0.000000, 0),  // Dark Elven Bow
		new("tomahawk", 10, 0.000000, 0),  // Tomahawk
		new("pike", 10, 0.000000, 0),  // Pike
		new("dwarven_trident", 10, 0.000000, 0),  // Dwarven Trident
		new("two-handed_sword", 10, 0.000000, 0),  // Two-Handed Sword
		new("crimson_sword", 10, 0.000000, 0),  // Crimson Sword
		new("elven_sword", 10, 0.000000, 0),  // Elven Sword
		new("kukuri", 10, 0.000000, 0),  // Kukuri
		new("gastraphetes", 10, 0.000000, 0),  // Gastraphetes
		new("spike_club", 10, 0.000000, 0),  // Spiked Club
		new("war_hammer", 10, 0.000000, 0),  // War Hammer
		new("dwarven_pike", 10, 0.000000, 0),  // Dwarven Pike
		new("sword_of_revolution", 10, 0.000000, 0),  // Sword of Revolution
		new("maingauche", 10, 0.000000, 0),  // Maingauche
		new("cursed_maingauche", 10, 0.000000, 0),  // Cursed Maingauche
		new("strengthening_long_bow", 10, 0.000000, 0),  // Strengthened Long Bow
		new("tarbar", 10, 0.000000, 0),  // Tarbar
		new("giants_sword", 10, 0.000000, 0),  // Titan Sword
		new("giants_hammer", 10, 0.000000, 0),  // Titan Hammer
		new("heavy_bone_club", 10, 0.000000, 0),  // Heavy Bone Club
		new("hammer_in_flames", 10, 0.000000, 0),  // War Pick
		new("winged_spear", 10, 0.000000, 0),  // Winged Spear
		new("cestus", 10, 0.000000, 0),  // Cestus
		new("viper_s_canine", 10, 0.000000, 0),  // Viper's Fang
		new("bagh-nakh", 10, 0.000000, 0),  // Bagh-Nakh
		new("single-edged_jamadhr", 10, 0.000000, 0),  // Single-Edged Jamadhr
		new("triple-edged_jamadhr", 10, 0.000000, 0),  // Triple-Edged Jamadhr
		new("bich_hwa", 10, 0.000000, 0),  // Bich'Hwa
		new("zweihander", 10, 0.000000, 0),  // Zweihander
		new("heavy_sword", 10, 0.000000, 0)  // Heavy Sword
	};
}