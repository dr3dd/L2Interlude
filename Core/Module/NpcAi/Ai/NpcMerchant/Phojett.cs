using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Phojett : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
		// Mystic Equip
		new("hose_of_devotion", 10, 0.000000, 0),  // Stockings of Devotion
		new("tunic_of_devotion", 10, 0.000000, 0),  // Tunic of Devotion
		new("hose_of_magicpower", 10, 0.000000, 0),  // Stockings of Magic
		new("tunic_of_magicpower", 10, 0.000000, 0),  // Tunic of Magic
		new("cursed_hose", 10, 0.000000, 0),  // Cursed Stockings
		new("cursed_tunic", 10, 0.000000, 0),  // Cursed Tunic
		new("dark_hose", 10, 0.000000, 0),  // Dark Stockings
		new("white_tunic", 10, 0.000000, 0),  // White Tunic
		new("mage_s_hose", 10, 0.000000, 0),  // Mystic's Stockings
		new("mage_s_tunic", 10, 0.000000, 0),  // Mystic's Tunic
		new("hose_of_knowledge", 10, 0.000000, 0),  // Stockings of Knowledge
		new("tunic_of_knowledge", 10, 0.000000, 0),  // Tunic of Knowledge
		new("mithril_hose", 10, 0.000000, 0),  // Mithril Stockings
		new("mithril_tunic", 10, 0.000000, 0),  // Mithril Tunic
		new("round_shield", 10, 0.000000, 0),  // Round Shield
		new("bone_shield", 10, 0.000000, 0),  // Bone Shield
		new("bronze_shield", 10, 0.000000, 0),  // Bronze Shield
		new("aspis", 10, 0.000000, 0),  // Aspis
		new("hoplon", 10, 0.000000, 0),  // Hoplon
		new("kite_shield", 10, 0.000000, 0),  // Kite Shield
		new("leather_gloves", 10, 0.000000, 0),  // Leather Gloves
		new("bracer", 10, 0.000000, 0),  // Bracer
		new("excellence_leather_gloves", 10, 0.000000, 0),  // Crafted Leather Gloves
		new("leather_gauntlet", 10, 0.000000, 0),  // Leather Gauntlets
		new("gauntlet", 10, 0.000000, 0),  // Gauntlets
		new("gauntlet_of_repose_of_the_soul", 10, 0.000000, 0),  // Rip Gauntlets
		new("low_boots", 10, 0.000000, 0),  // Low Boots
		new("boots", 10, 0.000000, 0),  // Boots
		new("leather_boots", 10, 0.000000, 0),  // Leather Boots
		new("blue_buckskin_boots", 10, 0.000000, 0),  // Blue Buckskin Boots
		new("iron_boots", 10, 0.000000, 0),  // Iron Boots
		new("boots_of_power", 10, 0.000000, 0),  // Boots of Power
		new("leather_helmet", 10, 0.000000, 0),  // Leather Helmet
		new("hard_leather_helmet", 10, 0.000000, 0),  // Hard Leather Helmet
		new("bone_helmet", 10, 0.000000, 0),  // Bone Helmet
		new("bronze_helmet", 10, 0.000000, 0),  // Bronze Helmet
		new("helmet", 10, 0.000000, 0),  // Helmet
	};

}