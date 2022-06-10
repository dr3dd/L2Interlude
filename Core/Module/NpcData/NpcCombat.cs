using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.NpcData
{
    public class NpcCombat
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
    }
}