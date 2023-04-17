using System;
using System.Collections.Generic;
using Core.Module.SkillData.Effects;

namespace Core.Module.SkillData;

public class EffectInit
{
    private readonly Dictionary<string, Type> _handlers;
    public EffectInit()
    {
        _handlers = new Dictionary<string, Type>();

        _handlers.TryAdd("i_p_attack_over_hit", typeof(PAttackOverHit));
        _handlers.TryAdd("i_install_camp", typeof(InstallCamp));
        _handlers.TryAdd("i_randomize_hate", typeof(RandomizeHate));
        _handlers.TryAdd("i_add_hate", typeof(AddHate));
        _handlers.TryAdd("i_fatal_blow", typeof(FatalBlow));
        _handlers.TryAdd("i_dispel_by_slot", typeof(DispelBySlot));
        _handlers.TryAdd("i_target_me_chance", typeof(TargetMeChance));
        _handlers.TryAdd("i_heal", typeof(Heal));
        _handlers.TryAdd("i_m_attack", typeof(MagicAttack));
        _handlers.TryAdd("i_hp_drain", typeof(HpDrain));
        _handlers.TryAdd("i_target_cancel_chance", typeof(TargetCancelChance));
        _handlers.TryAdd("i_hp_per_max", typeof(HpPerMax));
        _handlers.TryAdd("i_fishing_cast", typeof(FishingCast));
        _handlers.TryAdd("i_fishing_pumping", typeof(FishingPumping));
        _handlers.TryAdd("i_fishing_reeling", typeof(FishingReeling));
        _handlers.TryAdd("i_open_dwarf_recipebook", typeof(OpenDwarfRecipebook));
        _handlers.TryAdd("i_open_common_recipebook", typeof(OpenCommonRecipebook));
        _handlers.TryAdd("i_install_camp_ex", typeof(InstallCampEx));
        _handlers.TryAdd("i_restoration", typeof(Restoration));
        _handlers.TryAdd("i_remove_m_power", typeof(RemoveMPower));
        _handlers.TryAdd("i_summon_dd_cubic", typeof(SummonDdCubic));
        _handlers.TryAdd("i_summon_water_dot_cubic", typeof(SummonWaterDotCubic));
        _handlers.TryAdd("i_summon_debuff_cubic", typeof(SummonDebuffCubic));
        _handlers.TryAdd("i_summon", typeof(Summon));
        _handlers.TryAdd("i_consume_body", typeof(ConsumeBody));
        _handlers.TryAdd("i_holything_possess", typeof(HolythingPossess));
        _handlers.TryAdd("i_mp_by_level", typeof(MpByLevel));
        
        _handlers.TryAdd("p_speed", typeof(PSpeed));
        _handlers.TryAdd("p_block_act", typeof(BlockAct));
        _handlers.TryAdd("p_attack_speed_by_weapon", typeof(AttackSpeedByWeapon));
        _handlers.TryAdd("p_magical_defence", typeof(PMagicalDefence));
        _handlers.TryAdd("p_max_hp", typeof(PMaxHp));
        _handlers.TryAdd("p_physical_attack", typeof(PPhysicalAttack));
        _handlers.TryAdd("p_physical_defence", typeof(PPhysicalDefence));
        _handlers.TryAdd("p_avoid", typeof(PAvoid));
        _handlers.TryAdd("p_defence_attribute", typeof(PDefenceAttribute));
        _handlers.TryAdd("p_critical_rate", typeof(PCriticalRate));
        _handlers.TryAdd("p_shield_defence_rate", typeof(PShieldDefenceRate));
        _handlers.TryAdd("p_magic_speed", typeof(PMagicSpeed));
        _handlers.TryAdd("p_reuse_delay", typeof(PReuseDelay));
        _handlers.TryAdd("p_mp_regen", typeof(PMpRegen));
        _handlers.TryAdd("p_critical_damage", typeof(PCriticalDamage));
        _handlers.TryAdd("p_attack_range", typeof(PAttackRange));
        _handlers.TryAdd("p_attack_speed", typeof(PAttackSpeed));
        _handlers.TryAdd("p_hit", typeof(PHit));
        _handlers.TryAdd("p_vampiric_attack", typeof(PVampiricAttack));
        _handlers.TryAdd("p_fishing_mastery", typeof(PFishingMastery));
        _handlers.TryAdd("p_create_common_item", typeof(PCreateCommonItem));
        _handlers.TryAdd("p_preserve_abnormal", typeof(PreserveAbnormal));
        _handlers.TryAdd("p_reduce_drop_penalty", typeof(ReduceDropPenalty));
        _handlers.TryAdd("p_skill_critical", typeof(SkillCritical));
        _handlers.TryAdd("p_enlarge_storage", typeof(EnlargeStorage));
        _handlers.TryAdd("p_luck", typeof(Luck));
        _handlers.TryAdd("p_max_cp", typeof(MaxCp));
        _handlers.TryAdd("p_hp_regen", typeof(HpRegen));
        _handlers.TryAdd("p_max_mp", typeof(MaxMp));
        _handlers.TryAdd("p_hit_number", typeof(HitNumber));
        _handlers.TryAdd("p_remove_equip_penalty", typeof(RemoveEquipPenalty));
        _handlers.TryAdd("p_magical_attack", typeof(MagicalAttack));
        _handlers.TryAdd("p_reduce_cancel", typeof(ReduceCancel));
        _handlers.TryAdd("p_block_move", typeof(BlockMove));
        _handlers.TryAdd("p_hp_regen_by_move_mode", typeof(HpRegenByMoveMode));
        _handlers.TryAdd("p_avoid_by_move_mode", typeof(AvoidByMoveMode));
        _handlers.TryAdd("p_attack_attribute", typeof(AttackAttribute));
        _handlers.TryAdd("p_mp_regen_by_move_mode", typeof(MpRegenByMoveMode));
        _handlers.TryAdd("t_hp", typeof(THp));
        _handlers.TryAdd("c_rest", typeof(Rest));
    }

    public bool HasEffectHandler(string key)
    {
        return _handlers.ContainsKey(key);
    }
        
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