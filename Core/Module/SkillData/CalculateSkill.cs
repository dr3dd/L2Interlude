using System;
using Core.Module.CharacterData;
using Core.Module.ItemData;
using Helpers;

namespace Core.Module.SkillData
{
    public static class CalculateSkill
    {
        public static double CalcMagicDam(Character attacker, Character target, int skillDamage, bool ss,
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
        
        public static int CalcPAtkSpd(double rate)
        {
            if (rate < 2)
            {
                return 2700;
            }
            return (int) (470000 / rate);
        }
        
        public static bool CalcHitMiss(Character attacker, Character target)
        {
            var chance = (80 + (2 * (attacker.CharacterCombat().GetAccuracy() - target.CharacterCombat().GetEvasion()))) * 10;
            // Get additional bonus from the conditions when you are attacking
            
            //chance *= hitConditionBonus.getConditionBonus(attacker, target); TODO
            chance = Math.Max(chance, 200);
            chance = Math.Min(chance, 980);
            return chance < Rnd.Next(1000);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="character"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool CalcShieldUse(Character character, Character target)
        {
            return false;
        }
        
        public static bool CalcCrit(double rate)
        {
            return rate > Rnd.Next(1000);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <param name="shld"></param>
        /// <param name="crit"></param>
        /// <param name="dual"></param>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static double CalcPhysDam(Character attacker, Character target, bool shld,
            bool crit, bool dual, bool ss)
        {
            double damage = attacker.CharacterCombat().GetPhysicalAttack();
            double defence = target.CharacterCombat().GetPhysicalDefence();
            if (ss) 
            {
                damage *= 2;
            }
            //TMP
            if (crit) 
            {
                damage *= 2;
            }
            // defence modifier depending of the attacker weapon
            var weapon = attacker.GetActiveWeaponItem();
            
            damage = (70 * damage) / defence;
            damage += (Rnd.NextDouble() * damage) / 10;
            return Math.Round(damage);
        }
    }
}