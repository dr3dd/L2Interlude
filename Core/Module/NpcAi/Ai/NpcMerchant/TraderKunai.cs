using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class TraderKunai : MerchantForNewbie
{
    /// <summary>
    /// Warrior Equip
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(28, 15, 0.000000, 0), // Pants
        new(21, 15, 0.000000, 0), // Shirt
        new(29, 15, 0.000000, 0), // Leather Pants
        new(22, 15, 0.000000, 0), // Leather Shirt
        new(30, 15, 0.000000, 0), // Hard Leather Pants
        new(2386, 15, 0.000000, 0), // Wooden Gaiters
        new(23, 15, 0.000000, 0), // Wooden Breastplate
        new(412, 15, 0.000000, 0), // Cotton Pants
        new(390, 15, 0.000000, 0), // Cotton Shirt
        new(24, 15, 0.000000, 0), // Bone Breastplate
        new(31, 15, 0.000000, 0), // Bone Gaiters
        new(18, 15, 0.000000, 0), // Leather Shield
        new(19, 15, 0.000000, 0), // Small Shield
        new(20, 15, 0.000000, 0), // Buckler
        new(102, 15, 0.000000, 0), // Round Shield
        new(48, 15, 0.000000, 0), // Short Gloves
        new(1119, 15, 0.000000, 0), // Short Leather Gloves
        new(49, 15, 0.000000, 0), // Gloves
        new(50, 15, 0.000000, 0), // Leather Gloves
        new(1121, 15, 0.000000, 0), // Apprentice's Shoes
        new(35, 15, 0.000000, 0), // Cloth Shoes
        new(36, 15, 0.000000, 0), // Leather Sandals
        new(1129, 15, 0.000000, 0), // Crude Leather Shoes
        new(1122, 15, 0.000000, 0), // Cotton Shoes
        new(37, 15, 0.000000, 0), // Leather Shoes
        new(38, 15, 0.000000, 0), // Low Boots
        new(41, 15, 0.000000, 0), // Cloth Cap
        new(42, 15, 0.000000, 0), // Leather Cap
        new(43, 15, 0.000000, 0), // Wooden Helmet
        new(44, 15, 0.000000, 0), // Leather Helmet
    };

    /// <summary>
    /// Mystic Equip
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(462, 15, 0.000000, 0), // Stockings
        new(426, 15, 0.000000, 0), // Tunic
        new(1103, 15, 0.000000, 0), // Cotton Stockings
        new(1100, 15, 0.000000, 0), // Cotton Tunic
        new(463, 15, 0.000000, 0), // Feriotic Stockings
        new(428, 15, 0.000000, 0), // Feriotic Tunic
        new(464, 15, 0.000000, 0), // Leather Stockings
        new(429, 15, 0.000000, 0), // Leather Tunic
        new(1104, 15, 0.000000, 0), // Stockings of Devotion
        new(1101, 15, 0.000000, 0), // Tunic of Devotion
        new(18, 15, 0.000000, 0), // Leather Shield
        new(19, 15, 0.000000, 0), // Small Shield
        new(20, 15, 0.000000, 0), // Buckler
        new(102, 15, 0.000000, 0), // Round Shield
        new(48, 15, 0.000000, 0), // Short Gloves
        new(1119, 15, 0.000000, 0), // Short Leather Gloves
        new(49, 15, 0.000000, 0), // Gloves
        new(50, 15, 0.000000, 0), // Leather Gloves
        new(1121, 15, 0.000000, 0), // Apprentice's Shoes
        new(35, 15, 0.000000, 0), // Cloth Shoes
        new(36, 15, 0.000000, 0), // Leather Sandals
        new(1129, 15, 0.000000, 0), // Crude Leather Shoes
        new(1122, 15, 0.000000, 0), // Cotton Shoes
        new(37, 15, 0.000000, 0), // Leather Shoes
        new(38, 15, 0.000000, 0), // Low Boots
        new(41, 15, 0.000000, 0), // Cloth Cap
        new(42, 15, 0.000000, 0), // Leather Cap
        new(43, 15, 0.000000, 0), // Wooden Helmet
        new(44, 15, 0.000000, 0) // Leather Helmet
    };
}