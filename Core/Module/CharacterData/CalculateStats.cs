using System.Collections.Generic;
using System.Linq;
using Core.Module.Player;
using Core.Module.SkillData.Effects;

namespace Core.Module.CharacterData
{
    /// <summary>
    /// /* TODO need calculate stat properly considering diff and per, now calculates only diff */
    /// </summary>
    public static class CalculateStats
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="effects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static float CalculateMagicalDefence(IEnumerable<EffectDuration> effects, float result)
        {
            result = effects.Where(e => e.Effect is PMagicalDefence).Select(effect => (PMagicalDefence)effect.Effect).Aggregate(result,
                (current, effectDefence) => current + (effectDefence.GetMagicalDefence() < 0
                    ? (current * effectDefence.GetMagicalDefence() / 100)
                    : effectDefence.GetMagicalDefence()));
            return result;
        }
        public static float CalculateSpeed(IEnumerable<EffectDuration> effects, float result)
        {
            result = effects.Where(e => e.Effect is PSpeed).Select(effect => (PSpeed)effect.Effect).Aggregate(result,
                (current, effectSpeed) => current + (effectSpeed.GetEffectSpeed() < 0
                    ? (current * effectSpeed.GetEffectSpeed() / 100)
                    : effectSpeed.GetEffectSpeed()));
            return result;
        }

        public static float CalculateMaxHp(IEnumerable<EffectDuration> effects, float result)
        {
            result = effects.Where(e => e.Effect is PMaxHp).Select(effect => (PMaxHp)effect.Effect).Aggregate(result,
                (current, effectMaxHp) => current + (effectMaxHp.GetMaxHp() < 0
                    ? (current * effectMaxHp.GetMaxHp() / 100)
                    : effectMaxHp.GetMaxHp()));
            return result;
        }

        public static float CalculatePhysicalAttack(IEnumerable<EffectDuration> effects, float result)
        {
            result = effects.Where(e => e.Effect is PPhysicalAttack).Select(effect => (PPhysicalAttack)effect.Effect).Aggregate(result,
                (current, effectPhysicalAttack) => current + (effectPhysicalAttack.GetAttackDamage() < 0
                    ? (current * effectPhysicalAttack.GetAttackDamage() / 100)
                    : effectPhysicalAttack.GetAttackDamage()));
            return result;
        }

        public static double CalculatePhysicalDefence(IEnumerable<EffectDuration> effects, double result)
        {
            result = effects.Where(e => e.Effect is PPhysicalDefence).Select(effect => (PPhysicalDefence)effect.Effect).Aggregate(result,
                (current, effectPhysicalAttack) => current + (effectPhysicalAttack.GetPhysicalDefence() < 0
                    ? (current * effectPhysicalAttack.GetPhysicalDefence() / 100)
                    : effectPhysicalAttack.GetPhysicalDefence()));
            return result;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="effects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double CalculateAccuracy(IEnumerable<EffectDuration> effects, double result)
        {
            return 0;
        }
    }
}