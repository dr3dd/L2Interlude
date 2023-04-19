using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Cel : MerchantForNewbie
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
        new(1294, 15, 0.000000, 0), // Spellbook: Defense Aura
        new(1095, 15, 0.000000, 0), // Spellbook: Attack Aura
        new(1048, 15, 0.000000, 0), // Spellbook: Might
        new(1050, 15, 0.000000, 0), // Spellbook: Battle Heal
        new(1051, 15, 0.000000, 0), // Spellbook: Vampiric Touch
        new(1049, 15, 0.000000, 0), // Spellbook: Ice Bolt
        new(1152, 15, 0.000000, 0), // Spellbook: Flame Strike
        new(1054, 15, 0.000000, 0), // Spellbook: Group Heal
        new(1058, 15, 0.000000, 0), // Spellbook: Shield
        new(1099, 15, 0.000000, 0), // Spellbook: Wind Shackle
        new(1098, 15, 0.000000, 0), // Spellbook: Wind Walk
        new(1056, 15, 0.000000, 0), // Spellbook: Curse: Weakness
        new(1055, 15, 0.000000, 0), // Spellbook: Curse: Poison
        new(1053, 15, 0.000000, 0), // Spellbook: Cure Poison
        new(1052, 15, 0.000000, 0), // Spellbook: Flame Strike
        new(1097, 15, 0.000000, 0), // Spellbook: Drain Health
        new(1096, 15, 0.000000, 0), // Spellbook: Elemental Heal
        new(1386, 15, 0.000000, 0), // Spellbook: Disrupt Undead
        new(1514, 15, 0.000000, 0), // Spellbook: Resurrection
        new(1372, 15, 0.000000, 0), // Spellbook: Blaze
        new(1667, 15, 0.000000, 0), // Spellbook: Summon Shadow
        new(1671, 15, 0.000000, 0), // Spellbook: Summon Silhouette
        new(1669, 15, 0.000000, 0), // Spellbook: Summon Boxer the Unicorn
        new(1403, 15, 0.000000, 0), // Spellbook: Summon Kat the Cat
        new(1405, 15, 0.000000, 0), // Spellbook: Servitor Heal
        new(1370, 15, 0.000000, 0), // Spellbook: Aqua Swirl
        new(1401, 15, 0.000000, 0), // Spellbook: Acumen
        new(4916, 15, 0.000000, 0), // Spellbook: Energy Bolt
        new(1411, 15, 0.000000, 0), // Spellbook: Aura Burn
        new(1513, 15, 0.000000, 0), // Spellbook: Charm
        new(1399, 15, 0.000000, 0), // Spellbook: Concentration
        new(1515, 15, 0.000000, 0), // Spellbook: Kiss of Eva
        new(1371, 15, 0.000000, 0), // Spellbook: Twister
        new(1383, 15, 0.000000, 0), // Spellbook: Poison
        new(1377, 15, 0.000000, 0) // Spellbook: Poison Recovery
    };
}