using System;
using System.Collections.Generic;
using Core.Module.SkillData.Effects;

namespace Core.Module.SkillData
{
    public class EffectInit
    {
        private readonly Dictionary<string, Type> _handlers;
        public EffectInit()
        {
            _handlers = new Dictionary<string, Type>();

            _handlers.TryAdd("i_p_attack_over_hit", typeof(PAttackOverHit));
            _handlers.TryAdd("p_speed", typeof(PSpeed));
            _handlers.TryAdd("i_fatal_blow", typeof(FatalBlow));
            _handlers.TryAdd("i_dispel_by_slot", typeof(DispelBySlot));
            _handlers.TryAdd("i_target_me_chance", typeof(TargetMeChance));
            _handlers.TryAdd("i_heal", typeof(Heal));
            _handlers.TryAdd("i_m_attack", typeof(MagicAttack));
            _handlers.TryAdd("i_hp_drain", typeof(HpDrain));
            _handlers.TryAdd("p_block_act", typeof(BlockAct));
            _handlers.TryAdd("p_attack_speed_by_weapon", typeof(AttackSpeedByWeapon));
            _handlers.TryAdd("i_target_cancel_chance", typeof(TargetCancelChance));
/*            
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
            */
        }

        /*
        private void RegisterSkillHandler(string key, Effect handler)
        {
            _handlers.Add(key, handler);
        }*/

        public Type GetEffectHandler(string key)
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