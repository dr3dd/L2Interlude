using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class TraderSimplon : MerchantForNewbie
{
    /// <summary>
    /// Warrior Equip
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(412, 20, 0.000000, 0), // Cotton Pants
        new(390, 20, 0.000000, 0), // Cotton Shirt
        new(24, 20, 0.000000, 0), // Bone Breastplate
        new(31, 20, 0.000000, 0), // Bone Gaiters
        new(25, 20, 0.000000, 0), // Piece Bone Breastplate
        new(26, 20, 0.000000, 0), // Bronze Breastplate
        new(33, 20, 0.000000, 0), // Hard Leather Gaiters
        new(32, 20, 0.000000, 0), // Piece Bone Gaiters
        new(27, 20, 0.000000, 0), // Hard Leather Shirt
        new(34, 20, 0.000000, 0), // Bronze Gaiters
        new(413, 20, 0.000000, 0), // Puma Skin Gaiters
        new(347, 20, 0.000000, 0), // Ring Mail Breastplate
        new(376, 20, 0.000000, 0), // Iron Plate Gaiters
        new(391, 20, 0.000000, 0), // Puma Skin Shirt
        new(414, 20, 0.000000, 0), // Lion Skin Gaiters
        new(392, 20, 0.000000, 0), // Lion Skin Shirt
        new(58, 20, 0.000000, 0), // Mithril Breastplate
        new(59, 20, 0.000000, 0), // Mithril Gaiters
        new(102, 20, 0.000000, 0), // Round Shield
        new(625, 20, 0.000000, 0), // Bone Shield
        new(626, 20, 0.000000, 0), // Bronze Shield
        new(627, 20, 0.000000, 0), // Aspis
        new(50, 20, 0.000000, 0), // Leather Gloves
        new(51, 20, 0.000000, 0), // Bracer
        new(604, 20, 0.000000, 0), // Crafted Leather Gloves
        new(605, 20, 0.000000, 0), // Leather Gauntlets
        new(38, 20, 0.000000, 0), // Low Boots
        new(39, 20, 0.000000, 0), // Boots
        new(40, 20, 0.000000, 0), // Leather Boots
        new(1123, 20, 0.000000, 0), // Blue Buckskin Boots
        new(44, 20, 0.000000, 0), // Leather Helmet
        new(1148, 20, 0.000000, 0), // Hard Leather Helmet
        new(45, 20, 0.000000, 0), // Bone Helmet
        new(46, 20, 0.000000, 0), // Bronze Helmet
        new(415, 20, 0.000000, 0), // Mithril Banded Gaiters
        new(393, 20, 0.000000, 0), // Mithril Banded Mail
    };

    /// <summary>
    /// Mystic Equip
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(1104, 20, 0.000000, 0), // Stockings of Devotion
        new(1101, 20, 0.000000, 0), // Tunic of Devotion
        new(1105, 20, 0.000000, 0), // Stockings of Magic
        new(1102, 20, 0.000000, 0), // Tunic of Magic
        new(465, 20, 0.000000, 0), // Cursed Stockings
        new(432, 20, 0.000000, 0), // Cursed Tunic
        new(467, 20, 0.000000, 0), // Dark Stockings
        new(434, 20, 0.000000, 0), // White Tunic
        new(468, 20, 0.000000, 0), // Mystic's Stockings
        new(435, 20, 0.000000, 0), // Mystic's Tunic
        new(469, 20, 0.000000, 0), // Stockings of Knowledge
        new(436, 20, 0.000000, 0), // Tunic of Knowledge
        new(102, 20, 0.000000, 0), // Round Shield
        new(625, 20, 0.000000, 0), // Bone Shield
        new(626, 20, 0.000000, 0), // Bronze Shield
        new(627, 20, 0.000000, 0), // Aspis
        new(50, 20, 0.000000, 0), // Leather Gloves
        new(51, 20, 0.000000, 0), // Bracer
        new(604, 20, 0.000000, 0), // Crafted Leather Gloves
        new(605, 20, 0.000000, 0), // Leather Gauntlets
        new(38, 20, 0.000000, 0), // Low Boots
        new(39, 20, 0.000000, 0), // Boots
        new(40, 20, 0.000000, 0), // Leather Boots
        new(1123, 20, 0.000000, 0), // Blue Buckskin Boots
        new(44, 20, 0.000000, 0), // Leather Helmet
        new(1148, 20, 0.000000, 0), // Hard Leather Helmet
        new(45, 20, 0.000000, 0), // Bone Helmet
        new(46, 20, 0.000000, 0) // Bronze Helmet
    };
}