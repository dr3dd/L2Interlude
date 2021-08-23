namespace Core.Module.ParserEngine
{
    public struct SkillAcquireBegin
    {
        public string SkillName { get; set; }
        public int GetLevel { get; set; }
        public int LevelUpSp { get; set; }
        public bool AutoGet { get; set; }
        public string ItemNeeded { get; set; }
    }
}