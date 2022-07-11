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

        public int GetCharacterSpeed()
        {
            if (_npcInstance.CharacterMovement().IsRunning())
            {
                return GetRunSpeed();
            }
		
            return GetWalkSpeed();
        }
        
        public int GetRunSpeed()
        {
            return (int)_npcInstance.GetTemplate().GetStat().GroundHigh[0];
        }

        public int GetWalkSpeed()
        {
            return (int)_npcInstance.GetTemplate().GetStat().GroundLow[0];
        }

        public int GetEvasion()
        {
            throw new System.NotImplementedException();
        }

        public int GetAccuracy()
        {
            throw new System.NotImplementedException();
        }

        public int GetCriticalRate()
        {
            throw new System.NotImplementedException();
        }
    }
}