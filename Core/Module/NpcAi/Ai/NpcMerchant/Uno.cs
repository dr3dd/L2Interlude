using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Uno : MerchantForNewbie
{
    /// <summary>
    /// Warrior Weapon
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(1, 15, 0.000000, 0), // Short Sword
        new(4, 15, 0.000000, 0), // Club
        new(11, 15, 0.000000, 0), // Bone Dagger
        new(13, 15, 0.000000, 0), // Short Bow
        new(3, 15, 0.000000, 0), // Broadsword
        new(152, 15, 0.000000, 0), // Heavy Chisel
        new(12, 15, 0.000000, 0), // Knife
        new(215, 15, 0.000000, 0), // Doom Dagger
        new(14, 15, 0.000000, 0), // Bow
        new(5, 15, 0.000000, 0), // Mace
        new(153, 15, 0.000000, 0), // Sickle
        new(1333, 15, 0.000000, 0), // Brandish
        new(66, 15, 0.000000, 0), // Gladius
        new(67, 15, 0.000000, 0), // Orcish Sword
        new(122, 15, 0.000000, 0), // Handmade Sword
        new(154, 15, 0.000000, 0), // Dwarven Mace
        new(216, 15, 0.000000, 0), // Dirk
        new(271, 15, 0.000000, 0), // Hunting Bow
        new(2, 15, 0.000000, 0), // Long Sword
        new(218, 15, 0.000000, 0), // Throwing Knife
        new(272, 15, 0.000000, 0), // Forest Bow
        new(15, 15, 0.000000, 0), // Short Spear
        new(5284, 15, 0.000000, 0) // Zweihander
    };
    
    /// <summary>
    /// Mystic Weapon
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(7, 15, 0.000000, 0), // Apprentice's Rod
        new(308, 15, 0.000000, 0), // Buffalo's Horn
        new(8, 15, 0.000000, 0), // Willow Staff
        new(99, 15, 0.000000, 0), // Apprentice's Spellbook
        new(9, 15, 0.000000, 0), // Cedar Staff
        new(176, 15, 0.000000, 0), // Journeyman's Staff
        new(310, 15, 0.000000, 0), // Relic of The Saints
    };
}