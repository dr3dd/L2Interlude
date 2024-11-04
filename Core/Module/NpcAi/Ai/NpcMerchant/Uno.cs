using System.Collections.Generic;
using System.Threading.Tasks;
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

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == 162)
        {
            MySelf.SetCurrentQuestID("curse_of_fortress");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        if (ask == 162)
        {
            if (reply == 1)
            {
                MySelf.FHTML_SetFileName(ref fhtml0, "uno_q0314_03.htm");
                MySelf.FHTML_SetInt(ref fhtml0, "quest_id", 162);
                await MySelf.ShowFHTML(talker, fhtml0);
            }
        }
        else
        {
            await MenuSelected(talker, ask, reply, fhtml0);
        }
    }
}