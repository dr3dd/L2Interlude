using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Sandra : Merchant
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
		new("elven_ring", 10, 0.000000, 0),  // Elven Ring
		new("q_book_of_coin_collect", 10, 0.210000, 1),  // Coin Collecting Album
	
	};
}