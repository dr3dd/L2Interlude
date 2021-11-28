using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using Core.Module.SkillData.Effects;
using L2Logger;

namespace Core.Module.SkillData
{
    public class SkillDataModel
    {
        public int SkillId { get; }
        public string SkillName { get; }
        public int Level { get; }
        public OperateType OperateType { get; }
        public int MagicLevel { get; set; }
        public IDictionary<string, Effect> Effects { get; }
        public string OperateCond { get; set; }
        public byte IsMagic { get; set; }
        public int MpConsume2 { get; set; }
        public int CastRange { get; set; }
        public int EffectiveRange { get; set; }
        public float SkillHitTime { get; }
        public float SkillCoolTime { get; }
        public float SkillHitCancelTime { get; set; }
        public float ReuseDelay { get; }
        public string Attribute { get; set; }
        public string EffectPoint { get; set; }
        public TargetType TargetType { get; }
        public AbnormalType AbnormalType { get; }
        public int AbnormalTime { get; }
        public string AffectScope { get; set; }
        public string AffectLimit { get; set; }
        public string NextAction { get; set; }
        public string RideState { get; set; }
        public bool IsDeBuff { get; }

        public SkillDataModel(SkillBegin skillBegin, EffectInit effectInit)
        {
            SkillId = skillBegin.SkillId;
            SkillName = skillBegin.SkillName;
            Level = skillBegin.Level;
            OperateType = GetOperateType(skillBegin.OperateType);
            TargetType = GetTargetType(skillBegin.TargetType);
            AbnormalType = GetAbnormalType(skillBegin.AbnormalType);
            AbnormalTime = skillBegin.AbnormalTime;
            SkillHitTime = skillBegin.SkillHitTime;
            SkillCoolTime = skillBegin.SkillCoolTime;
            ReuseDelay = skillBegin.ReuseDelay;
            IsDeBuff = (skillBegin.DeBuff == 1);
            EffectiveRange = skillBegin.EffectiveRange;
            CastRange = skillBegin.CastRange;

            //for debug
            switch (SkillName)
            {
                case "s_wind_walk2":
                case "s_speed_walk1":
                case "s_speed_walk2":
                case "s_song_of_wind":
                case "s_wind_strike11":
                case "s_self_heal":
                case "s_ice_bolt11":
                case "s_heal13":
                case "s_advanced_magic_defence11":
                case "s_song_of_warding":
                case "s_spirit_barrier1":
                case "s_magic_resistance11":
                case "s_song_of_vitality":
                case "s_might1":
                case "s_shield1":
                    Effects = GetEffects(skillBegin.Effect, effectInit);
                    break;
            }
        }

        private IDictionary<string, Effect> GetEffects(IList<string> skillBeginEffect, EffectInit effectInit)
        {
            IDictionary<string, Effect> effects = new Dictionary<string, Effect>();
            try
            {
                foreach (var strEffect in skillBeginEffect)
                {
                    var effectParams = strEffect.Split(";");
                    //var effect = effectInit.GetEffectHandler(effectParams[0]);
                    //var effect = effectInit.GetEffectHandler(effectParams[0]);
                    
                    var effect = (Effect)Activator.CreateInstance(effectInit.GetEffectHandler(effectParams[0]), effectParams, this);
                    effects.Add(effectParams[0], effect);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
            return effects;
        }

        private OperateType GetOperateType(string operateType)
        {
            return operateType switch
            {
                "A1" => OperateType.A1,
                "A2" => OperateType.A2,
                "A3" => OperateType.A3,
                "P" => OperateType.P,
                "T" => OperateType.T,
                _ => throw new ArgumentOutOfRangeException(nameof(operateType), operateType, operateType)
            };
        }

        private TargetType GetTargetType(string targetType)
        {
            return targetType switch
            {
                "none" => TargetType.None,
                "self" => TargetType.Self,
                "enemy_only" => TargetType.EnemyOnly,
                "enemy" => TargetType.Enemy,
                "target" => TargetType.Target,
                "holy_thing" => TargetType.HolyThing,
                "summon" => TargetType.Summon,
                "npc_body" => TargetType.NpcBody,
                "door_treasure" => TargetType.DoorTreasure,
                "pc_body" => TargetType.PcBody,
                "others" => TargetType.Others,
                "item" => TargetType.Item,
                "wyvern_target" => TargetType.WyvernTarget,
                _ => TargetType.None
            };
        }

        private AbnormalType GetAbnormalType(string abnormalType)
        {
            return abnormalType switch
            {
                null => AbnormalType.None,
                "none" => AbnormalType.None,
                "speed_up" => AbnormalType.SpeedUp,
                "speed_up_special" => AbnormalType.SpeedUpSpecial,
                "pa_up" => AbnormalType.PhysicalAttackUp,
                "pa_up_special" => AbnormalType.PhysicalAttackUpSpecial,
                "majesty" => AbnormalType.Majesty,
                "pd_up" => AbnormalType.PhysicalDefence,
                "stun" => AbnormalType.Stun,
                "attack_speed_up_bow" => AbnormalType.AttackSpeedUpBow,
                "speed_down" => AbnormalType.SpeedDown,
                "pd_up_bow" => AbnormalType.PhysicalDefenceUpBow,
                "pa_down" => AbnormalType.PhysicalAttackDown,
                "max_hp_up" => AbnormalType.MaxHpUp,
                "poison" => AbnormalType.Poison,
                "resist_poison" => AbnormalType.ResistPoison,
                "hp_regen_up" => AbnormalType.HpRegenUp,
                "ma_up" => AbnormalType.MagicAttackUp,
                "divine_power" => AbnormalType.DivinePower,
                "sleep" => AbnormalType.Sleep,
                "critical_prob_up" => AbnormalType.CriticalProbUp,
                "cancel_prob_down" => AbnormalType.CancelProbDown,
                "surr_fire" => AbnormalType.SurrenderFire,
                "casting_time_down" => AbnormalType.CastingTimeDown,
                "avoid_up" => AbnormalType.AvoidUp,
                "surr_water" => AbnormalType.SurrenderWater,
                "surr_wind" => AbnormalType.SurrenderWind,
                "root" => AbnormalType.Root,
                "attack_time_up" => AbnormalType.AttackTimeUp,
                "hp_recover" => AbnormalType.HpRecover,
                "hit_down" => AbnormalType.HitDown,
                "surr_earth" => AbnormalType.SurrenderEarth,
                "holy_attack" => AbnormalType.HolyAttack,
                "resist_derangement" => AbnormalType.ResistDerangement,
                "possession" => AbnormalType.Possession,
                "bleeding" => AbnormalType.Bleeding,
                "pinch" => AbnormalType.Pinch,
                "md_up" => AbnormalType.MagicalDefenceUp,
                "max_breath_up" => AbnormalType.MaxBreathUp,
                "turn_flee" => AbnormalType.TurnFlee,
                "dot_attr" => AbnormalType.DotAttr,
                "dot_mp" => AbnormalType.DotMp,
                "pd_up_special" => AbnormalType.PhysicalDefenceUpSpecial,
                "avoid_up_special" => AbnormalType.AvoidUpSpecial,
                "detect_weakness" => AbnormalType.DetectWeakness,
                "dmg_shield" => AbnormalType.DamageShield,
                "turn_passive" => AbnormalType.TurnPassive,
                "pd_down" => AbnormalType.PhysicalDefenceDown,
                "thrill_fight" => AbnormalType.ThrillFight,
                "ab_hawk_eye" => AbnormalType.AbHawkEye,
                "song_of_earth" => AbnormalType.SongOfEarth,
                "song_of_life" => AbnormalType.SongOfLife,
                "song_of_water" => AbnormalType.SongOfWater,
                "song_of_warding" => AbnormalType.SongOfWarding,
                "song_of_wind" => AbnormalType.SongOfWind,
                "song_of_hunter" => AbnormalType.SongOfHunter,
                "song_of_invocation" => AbnormalType.SongOfInvocation,
                "song_of_vitality" => AbnormalType.SongOfVitality,
                "song_of_vengeance" => AbnormalType.SongOfVengeance,
                "song_of_flame_guard" => AbnormalType.SongOfFlameGuard,
                "dance_of_warrior" => AbnormalType.DanceOfWarrior,
                "dance_of_inspiration" => AbnormalType.DanceOfInspiration,
                "dance_of_mystic" => AbnormalType.DanceOfMystic,
                "dance_of_fire" => AbnormalType.DanceOfFire,
                "dance_of_fury" => AbnormalType.DanceOfFury,
                "dance_of_concentration" => AbnormalType.DanceOfConcentration,
                "dance_of_light" => AbnormalType.DanceOfLight,
                "dance_of_aqua_guard" => AbnormalType.DanceOfAquaGuard,
                "paralyze" => AbnormalType.Paralyze,
                "resist_bleeding" => AbnormalType.ResistBleeding,
                "max_mp_up" => AbnormalType.MaxMpUp,
                "silence" => AbnormalType.Silence,
                "attack_time_down" => AbnormalType.AttackTimeDown,
                "hit_up" => AbnormalType.HitUp,
                "critical_dmg_up" => AbnormalType.CriticalDamageUp,
                "shield_prob_up" => AbnormalType.ShieldProbUp,
                "hp_regen_down" => AbnormalType.HpRegenDown,
                "reuse_delay_up" => AbnormalType.ReuseDelayUp,
                "mp_recover" => AbnormalType.MpRecover,
                "decrease_weight_penalty" => AbnormalType.DecreaseWeightPenalty,
                "resist_shock" => AbnormalType.ResistShock,
                "resist_spiritless" => AbnormalType.ResistSpiritless,
                "mp_regen_up" => AbnormalType.MpRegenUp,
                "md_down" => AbnormalType.MagicDefenceDown,
                "vampiric_attack" => AbnormalType.VampiricAttack,
                "heal_effect_down" => AbnormalType.HealEffectDown,
                "duelist_spirit" => AbnormalType.DuelistSpirit,
                "song_of_storm_guard" => AbnormalType.SongOfStormGuard,
                "dance_of_earth_guard" => AbnormalType.DanceOfEarthGuard,
                "dance_of_vampire" => AbnormalType.DanceOfVampire,
                "dance_of_protection" => AbnormalType.DanceOfProtection,
                "snipe" => AbnormalType.Snipe,
                "magic_critical_up" => AbnormalType.MagicCriticalUp,
                "shield_defence_up" => AbnormalType.ShieldDefenceUp,
                "heal_effect_up" => AbnormalType.HealEffectUp,
                "preserve_abnormal" => AbnormalType.PreserveAbnormal,
                "reduce_drop_penalty" => AbnormalType.ReduceDropPenalty,
                "touch_of_life" => AbnormalType.TouchOfLife,
                "touch_of_death" => AbnormalType.TouchOfDeath,
                "song_of_renewal" => AbnormalType.SongOfRenewal,
                "reflect_abnormal" => AbnormalType.ReflectAbnormal,
                "silence_physical" => AbnormalType.SilencePhysical,
                "focus_dagger" => AbnormalType.FocusDagger,
                "multi_debuff" => AbnormalType.MultiDebuff,
                "song_of_meditation" => AbnormalType.SongOfMeditation,
                "song_of_champion" => AbnormalType.SongOfChampion,
                "dance_of_siren" => AbnormalType.DanceOfSiren,
                "dance_of_shadow" => AbnormalType.DanceOfShadow,
                "turn_stone" => AbnormalType.TurnStone,
                "silence_all" => AbnormalType.SilenceAll,
                "ultimate_debuff" => AbnormalType.UltimateDebuff,
                "multi_buff" => AbnormalType.MultiBuff,
                "md_up_attr" => AbnormalType.MagicDefenceUpAttr,
                "resist_holy_unholy" => AbnormalType.ResistHolyUnholy,
                "resist_debuff_dispel" => AbnormalType.ResistDebuffDispel,
                "life_force" => AbnormalType.LifeForce,
                "big_head" => AbnormalType.BigHead,
                "valakas_item" => AbnormalType.ValakasItem,
                "gara" => AbnormalType.Gara,
                "fatal_poison" => AbnormalType.FatalPoison,
                "fly_away" => AbnormalType.FlyAway,
                "antaras_debuff" => AbnormalType.AntarasDebuff,
                "public_slot" => AbnormalType.PublicSlot,
                "rage_might" => AbnormalType.RageMight,
                "ultimate_buff" => AbnormalType.UltimateBuff,
                "ssq_town_curse" => AbnormalType.SsqTownCurse,
                "ssq_town_blessing" => AbnormalType.SsqTownBlessing,
                "watcher_gaze" => AbnormalType.WatcherGaze,
                "spa_disease_a" => AbnormalType.SpaDiseaseA,
                "spa_disease_b" => AbnormalType.SpaDiseaseB,
                "spa_disease_c" => AbnormalType.SpaDiseaseC,
                "spa_disease_d" => AbnormalType.SpaDiseaseD,
                "avoid_down" => AbnormalType.AvoidDown,
                "dragon_breath" => AbnormalType.DragonBreath,
                "big_body" => AbnormalType.BigBody,
                "buff_queen_of_cat" => AbnormalType.BuffQueenOfCat,
                "buff_unicorn_seraphim" => AbnormalType.BuffUnicornSeraphim,
                "debuff_nightshade" => AbnormalType.DebuffNightshade,
                "resist_earth" => AbnormalType.ResistEarth,
                "angelic_icon" => AbnormalType.AngelicIcon,
                "psycho_symphony" => AbnormalType.PsychoSymphony,
                "demonic_blade_dance" => AbnormalType.DemonicBladeDance,
                "rapid_fire" => AbnormalType.RapidFire,
                "dead_eye" => AbnormalType.DeadEye,
                "spirit_of_sagittarius" => AbnormalType.SpiritOfSagittarius,
                "blessing_of_sagittarius" => AbnormalType.BlessingOfSagittarius,
                "infernal_form" => AbnormalType.InfernalForm,
                "betray" => AbnormalType.Betray,
                "arcane_disruption" => AbnormalType.ArcaneDisruption,
                "greater_buff" => AbnormalType.GreaterBuff,
                "resist_holy" => AbnormalType.ResistHoly,
                "resist_unholy" => AbnormalType.ResistUnholy,
                "magical_backfire" => AbnormalType.MagicalBackfire,
                "clarity" => AbnormalType.Clarity,
                "self_resurrection" => AbnormalType.SelfResurrection,
                "meditation" => AbnormalType.Meditation,
                "surr_unholy" => AbnormalType.SurrenderUnholy,
                "surr_holy" => AbnormalType.SurrenderHoly,
                "heroic_miracle" => AbnormalType.HeroicMiracle,
                "heroic_grandeur" => AbnormalType.HeroicGrandeur,
                "bless_of_fire" => AbnormalType.BlessOfFire,
                "bless_of_water" => AbnormalType.BlessOfWater,
                "bless_of_wind" => AbnormalType.BlessOfWind,
                "bless_of_earth" => AbnormalType.BlessOfEarth,
                "bless_of_darkness" => AbnormalType.BlessOfDarknes,
                "bless_of_sacredness" => AbnormalType.BlessOfSacredness,
                "song_of_elemental" => AbnormalType.SongOfElemental,
                "dance_of_alignment" => AbnormalType.DanceOfAlignment,
                "empowering_echo" => AbnormalType.EmpoweringEcho,
                "mana_gain" => AbnormalType.ManaGain,
                "critical_down" => AbnormalType.CriticalDown,
                "berserker" => AbnormalType.Berserker,
                _ => throw new ArgumentOutOfRangeException(nameof(abnormalType), abnormalType, abnormalType)
            };
        }
        
        public override string ToString()
        {
            return SkillId + ": " + SkillName;
        }
    }
}