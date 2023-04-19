using System.Collections.Generic;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class TraderVaranket : MerchantForNewbie
{
    /// <summary>
    /// Accessory (Magic Rings)
    /// </summary>
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new(908, 20, 0.0, 0), // Necklace of Wisdom
        new(909, 20, 0.0, 0), // Blue Diamond Necklace
        new(910, 20, 0.0, 0), // Necklace of Devotion
        new(845, 20, 0.0, 0), // Cat's Eye Earring
        new(846, 20, 0.0, 0), // Coral Earring
        new(847, 20, 0.0, 0), // Red Crescent Earring
        new(877, 20, 0.0, 0), // Ring of Wisdom
        new(878, 20, 0.0, 0), // Blue Coral Ring
        new(890, 20, 0.0, 0), // Ring of Devotion
    };

    /// <summary>
    /// SpellBooks, Amulets
    /// </summary>
    public override IList<BuySellList> SellList1 => new List<BuySellList>
    {
        new(1294, 20, 0.000000, 0), // Spellbook: Defense Aura
        new(1095, 20, 0.000000, 0), // Spellbook: Attack Aura
        new(1048, 20, 0.000000, 0), // Spellbook: Might
        new(1050, 20, 0.000000, 0), // Spellbook: Battle Heal
        new(1051, 20, 0.000000, 0), // Spellbook: Vampiric Touch
        new(1049, 20, 0.000000, 0), // Spellbook: Ice Bolt
        new(1152, 20, 0.000000, 0), // Spellbook: Flame Strike
        new(1054, 20, 0.000000, 0), // Spellbook: Group Heal
        new(1058, 20, 0.000000, 0), // Spellbook: Shield
        new(1099, 20, 0.000000, 0), // Spellbook: Wind Shackle
        new(1098, 20, 0.000000, 0), // Spellbook: Wind Walk
        new(1056, 20, 0.000000, 0), // Spellbook: Curse: Weakness
        new(1055, 20, 0.000000, 0), // Spellbook: Curse: Poison
        new(1053, 20, 0.000000, 0), // Spellbook: Cure Poison
        new(1052, 20, 0.000000, 0), // Spellbook: Flame Strike
        new(1097, 20, 0.000000, 0), // Spellbook: Drain Health
        new(1096, 20, 0.000000, 0), // Spellbook: Elemental Heal
        new(1386, 20, 0.000000, 0), // Spellbook: Disrupt Undead
        new(1514, 20, 0.000000, 0), // Spellbook: Resurrection
        new(1372, 20, 0.000000, 0), // Spellbook: Blaze
        new(1667, 20, 0.000000, 0), // Spellbook: Summon Shadow
        new(1671, 20, 0.000000, 0), // Spellbook: Summon Silhouette
        new(1669, 20, 0.000000, 0), // Spellbook: Summon Boxer the Unicorn
        new(1403, 20, 0.000000, 0), // Spellbook: Summon Kat the Cat
        new(1405, 20, 0.000000, 0), // Spellbook: Servitor Heal
        new(1370, 20, 0.000000, 0), // Spellbook: Aqua Swirl
        new(1401, 20, 0.000000, 0), // Spellbook: Acumen
        new(4916, 20, 0.000000, 0), // Spellbook: Energy Bolt
        new(1411, 20, 0.000000, 0), // Spellbook: Aura Burn
        new(1513, 20, 0.000000, 0), // Spellbook: Charm
        new(1399, 20, 0.000000, 0), // Spellbook: Concentration
        new(1515, 20, 0.000000, 0), // Spellbook: Kiss of Eva
        new(1371, 20, 0.000000, 0), // Spellbook: Twister
        new(1383, 20, 0.000000, 0), // Spellbook: Poison
        new(1377, 20, 0.000000, 0), // Spellbook: Poison Recovery
        new(1512, 20, 0.000000, 0), // Spellbook: Confusion
        new(1379, 20, 0.000000, 0), // Spellbook: Cure Bleeding
        new(1415, 20, 0.000000, 0), // Spellbook: Dryad Root
        new(1388, 20, 0.000000, 0), // Spellbook: Mental Shield
        new(1517, 20, 0.000000, 0), // Spellbook: Body To Mind
        new(4908, 20, 0.000000, 0), // Spellbook: Shadow Spark
        new(1417, 20, 0.000000, 0), // Spellbook: Surrender To Earth
        new(1400, 20, 0.000000, 0), // Spellbook: Surrender To Fire
        new(1418, 20, 0.000000, 0), // Spellbook: Surrender To Poison
        new(1668, 20, 0.000000, 0), // Spellbook: Summon Mew the Cat
        new(1670, 20, 0.000000, 0), // Spellbook: Summon Mirage the Unicorn
        new(1404, 20, 0.000000, 0), // Spellbook: Servitor Recharge
        new(4906, 20, 0.000000, 0), // Spellbook: Solar Spark
        new(1402, 20, 0.000000, 0), // Spellbook: Agility
        new(1391, 20, 0.000000, 0), // Spellbook: Empower
        new(1410, 20, 0.000000, 0), // Spellbook: Poisonous Cloud
        new(1398, 20, 0.000000, 0), // Spellbook: Focus
        new(1389, 20, 0.000000, 0), // Spellbook: Holy Weapon
        new(1378, 20, 0.000000, 0), // Spellbook: Divine Heal
        new(1414, 20, 0.000000, 0), // Spellbook: Resist Fire
        new(1385, 20, 0.000000, 0), // Spellbook: Recharge
        new(4910, 20, 0.000000, 0), // Spellbook: Vampiric Rage
        new(1394, 20, 0.000000, 0), // Spellbook: Sleep
        new(1516, 20, 0.000000, 0), // Spellbook: Corpse Life Drain
        new(1529, 20, 0.000000, 0), // Amulet: Dreaming Spirit
        new(1525, 20, 0.000000, 0), // Amulet: Life Drain
        new(1527, 20, 0.000000, 0), // Amulet: Venom
        new(1524, 20, 0.000000, 0), // Amulet: Soul Shield
        new(1531, 20, 0.000000, 0), // Amulet: Chill Flame
        new(1522, 20, 0.000000, 0), // Amulet: Chant of Battle
        new(1526, 20, 0.000000, 0), // Amulet: Fear
        new(1534, 20, 0.000000, 0), // Amulet: Madness
        new(1537, 20, 0.000000, 0), // Amulet: Seal of Poison
        new(1856, 20, 0.000000, 0), // Amulet: Chant of Life
        new(1523, 20, 0.000000, 0), // Amulet: Chant of Shielding
        new(1521, 20, 0.000000, 0), // Amulet: Chant of Fire
        new(1535, 20, 0.000000, 0), // Amulet: Frost Flame
        new(1532, 20, 0.000000, 0), // Amulet: Blaze Quake
        new(1536, 20, 0.000000, 0), // Amulet: Seal of Binding
        new(1533, 20, 0.000000, 0), // Amulet: Aura Sink
        new(1528, 20, 0.000000, 0), // Amulet: Seal of Chaos
        new(1518, 20, 0.000000, 0), // Amulet: Flame Chant
        new(1519, 20, 0.000000, 0), // Amulet: Pa'agrio's Gift
        new(3038, 20, 0.000000, 0) // Blueprint: Summon Mechanic Golem
    };
}