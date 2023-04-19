using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Green : Merchant
{
    /// <summary>
    /// Consumable Items (Scrolls, Potions)
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(1835, 20, 0.000000, 0), // Soulshot: No Grade
        new(2509, 20, 0.000000, 0), // Spiritshot: No Grade
        new(3947, 20, 0.000000, 0), // Blessed Spiritshot: No Grade
        new(5146, 20, 0.000000, 0), // Compressed Package of Blessed Spiritshots: No Grade
        new(5140, 20, 0.000000, 0), // Compressed Package of Spiritshots: No Grade
        new(5134, 20, 0.000000, 0), // Compressed Package of Soulshots: No Grade
        new(5262, 20, 0.000000, 0), // Greater Compressed Package of Blessed Spiritshots: No-grade
        new(5256, 20, 0.000000, 0), // Greater Compressed Package of Spiritshots: No-grade
        new(5250, 20, 0.000000, 0), // Greater Compressed Package of Soulshots: No-grade
        new(17, 20, 0.000000, 0),   // Wooden Arrow
        new(1341, 20, 0.000000, 0), // Bone Arrow
        new(1060, 20, 0.000000, 0), // Lesser Healing Potion
        new(1831, 20, 0.000000, 0), // Antidote
        new(1833, 20, 0.000000, 0), // Bandage
        new(734, 20, 0.000000, 0),  // Haste Potion
        new(735, 20, 0.000000, 0),  // Potion of Alacrity
        new(6035, 20, 0.000000, 0), // Magic Haste Potion
        new(736, 20, 0.000000, 0),  // Scroll of Escape
        new(737, 20, 0.000000, 0),  // Scroll of Resurrection
        new(3031, 20, 0.000000, 0), // Spirit Ore
        new(1785, 20, 0.000000, 0), // Soul Ore
        new(5589, 20, 0.000000, 0), // Energy Stone
        new(1661, 20, 0.000000, 0), // Thief Key
        new(5192, 20, 0.000000, 0), // Rope of Magic: D-Grade
        new(8594, 20, 0.000000, 0), // Scroll: Recovery (No Grade)
        new(8595, 20, 0.000000, 0), // Scroll: Recovery (Grade D)
        new(8622, 20, 0.000000, 0), // Elixir of Life (No Grade)
        new(8623, 20, 0.000000, 0), // Elixir of Life (D-Grade)
        new(8628, 20, 0.000000, 0), // Elixir of Mental Strength (No Grade)
        new(8629, 20, 0.000000, 0), // Elixir of Mental Strength (D-Grade)
        new(8634, 20, 0.000000, 0), // Elixir of CP (No Grade)
        new(8635, 20, 0.000000, 0), // Elixir of CP (D-Grade)
        new(8615, 20, 0.000000, 0), // Summoning Crystal
        new(4625, 20, 0.000000, 0), // Dice (Heart)
        new(4626, 20, 0.000000, 0), // Dice (Spade)
        new(4627, 20, 0.000000, 0), // Dice (Clover)
        new(4628, 20, 0.000000, 0), // Dice (Diamond)
    };
}