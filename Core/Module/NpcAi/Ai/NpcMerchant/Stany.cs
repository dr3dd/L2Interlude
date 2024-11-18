using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class Stany : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
		// Mystic Weapons
		new("apprentice_s_staff", 10, 0.000000, 0),  // Journeyman's Staff
		new("relic_of_saints", 10, 0.000000, 0),  // Relic of The Saints
		new("mage_staff", 10, 0.000000, 0),  // Mage Staff
		new("crucifix_of_blessing", 10, 0.000000, 0),  // Crucifix of Blessing
		new("voodoo_doll", 10, 0.000000, 0),  // Voodoo Doll
		new("bone_staff", 10, 0.000000, 0),  // Bone Staff
		new("scroll_of_wisdom", 10, 0.000000, 0),  // Scroll of Wisdom
		new("sword_of_priest", 10, 0.000000, 0),  // Priest Sword
		new("branch_of_life", 10, 0.000000, 0),  // Branch of Life
		new("proof_of_revenge", 10, 0.000000, 0),  // Proof of Revenge
		new("mace_of_prayer", 10, 0.000000, 0),  // Mace of Prayer
		new("doom_hammer", 10, 0.000000, 0),  // Doom Hammer
		new("mystic_staff", 10, 0.000000, 0),  // Mystic Staff
		new("staff_of_mana", 10, 0.000000, 0),  // Staff of Mana
		new("divine_tome", 10, 0.000000, 0),  // Divine Tome
		new("sword_of_magic", 10, 0.000000, 0),  // Sword of Magic
		new("sword_of_mystic", 10, 0.000000, 0),  // Sword of Mystic
		new("sword_of_occult", 10, 0.000000, 0),  // Sword of Occult
		new("dagger_of_mana", 10, 0.000000, 0),  // Dagger of Mana
		new("mystic_knife", 10, 0.000000, 0),  // Mystic Knife
		new("conjure_knife", 10, 0.000000, 0),  // Conjurer's Knife
		new("knife_o__silenus", 10, 0.000000, 0),  // Shilen Knife
		new("staff_of_magicpower", 10, 0.000000, 0),  // Staff of Magic
		new("blood_of_saints", 10, 0.000000, 0),  // Blood of Saint
		new("tome_of_blood", 10, 0.000000, 0),  // Tome of Blood
		new("goathead_staff", 10, 0.000000, 0),  // Goat Head Staff
		new("sword_of_magic_fog", 10, 0.000000, 0),  // Sword of Magic Fog
		new("mace_of_priest", 10, 0.000000, 0),  // Priest Mace
		new("crucifix_of_blood", 10, 0.000000, 0),  // Crucifix of Blood
		new("demon_fangs", 10, 0.000000, 0),  // Demon Fangs
		new("wooden_arrow", 10, 0.000000, 0),  // Wooden Arrow
		new("bone_arrow", 10, 0.000000, 0),  // Bone Arrow
		new("fine_steel_arrow", 10, 0.000000, 0)  // Fine Steel Arrow
	};
}