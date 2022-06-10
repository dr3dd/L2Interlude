using System;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.SkillData
{
    public static class CalculateSkill
    {
        public static double CalcMagicDam(PlayerInstance attacker, Character target, int skillDamage, bool ss,
            bool bss, bool mCrit)
        {
            double damage = 1;
            // Add Matk/Mdef Bonus
            int ssModifier = 1;
            if (bss)
            {
                ssModifier = 4;
            }
            else if (ss)
            {
                ssModifier = 2;
            }
            
            double mAtk = attacker.GetMagicalAttack();
            double mDef = target.GetMagicalDefence();
            // apply ss bonus
            mAtk *= ssModifier;
            
            damage = ((91 * Math.Sqrt(mAtk)) / mDef) * skillDamage;
            if (mCrit)
            {
                damage *= 3;
            }
            return damage;
        }
    }
}