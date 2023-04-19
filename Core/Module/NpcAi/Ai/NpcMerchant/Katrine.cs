using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Katrine : MerchantForNewbie
{
    /// <summary>
    /// Consumable Items (Scrolls, Potions)
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(1835, 15, 0.000000, 0), // Soulshot: No Grade
        new(2509, 15, 0.000000, 0), // Spiritshot: No Grade
        new(3947, 15, 0.000000, 0), // Blessed Spiritshot: No Grade
        new(5146, 15, 0.000000, 0), // Compressed Package of Blessed Spiritshots: No Grade
        new(5140, 15, 0.000000, 0), // Compressed Package of Spiritshots: No Grade
        new(5134, 15, 0.000000, 0), // Compressed Package of Soulshots: No Grade
        new(5262, 15, 0.000000, 0), // Greater Compressed Package of Blessed Spiritshots: No-grade
        new(5256, 15, 0.000000, 0), // Greater Compressed Package of Spiritshots: No-grade
        new(5250, 15, 0.000000, 0), // Greater Compressed Package of Soulshots: No-grade
        new(17, 15, 0.000000, 0),   // Wooden Arrow
        new(1341, 15, 0.000000, 0), // Bone Arrow
        new(1060, 15, 0.000000, 0), // Lesser Healing Potion
        new(1831, 15, 0.000000, 0), // Antidote
        new(1833, 15, 0.000000, 0), // Bandage
        new(734, 15, 0.000000, 0),  // Haste Potion
        new(735, 15, 0.000000, 0),  // Potion of Alacrity
        new(6035, 15, 0.000000, 0), // Magic Haste Potion
        new(736, 15, 0.000000, 0),  // Scroll of Escape
        new(737, 15, 0.000000, 0),  // Scroll of Resurrection
        new(3031, 15, 0.000000, 0), // Spirit Ore
        new(1785, 15, 0.000000, 0), // Soul Ore
        new(5589, 15, 0.000000, 0), // Energy Stone
        new(1661, 15, 0.000000, 0), // Thief Key
        new(5192, 15, 0.000000, 0), // Rope of Magic: D-Grade
        new(8594, 15, 0.000000, 0), // Scroll: Recovery (No Grade)
        new(8595, 15, 0.000000, 0), // Scroll: Recovery (Grade D)
        new(8622, 15, 0.000000, 0), // Elixir of Life (No Grade)
        new(8623, 15, 0.000000, 0), // Elixir of Life (D-Grade)
        new(8628, 15, 0.000000, 0), // Elixir of Mental Strength (No Grade)
        new(8629, 15, 0.000000, 0), // Elixir of Mental Strength (D-Grade)
        new(8634, 15, 0.000000, 0), // Elixir of CP (No Grade)
        new(8635, 15, 0.000000, 0), // Elixir of CP (D-Grade)
        new(8615, 15, 0.000000, 0), // Summoning Crystal
        new(4625, 15, 0.000000, 0), // Dice (Heart)
        new(4626, 15, 0.000000, 0), // Dice (Spade)
        new(4627, 15, 0.000000, 0), // Dice (Clover)
        new(4628, 15, 0.000000, 0), // Dice (Diamond)
    };
}