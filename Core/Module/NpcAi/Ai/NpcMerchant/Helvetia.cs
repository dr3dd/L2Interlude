using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Helvetia : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
		// Consumable Items (Scrolls,  Potions)
		new("soulshot_none", 10, 0.000000, 0),  // Soulshot: No Grade
		new("spiritshot_none", 10, 0.000000, 0),  // Spiritshot: No Grade
		new("blessed_spiritshot_none", 10, 0.000000, 0),  // Blessed Spiritshot: No Grade
		new("comp_bspiritshot_none", 10, 0.000000, 0),  // Compressed Package of Blessed Spiritshots: No Grade
		new("comp_spiritshot_none", 10, 0.000000, 0),  // Compressed Package of Spiritshots: No Grade
		new("comp_soulshot_none", 10, 0.000000, 0),  // Compressed Package of Soulshots: No Grade
		new("adv_comp_bspiritshot_none", 10, 0.000000, 0),  // Greater Compressed Package of Blessed Spiritshots: No-grade
		new("adv_comp_spiritshot_none", 10, 0.000000, 0),  // Greater Compressed Package of Spiritshots: No-grade
		new("adv_comp_soulshot_none", 10, 0.000000, 0),  // Greater Compressed Package of Soulshots: No-grade
		new("wooden_arrow", 10, 0.000000, 0),  // Wooden Arrow
		new("bone_arrow", 10, 0.000000, 0),  // Bone Arrow
		new("fine_steel_arrow", 10, 0.000000, 0),  // Fine Steel Arrow
		new("lesser_healing_potion", 10, 0.000000, 0),  // Lesser Healing Potion
		new("healing_potion", 10, 0.000000, 0),  // Healing Potion
		new("antidote", 10, 0.000000, 0),  // Antidote
		new("advanced_antidote", 10, 0.000000, 0),  // Greater Antidote
		new("bandage", 10, 0.000000, 0),  // Bandage
		new("emergency_dressing", 10, 0.000000, 0),  // Emergency Dressing
		new("quick_step_potion", 10, 0.000000, 0),  // Haste Potion
		new("swift_attack_potion", 10, 0.000000, 0),  // Potion of Alacrity
		new("potion_of_acumen2", 10, 0.000000, 0),  // Magic Haste Potion
		new("scroll_of_awake", 10, 0.000000, 0),  // Waking Scroll
		new("scroll_of_escape", 10, 0.000000, 0),  // Scroll of Escape
		new("scroll_of_resurrection", 10, 0.000000, 0),  // Scroll of Resurrection
		new("scroll_of_escape_to_agit", 10, 0.000000, 0),  // Scroll of Escape: Clan Hall
		new("scroll_of_escape_to_castle", 10, 0.000000, 0),  // Scroll of Escape: Castle
		new("spirit_ore", 10, 0.000000, 0),  // Spirit Ore
		new("soul_ore", 10, 0.000000, 0),  // Soul Ore
		new("energy_stone", 10, 0.000000, 0),  // Energy Stone
		new("key_of_thief", 10, 0.000000, 0),  // Thief Key
		new("rope_of_magic_d", 10, 0.000000, 0),  // Rope of Magic: D-Grade
		new("rope_of_magic_c", 10, 0.000000, 0),  // Rope of Magic: C-Grade
		new("rope_of_magic_b", 10, 0.000000, 0),  // Rope of Magic: B-Grade
		new("rope_of_magic_a", 10, 0.000000, 0),  // Rope of Magic: A-Grade
		new("gemstone_d", 10, 0.000000, 0),  // Gemstone D
		new("gemstone_c", 10, 0.000000, 0),  // Gemstone C
		new("gemstone_b", 10, 0.000000, 0),  // Gemstone B
		new("scroll_recovery_no_grade", 10, 0.000000, 0),  // Scroll: Recovery (No Grade)
		new("scroll_recovery_grade_d", 10, 0.000000, 0),  // Scroll: Recovery (Grade D)
		new("scroll_recovery_grade_c", 10, 0.000000, 0),  // Scroll: Recovery (Grade C)
		new("scroll_recovery_grade_b", 10, 0.000000, 0),  // Scroll: Recovery (Grade B)
		new("scroll_recovery_grade_a", 10, 0.000000, 0),  // Scroll: Recovery (Grade A)
		new("elixir_of_life_no", 10, 0.000000, 0),  // Elixir of Life (No Grade)
		new("elixir_of_life_d", 10, 0.000000, 0),  // Elixir of Life (D-Grade)
		new("elixir_of_life_c", 10, 0.000000, 0),  // Elixir of Life (C-Grade)
		new("elixir_of_cp_no", 10, 0.000000, 0),  // Elixir of CP (No Grade)
		new("elixir_of_cp_d", 10, 0.000000, 0),  // Elixir of CP (D-Grade)
		new("elixir_of_cp_c", 10, 0.000000, 0),  // Elixir of CP (C-Grade)
		new("elixir_of_cp_b", 10, 0.000000, 0),  // Elixir of CP (B-Grade)
		new("elixir_of_cp_a", 10, 0.000000, 0),  // Elixir of CP (A-Grade)
		new("summoning_crystal", 10, 0.000000, 0),  // Summoning Crystal
		new("mysterious_solvent", 10, 0.000000, 0),  // Mysterious Solvent
		new("dice_heart", 10, 0.000000, 0),  // Dice (Heart)
		new("dice_spade", 10, 0.000000, 0),  // Dice (Spade)
		new("dice_clover", 10, 0.000000, 0),  // Dice (Clover)
		new("dice_diamond", 10, 0.000000, 0)  // Dice (Diamond)
	
	};
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new("dye_s1c3_d", 10, 0.000000, 0), 
		new("dye_s1d3_d", 10, 0.000000, 0), 
		new("dye_c1s3_d", 10, 0.000000, 0), 
		new("dye_c1d3_d", 10, 0.000000, 0), 
		new("dye_d1s3_d", 10, 0.000000, 0), 
		new("dye_d1c3_d", 10, 0.000000, 0), 
		new("dye_i1m3_d", 10, 0.000000, 0), 
		new("dye_i1w3_d", 10, 0.000000, 0), 
		new("dye_m1i3_d", 10, 0.000000, 0), 
		new("dye_m1w3_d", 10, 0.000000, 0), 
		new("dye_w1i3_d", 10, 0.000000, 0), 
		new("dye_w1m3_d", 10, 0.000000, 0), 
		new("dye_s1c2_d", 10, 0.000000, 0), 
		new("dye_s1d2_d", 10, 0.000000, 0), 
		new("dye_c1s2_d", 10, 0.000000, 0), 
		new("dye_c1d2_d", 10, 0.000000, 0), 
		new("dye_d1s2_d", 10, 0.000000, 0), 
		new("dye_d1c2_d", 10, 0.000000, 0), 
		new("dye_i1m2_d", 10, 0.000000, 0), 
		new("dye_i1w2_d", 10, 0.000000, 0), 
		new("dye_m1i2_d", 10, 0.000000, 0), 
		new("dye_m1w2_d", 10, 0.000000, 0), 
		new("dye_w1i2_d", 10, 0.000000, 0), 
		new("dye_w1m2_d", 10, 0.000000, 0), 
		new("dye_s1c3_c", 10, 0.000000, 0), 
		new("dye_s1d3_c", 10, 0.000000, 0), 
		new("dye_c1s3_c", 10, 0.000000, 0), 
		new("dye_c1c3_c", 10, 0.000000, 0), 
		new("dye_d1s3_c", 10, 0.000000, 0), 
		new("dye_d1c3_c", 10, 0.000000, 0), 
		new("dye_i1m3_c", 10, 0.000000, 0), 
		new("dye_i1w3_c", 10, 0.000000, 0), 
		new("dye_m1i3_c", 10, 0.000000, 0), 
		new("dye_m1w3_c", 10, 0.000000, 0), 
		new("dye_w1i3_c", 10, 0.000000, 0), 
		new("dye_w1m3_c", 10, 0.000000, 0), 
		new("dye_s1c2_c", 10, 0.000000, 0), 
		new("dye_s1d2_c", 10, 0.000000, 0), 
		new("dye_c1s2_c", 10, 0.000000, 0), 
		new("dye_c1c2_c", 10, 0.000000, 0), 
		new("dye_d1s2_c", 10, 0.000000, 0), 
		new("dye_d1c2_c", 10, 0.000000, 0), 
		new("dye_i1m2_c", 10, 0.000000, 0), 
		new("dye_i1w2_c", 10, 0.000000, 0), 
		new("dye_m1i2_c", 10, 0.000000, 0), 
		new("dye_m1w2_c", 10, 0.000000, 0), 
		new("dye_w1i2_c", 10, 0.000000, 0), 
		new("dye_w1m2_c", 10, 0.000000, 0), 
		new("dye_s2c4_c", 10, 0.000000, 0), 
		new("dye_s2d4_c", 10, 0.000000, 0), 
		new("dye_c2s4_c", 10, 0.000000, 0), 
		new("dye_c2c4_c", 10, 0.000000, 0), 
		new("dye_d2s4_c", 10, 0.000000, 0), 
		new("dye_d2c4_c", 10, 0.000000, 0), 
		new("dye_i2m4_c", 10, 0.000000, 0), 
		new("dye_i2w4_c", 10, 0.000000, 0), 
		new("dye_m2i4_c", 10, 0.000000, 0), 
		new("dye_m2w4_c", 10, 0.000000, 0), 
		new("dye_w2i4_c", 10, 0.000000, 0), 
		new("dye_w2m4_c", 10, 0.000000, 0), 
		new("dye_s2c3_c", 10, 0.000000, 0), 
		new("dye_s2d3_c", 10, 0.000000, 0), 
		new("dye_c2s3_c", 10, 0.000000, 0), 
		new("dye_c2c3_c", 10, 0.000000, 0), 
		new("dye_d2s3_c", 10, 0.000000, 0), 
		new("dye_d2c3_c", 10, 0.000000, 0), 
		new("dye_i2m3_c", 10, 0.000000, 0), 
		new("dye_i2w3_c", 10, 0.000000, 0), 
		new("dye_m2i3_c", 10, 0.000000, 0), 
		new("dye_m2w3_c", 10, 0.000000, 0), 
		new("dye_w2i3_c", 10, 0.000000, 0), 
		new("dye_w2m3_c", 10, 0.000000, 0), 
		new("dye_s3c5_c", 10, 0.000000, 0), 
		new("dye_s3d5_c", 10, 0.000000, 0), 
		new("dye_c3s5_c", 10, 0.000000, 0), 
		new("dye_c3c5_c", 10, 0.000000, 0), 
		new("dye_d3s5_c", 10, 0.000000, 0), 
		new("dye_d3c5_c", 10, 0.000000, 0), 
		new("dye_i3m5_c", 10, 0.000000, 0), 
		new("dye_i3w5_c", 10, 0.000000, 0), 
		new("dye_m3i5_c", 10, 0.000000, 0), 
		new("dye_m3w5_c", 10, 0.000000, 0), 
		new("dye_w3i5_c", 10, 0.000000, 0), 
		new("dye_w3m5_c", 10, 0.000000, 0), 
		new("dye_s3c4_c", 10, 0.000000, 0), 
		new("dye_s3d4_c", 10, 0.000000, 0), 
		new("dye_c3s4_c", 10, 0.000000, 0), 
		new("dye_c3c4_c", 10, 0.000000, 0), 
		new("dye_d3s4_c", 10, 0.000000, 0), 
		new("dye_d3c4_c", 10, 0.000000, 0), 
		new("dye_i3m4_c", 10, 0.000000, 0), 
		new("dye_i3w4_c", 10, 0.000000, 0), 
		new("dye_m3i4_c", 10, 0.000000, 0), 
		new("dye_m3w4_c", 10, 0.000000, 0), 
		new("dye_w3i4_c", 10, 0.000000, 0), 
		new("dye_w3m4_c", 10, 0.000000, 0), 
		new("dye_s4c6_c", 10, 0.000000, 0), 
		new("dye_s4d6_c", 10, 0.000000, 0), 
		new("dye_c4s6_c", 10, 0.000000, 0), 
		new("dye_c4c6_c", 10, 0.000000, 0), 
		new("dye_d4s6_c", 10, 0.000000, 0), 
		new("dye_d4c6_c", 10, 0.000000, 0), 
		new("dye_i4m6_c", 10, 0.000000, 0), 
		new("dye_i4w6_c", 10, 0.000000, 0), 
		new("dye_m4i6_c", 10, 0.000000, 0), 
		new("dye_m4w6_c", 10, 0.000000, 0), 
		new("dye_w4i6_c", 10, 0.000000, 0), 
		new("dye_w4m6_c", 10, 0.000000, 0), 
		new("dye_s4c5_c", 10, 0.000000, 0), 
		new("dye_s4d5_c", 10, 0.000000, 0), 
		new("dye_c4s5_c", 10, 0.000000, 0), 
		new("dye_c4c5_c", 10, 0.000000, 0), 
		new("dye_d4s5_c", 10, 0.000000, 0), 
		new("dye_d4c5_c", 10, 0.000000, 0), 
		new("dye_i4m5_c", 10, 0.000000, 0), 
		new("dye_i4w5_c", 10, 0.000000, 0), 
		new("dye_m4i5_c", 10, 0.000000, 0), 
		new("dye_m4w5_c", 10, 0.000000, 0), 
		new("dye_w4i5_c", 10, 0.000000, 0), 
		new("dye_w4m5_c", 10, 0.000000, 0)
	};
}