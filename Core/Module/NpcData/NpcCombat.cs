using System;
using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.NpcData
{
    public class NpcCombat : ICharacterCombat
    {
        private readonly NpcInstance _npcInstance;
        
        public NpcCombat(NpcInstance npcInstance)
        {
            _npcInstance = npcInstance;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public int GetMagicalAttack()
        {
            return (int) _npcInstance.GetTemplate().GetStat().BaseMagicAttack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMagicalDefence()
        {
            float levelBonus = Initializer.PcParameterInit().GetLevelBonus(_npcInstance.GetTemplate().GetStat().Level);
            var baseStat = (int) _npcInstance.GetTemplate().GetStat().BaseMagicDefend;
            
            var menStat = _npcInstance.GetTemplate().GetStat().Men;
            float menBonus = (Initializer.PcParameterInit().GetMenBonus(menStat) + 100) / 100f;
            
            var result = baseStat * menBonus * levelBonus;
            
            var effects = GetNpcEffects();
            return (int) CalculateStats.CalculateMagicalDefence(effects, result);
        }

        public double GetMovementSpeedMultiplier()
        {
            var baseSpeed = GetWalkSpeed();
            return GetCharacterSpeed() / baseSpeed;
        }

        public int GetPhysicalAttack()
        {
            throw new System.NotImplementedException();
        }

        public int GetPhysicalDefence()
        {
            var baseStat = (int) _npcInstance.GetTemplate().GetStat().BaseDefend;
            var effects = GetNpcEffects();
            return (int) CalculateStats.CalculatePhysicalDefence(effects, baseStat);
        }
        
        private IEnumerable<EffectDuration> GetNpcEffects()
        {
            return _npcInstance.CharacterEffect().GetEffects().Values;
        }

        public float GetCollisionRadius()
        {
            return _npcInstance.GetTemplate().GetStat().CollisionRadius[0];
        }

        public float GetCollisionHeight()
        {
            return _npcInstance.GetTemplate().GetStat().CollisionHeight[0];
        }

        public float GetCharacterSpeed()
        {
            return _npcInstance.CharacterMovement().IsRunning() ? GetRunSpeed() : GetWalkSpeed();
        }
        
        public float GetRunSpeed()
        {
            return _npcInstance.GetTemplate().GetStat().GroundHigh[0];
        }

        public float GetWalkSpeed()
        {
            return _npcInstance.GetTemplate().GetStat().GroundLow[0];
        }

        public int GetEvasion()
        {
            var dexStat = _npcInstance.GetTemplate().GetStat().Dex;
            var result = Math.Sqrt(dexStat) * 6 + _npcInstance.GetTemplate().GetStat().Level;
            return (int) result;
        }

        public int GetAccuracy()
        {
            var dexStat = _npcInstance.GetTemplate().GetStat().Dex;
            var weaponAccuracy = _npcInstance.GetTemplate().GetStat().PhysicalHitModify;
            var result = Math.Sqrt(dexStat) * 6 + _npcInstance.GetTemplate().GetStat().Level + weaponAccuracy;
            //result = CalculateStats.CalculateAccuracy(effects, result);
            return (int) result;
        }

        public int GetCriticalRate()
        {
            throw new System.NotImplementedException();
        }
    }
}