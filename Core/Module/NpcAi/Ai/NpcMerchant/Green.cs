using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Green : Merchant
{
    /// <summary>
    /// Consumable Items (Scrolls, Potions)
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
		new("soulshot_none", 20, 0.000000, 0),  // Soulshot: No Grade
		new("spiritshot_none", 20, 0.000000, 0),  // Spiritshot: No Grade
		new("blessed_spiritshot_none", 20, 0.000000, 0),  // Blessed Spiritshot: No Grade
		new("comp_bspiritshot_none", 20, 0.000000, 0),  // Compressed Package of Blessed Spiritshots: No Grade
		new("comp_spiritshot_none", 20, 0.000000, 0),  // Compressed Package of Spiritshots: No Grade
		new("comp_soulshot_none", 20, 0.000000, 0),  // Compressed Package of Soulshots: No Grade
		new("adv_comp_bspiritshot_none", 20, 0.000000, 0),  // Greater Compressed Package of Blessed Spiritshots: No-grade
		new("adv_comp_spiritshot_none", 20, 0.000000, 0),  // Greater Compressed Package of Spiritshots: No-grade
		new("adv_comp_soulshot_none", 20, 0.000000, 0),  // Greater Compressed Package of Soulshots: No-grade
		new("wooden_arrow", 20, 0.000000, 0),  // Wooden Arrow
		new("bone_arrow", 20, 0.000000, 0),  // Bone Arrow
		new("fine_steel_arrow", 20, 0.000000, 0),  // Fine Steel Arrow
		new("lesser_healing_potion", 20, 0.000000, 0),  // Lesser Healing Potion
		new("healing_potion", 20, 0.000000, 0),  // Healing Potion
		new("antidote", 20, 0.000000, 0),  // Antidote
		new("advanced_antidote", 20, 0.000000, 0),  // Greater Antidote
		new("bandage", 20, 0.000000, 0),  // Bandage
		new("emergency_dressing", 20, 0.000000, 0),  // Emergency Dressing
		new("quick_step_potion", 20, 0.000000, 0),  // Haste Potion
		new("swift_attack_potion", 20, 0.000000, 0),  // Potion of Alacrity
		new("potion_of_acumen2", 20, 0.000000, 0),  // Magic Haste Potion
		new("scroll_of_awake", 20, 0.000000, 0),  // Waking Scroll
		new("scroll_of_escape", 20, 0.000000, 0),  // Scroll of Escape
		new("scroll_of_resurrection", 20, 0.000000, 0),  // Scroll of Resurrection
		new("scroll_of_escape_to_agit", 20, 0.000000, 0),  // Scroll of Escape: Clan Hall
		new("spirit_ore", 20, 0.000000, 0),  // Spirit Ore
		new("soul_ore", 20, 0.000000, 0),  // Soul Ore
		new("energy_stone", 20, 0.000000, 0),  // Energy Stone
		new("key_of_thief", 20, 0.000000, 0),  // Thief Key
		new("rope_of_magic_d", 20, 0.000000, 0),  // Rope of Magic: D-Grade
		new("rope_of_magic_c", 20, 0.000000, 0),  // Rope of Magic: C-Grade
		new("rope_of_magic_b", 20, 0.000000, 0),  // Rope of Magic: B-Grade
		new("gemstone_d", 20, 0.000000, 0),  // Gemstone D
		new("scroll_recovery_no_grade", 20, 0.000000, 0),  // Scroll: Recovery (No Grade)
		new("scroll_recovery_grade_d", 20, 0.000000, 0),  // Scroll: Recovery (Grade D)
		new("scroll_recovery_grade_c", 20, 0.000000, 0),  // Scroll: Recovery (Grade C)
		new("scroll_recovery_grade_b", 20, 0.000000, 0),  // Scroll: Recovery (Grade B)
		new("elixir_of_life_no", 20, 0.000000, 0),  // Elixir of Life (No Grade)
		new("elixir_of_life_d", 20, 0.000000, 0),  // Elixir of Life (D-Grade)
		new("elixir_of_life_c", 20, 0.000000, 0),  // Elixir of Life (C-Grade)
		new("elixir_of_mental_strength_no", 20, 0.000000, 0),  // Elixir of Mental Strength (No Grade)
		new("elixir_of_mental_strength_d", 20, 0.000000, 0),  // Elixir of Mental Strength (D-Grade)
		new("elixir_of_cp_no", 20, 0.000000, 0),  // Elixir of CP (No Grade)
		new("elixir_of_cp_d", 20, 0.000000, 0),  // Elixir of CP (D-Grade)
		new("elixir_of_cp_c", 20, 0.000000, 0),  // Elixir of CP (C-Grade)
		new("elixir_of_cp_b", 20, 0.000000, 0),  // Elixir of CP (B-Grade)
		new("summoning_crystal", 20, 0.000000, 0),  // Summoning Crystal
		new("dice_heart", 20, 0.000000, 0),  // Dice (Heart)
		new("dice_spade", 20, 0.000000, 0),  // Dice (Spade)
		new("dice_clover", 20, 0.000000, 0),  // Dice (Clover)
		new("dice_diamond", 20, 0.000000, 0),  // Dice (Diamond)
	};
}