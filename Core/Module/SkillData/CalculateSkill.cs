using System;
using Core.Module.Player;

namespace Core.Module.SkillData
{
    public static class CalculateSkill
    {
        public static double CalcMagicDam(PlayerInstance attacker, PlayerInstance target, int skillDamage, bool ss,
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
            
            double mAtk = attacker.PlayerCombat().GetMagicalAttack();
            double mDef = target.PlayerCombat().GetMagicalDefence();
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