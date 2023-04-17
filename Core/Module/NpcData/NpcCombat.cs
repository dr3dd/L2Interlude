using System;
using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.NpcData
{
    public class NpcCombat : ICharacterCombat
    {
        private readonly NpcInstance _npcInstance;
        private readonly NpcStat _stat;
        
        public NpcCombat(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
            _stat = _npcInstance.GetTemplate().GetStat();
        }

        /// <summary>
        /// mAtk = weaponPAtk * mod_lvl * mod_int * mod_per  + mod_diff
        /// Int bonus = Int Bonus + 100 / 100f
        /// Weapon M.Atk * Level Bonus * Int bonus
        /// </summary>
        /// <returns></returns>
        public int GetMagicalAttack()
        {
            var baseMAtk = _stat.BasePhysicalAttack;
            var intBonus = _stat.IntBonus;
            var levelBonus = _stat.LevelBonus;
            
            var modInt = (intBonus + 100) / 100f;
            var result = baseMAtk * levelBonus * modInt;

            var effects = GetNpcEffects();
            result = CalculateStats.CalculatePhysicalAttack(effects, result);
            return (int) Math.Round(result);
        }

        /// <summary>
        /// mDef =  BaseMagicDefend * mod_lvl * mod_men * mod_per  + mod_diff
        /// BaseMagicDefend - base magical defence in pts file
        /// mod_per - skills which are affected on magic defence in per
        /// mod_diff - skills which are affected on magic defence in diff
        /// </summary>
        /// <returns></returns>
        public int GetMagicalDefence()
        {
            var baseMagicDefend = _stat.BaseMagicDefend;
            var levelBonus = _stat.LevelBonus;
            var menBonus = _stat.MenBonus;
            
            var modMen = (menBonus + 100) / 100f;
            var result = baseMagicDefend * levelBonus * modMen;
            var effects = GetNpcEffects();
            result = CalculateStats.CalculateMagicalDefence(effects, result);
            return (int) Math.Round(result);
        }

        public double GetMovementSpeedMultiplier()
        {
            var baseSpeed = GetWalkSpeed();
            return GetCharacterSpeed() / baseSpeed;
        }

        /// <summary>
        /// pAtk = weaponPAtk * mod_lvl * mod_str * mod_per  + mod_diff
        /// Str bonus = Str Bonus + 100 / 100f
        /// Weapon P.Atk * Level Bonus * Str bonus
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalAttack()
        {
            var basePAtk = _stat.BasePhysicalAttack;
            var strBonus = _stat.StrBonus;
            var levelBonus = _stat.LevelBonus;
            
            var modStr = (strBonus + 100) / 100f;
            var result = basePAtk * levelBonus * modStr;

            var effects = GetNpcEffects();
            result = CalculateStats.CalculatePhysicalAttack(effects, result);
            return (int) Math.Round(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalDefence()
        {
            var levelBonus = _stat.LevelBonus;
            var baseDefend = _stat.BaseDefend;
            var result = (4 + baseDefend) * levelBonus;
            var effects = GetNpcEffects();
            result = CalculateStats.CalculatePhysicalDefence(effects, result);
            return (int) Math.Round(result);
        }
        
        private IEnumerable<EffectDuration> GetNpcEffects()
        {
            return _npcInstance.CharacterEffect().GetEffects().Values;
        }

        public float GetCollisionRadius()
        {
            return _stat.CollisionRadius[0];
        }

        public float GetCollisionHeight()
        {
            return _stat.CollisionHeight[0];
        }

        public float GetCharacterSpeed()
        {
            return _npcInstance.CharacterMovement().IsRunning() ? GetRunSpeed() : GetWalkSpeed();
        }
        
        public float GetRunSpeed()
        {
            return _stat.GroundHigh[0];
        }

        public float GetWalkSpeed()
        {
            return _stat.GroundLow[0];
        }

        public int GetEvasion()
        {
            var dexStat = _stat.Dex;
            var result = Math.Sqrt(dexStat) * 6 + _stat.Level;
            return (int) Math.Round(result);
        }

        /// <summary>
        /// Accuracy = ((square root of DEX)*6)+Level+PhysicalHitModify+Passive+Guidance SA+M.Def. Bonus+Buffs
        /// </summary>
        /// <returns></returns>
        public int GetAccuracy()
        {
            var dexStat = _stat.Dex;
            var weaponAccuracy = _stat.PhysicalHitModify;
            var result = Math.Sqrt(dexStat) * 6 + _stat.Level + weaponAccuracy;
            return (int) Math.Round(result);
        }

        /// <summary>
        /// Base Critical = DEX Modifier * Base Critical
        /// Final Critical = Base Critical + Passives + Buffs
        /// </summary>
        /// <returns></returns>
        public int GetCriticalRate()
        {
            var baseCritical = _stat.BaseCritical * 10;
            var dexBonus = _stat.DexBonus;
            var modDex = (dexBonus + 100) / 100f;
            var result = modDex * baseCritical;
            return (int) Math.Round(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetPhysicalAttackSpeed()
        {
            var baseAttackSpeed = _stat.BaseAttackSpeed;
            var dexBonus = _stat.DexBonus;
            var modDex = (dexBonus + 100) / 100f;
            var result = modDex * baseAttackSpeed;
            return (int) Math.Round(result);
        }
    }
}