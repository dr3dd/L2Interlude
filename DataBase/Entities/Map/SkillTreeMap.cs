using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class SkillTreeMap : EntityMap<SkillTreeEntity>
    {
        public SkillTreeMap()
        {
            Map(i => i.ClassId).ToColumn("class_id");
            Map(i => i.SkillId).ToColumn("skill_id");
            Map(i => i.Level).ToColumn("level");
            Map(i => i.Name).ToColumn("name");
            Map(i => i.Sp).ToColumn("sp");
            Map(i => i.MinLevel).ToColumn("min_level");
        }
    }
}
