using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class TraderGarita : MerchantForNewbie
{
    /// <summary>
    /// Accessory (Magic Rings)
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(118, 15, 0.000000, 0), // Necklace of Magic
        new(906, 15, 0.000000, 0), // Necklace of Knowledge
        new(907, 15, 0.000000, 0), // Necklace of Anguish
        new(908, 15, 0.000000, 0), // Necklace of Wisdom
        new(112, 15, 0.000000, 0), // Apprentice's Earring
        new(113, 15, 0.000000, 0), // Mystic's Earring
        new(114, 15, 0.000000, 0), // Earring of Strength
        new(115, 15, 0.000000, 0), // Earring of Wisdom
        new(845, 15, 0.000000, 0), // Cat's Eye Earring
        new(116, 15, 0.000000, 0), // Magic Ring
        new(875, 15, 0.000000, 0), // Ring of Knowledge
        new(876, 15, 0.000000, 0), // Ring of Anguish
        new(877, 15, 0.000000, 0), // Ring of Wisdom
    };
}