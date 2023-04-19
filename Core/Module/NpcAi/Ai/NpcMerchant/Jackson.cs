using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Jackson : MerchantForNewbie
{
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

    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new BuySellList(462, 15, 0.000000, 0), // Stockings
        new BuySellList(426, 15, 0.000000, 0), // Tunic
        new BuySellList(1103, 15, 0.000000, 0), // Cotton Stockings
        new BuySellList(1100, 15, 0.000000, 0), // Cotton Tunic
        new BuySellList(463, 15, 0.000000, 0), // Feriotic Stockings
        new BuySellList(428, 15, 0.000000, 0), // Feriotic Tunic
        new BuySellList(464, 15, 0.000000, 0), // Leather Stockings
        new BuySellList(429, 15, 0.000000, 0), // Leather Tunic
        new BuySellList(1104, 15, 0.000000, 0), // Stockings of Devotion
        new BuySellList(1101, 15, 0.000000, 0), // Tunic of Devotion
        new BuySellList(18, 15, 0.000000, 0), // Leather Shield
        new BuySellList(19, 15, 0.000000, 0), // Small Shield
        new BuySellList(20, 15, 0.000000, 0), // Buckler
        new BuySellList(102, 15, 0.000000, 0), // Round Shield
        new BuySellList(48, 15, 0.000000, 0), // Short Gloves
        new BuySellList(1119, 15, 0.000000, 0), // Short Leather Gloves
        new BuySellList(49, 15, 0.000000, 0), // Gloves
        new BuySellList(50, 15, 0.000000, 0), // Leather Gloves
        new BuySellList(1121, 15, 0.000000, 0), // Apprentice's Shoes
        new BuySellList(35, 15, 0.000000, 0), // Cloth Shoes
        new BuySellList(36, 15, 0.000000, 0), // Leather Sandals
        new BuySellList(1129, 15, 0.000000, 0), // Crude Leather Shoes
        new BuySellList(1122, 15, 0.000000, 0), // Cotton Shoes
        new BuySellList(37, 15, 0.000000, 0), // Leather Shoes
        new BuySellList(38, 15, 0.000000, 0), // Low Boots
        new BuySellList(41, 15, 0.000000, 0), // Cloth Cap
        new BuySellList(42, 15, 0.000000, 0), // Leather Cap
        new BuySellList(43, 15, 0.000000, 0), // Wooden Helmet
        new BuySellList(44, 15, 0.000000, 0) // Leather Helmet
    };
}