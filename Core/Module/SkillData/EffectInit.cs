using System;
using System.Collections.Generic;
using Core.Module.SkillData.Effects;

namespace Core.Module.SkillData
{
    public class EffectInit
    {
        private readonly IDictionary<string, Effect> _handlers;
        public EffectInit()
        {
            _handlers = new Dictionary<string, Effect>();

            RegisterSkillHandler("i_p_attack_over_hit", new PAttackOverHit());
            RegisterSkillHandler("p_speed", new PSpeed());
            RegisterSkillHandler("i_fatal_blow", new FatalBlow());
            RegisterSkillHandler("i_dispel_by_slot", new DispelBySlot());
            RegisterSkillHandler("i_target_me_chance", new TargetMeChance());
            RegisterSkillHandler("i_heal", new Heal());
            RegisterSkillHandler("i_m_attack", new MagicAttack());
            RegisterSkillHandler("i_hp_drain", new HpDrain());
            RegisterSkillHandler("p_block_act", new BlockAct());
            RegisterSkillHandler("p_attack_speed_by_weapon", new AttackSpeedByWeapon());
            RegisterSkillHandler("i_target_cancel_chance", new TargetCancelChance());
            
        }
        
        private void RegisterSkillHandler(string key, Effect handler)
        {
            _handlers.Add(key, handler);
        }

        public Effect GetEffectHandler(string key)
        {
            try
            {
                return _handlers[key];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}