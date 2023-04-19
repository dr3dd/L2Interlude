using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class TraderSydney : MerchantForNewbie
{
    /// <summary>
    /// Warrior Weapons
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(2, 20, 0.000000, 0), // Long Sword
        new(218, 20, 0.000000, 0), // Throwing Knife
        new(272, 20, 0.000000, 0), // Forest Bow
        new(15, 20, 0.000000, 0), // Short Spear
        new(68, 20, 0.000000, 0), // Falchion
        new(219, 20, 0.000000, 0), // Sword Breaker
        new(273, 20, 0.000000, 0), // Composition Bow
        new(155, 20, 0.000000, 0), // Buzdygan
        new(87, 20, 0.000000, 0), // Iron Hammer
        new(16, 20, 0.000000, 0), // Long Spear
        new(123, 20, 0.000000, 0), // Saber
        new(7880, 20, 0.000000, 0), // Steel Sword
        new(220, 20, 0.000000, 0), // Crafted Dagger
        new(221, 20, 0.000000, 0), // Assassin Knife
        new(274, 20, 0.000000, 0), // Strengthened Bow
        new(156, 20, 0.000000, 0), // Hand Axe
        new(166, 20, 0.000000, 0), // Heavy Mace
        new(168, 20, 0.000000, 0), // Work Hammer
        new(291, 20, 0.000000, 0), // Trident
        new(69, 20, 0.000000, 0), // Bastard Sword
        new(126, 20, 0.000000, 0), // Artisan's Sword
        new(222, 20, 0.000000, 0), // Poniard Dagger
        new(275, 20, 0.000000, 0), // Long Bow
        new(277, 20, 0.000000, 0), // Dark Elven Bow
        new(292, 20, 0.000000, 0), // Pike
        new(295, 20, 0.000000, 0), // Dwarven Trident
        new(256, 20, 0.000000, 0), // Cestus
        new(257, 20, 0.000000, 0), // Viper's Fang
        new(258, 20, 0.000000, 0), // Bagh-Nakh
        new(259, 20, 0.000000, 0), // Single-Edged Jamadhr
    };

    /// <summary>
    /// Mystic Weapons
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(176, 20, 0.000000, 0), // Journeyman's Staff
        new(310, 20, 0.000000, 0), // Relic of The Saints
        new(177, 20, 0.000000, 0), // Mage Staff
        new(311, 20, 0.000000, 0), // Crucifix of Blessing
        new(100, 20, 0.000000, 0), // Voodoo Doll
        new(178, 20, 0.000000, 0), // Bone Staff
        new(101, 20, 0.000000, 0), // Scroll of Wisdom
        new(7885, 20, 0.000000, 0), // Priest Sword
        new(312, 20, 0.000000, 0), // Branch of Life
        new(314, 20, 0.000000, 0), // Proof of Revenge
        new(179, 20, 0.000000, 0), // Mace of Prayer
        new(182, 20, 0.000000, 0), // Doom Hammer
        new(183, 20, 0.000000, 0), // Mystic Staff
        new(185, 20, 0.000000, 0), // Staff of Mana
        new(315, 20, 0.000000, 0), // Divine Tome
        new(83, 20, 0.000000, 0), // Sword of Magic
        new(143, 20, 0.000000, 0), // Sword of Mystic
        new(144, 20, 0.000000, 0), // Sword of Occult
        new(238, 20, 0.000000, 0), // Dagger of Mana
        new(239, 20, 0.000000, 0), // Mystic Knife
        new(240, 20, 0.000000, 0) // Conjurer's Knife
    };
}