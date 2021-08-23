using Core.Module.ParserEngine;

namespace Core.Module.SkillData
{
    public class SkillAcquireModel
    {
        public string SkillName { get; }
        public int LevelToGetSkill { get; }
        public int LevelUpSp { get; }
        public bool AutoGet { get; }
        public string ItemNeeded { get; } //TODO need implement list of needed items

        public SkillAcquireModel(SkillAcquireBegin acquireBegin)
        {
            SkillName = acquireBegin.SkillName;
            LevelToGetSkill = acquireBegin.GetLevel;
            LevelUpSp = acquireBegin.LevelUpSp;
            AutoGet = acquireBegin.AutoGet;
            ItemNeeded = acquireBegin.ItemNeeded;
        }
    }
}