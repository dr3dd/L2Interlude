using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class TraderUska : MerchantForNewbie
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

    /// <summary>
    /// SpellBooks, Amulets
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(1529, 15, 0.000000, 0), // Amulet: Dreaming Spirit
        new(1525, 15, 0.000000, 0), // Amulet: Life Drain
        new(1527, 15, 0.000000, 0), // Amulet: Venom
        new(1524, 15, 0.000000, 0), // Amulet: Soul Shield
        new(1531, 15, 0.000000, 0), // Amulet: Chill Flame
        new(1522, 15, 0.000000, 0), // Amulet: Chant of Battle
        new(1526, 15, 0.000000, 0), // Amulet: Fear
        new(1534, 15, 0.000000, 0), // Amulet: Madness
        new(1537, 15, 0.000000, 0), // Amulet: Seal of Poison
        new(1856, 15, 0.000000, 0), // Amulet: Chant of Life
        new(1523, 15, 0.000000, 0), // Amulet: Chant of Shielding
        new(1521, 15, 0.000000, 0), // Amulet: Chant of Fire
        new(1535, 15, 0.000000, 0), // Amulet: Frost Flame
    };
}