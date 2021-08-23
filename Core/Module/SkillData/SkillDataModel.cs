using System;
using Core.Module.ParserEngine;

namespace Core.Module.SkillData
{
    public class SkillDataModel
    {
        public int SkillId { get; }
        public string SkillName { get; }
        public int Level { get; }
        public OperateType OperateType { get; set; }
        public int MagicLevel { get; set; }
        public string Effect { get; set; }
        public string OperateCond { get; set; }
        public byte IsMagic { get; set; }
        public int MpConsume2 { get; set; }
        public int CastRange { get; set; }
        public int EffectiveRange { get; set; }
        public float SkillHitTime { get; set; }
        public float SkillCoolTime { get; set; }
        public float SkillHitCancelTime { get; set; }
        public float ReuseDelay { get; set; }
        public string Attribute { get; set; }
        public string EffectPoint { get; set; }
        public TargetType TargetType { get; }
        public string AffectScope { get; set; }
        public string AffectLimit { get; set; }
        public string NextAction { get; set; }
        public string RideState { get; set; }

        public SkillDataModel(SkillBegin skillBegin)
        {
            SkillId = skillBegin.SkillId;
            SkillName = skillBegin.SkillName;
            Level = skillBegin.Level;
            OperateType = GetOperateType(skillBegin.OperateType);
            TargetType = GetTargetType(skillBegin.TargetType);
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
        
        public override string ToString()
        {
            return SkillId + ": " + SkillName;
        }
    }
}