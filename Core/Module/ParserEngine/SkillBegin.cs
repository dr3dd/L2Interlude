namespace Core.Module.ParserEngine
{
    public struct SkillBegin
    {
        public string SkillName { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }
        public string OperateType { get; set; }
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
        public string TargetType { get; set; }
        public string AffectScope { get; set; }
        public string AffectLimit { get; set; }
        public string NextAction { get; set; }
        public string RideState { get; set; }
    }
}