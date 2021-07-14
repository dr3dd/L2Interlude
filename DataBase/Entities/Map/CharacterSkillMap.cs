using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class CharacterSkillMap : EntityMap<CharacterSkillEntity>
    {
        public CharacterSkillMap()
        {
            Map(i => i.CharacterObjectId).ToColumn("char_obj_id");
            Map(i => i.SkillId).ToColumn("skill_id");
            Map(i => i.SkillLevel).ToColumn("skill_level");
            Map(i => i.SkillName).ToColumn("skill_name");
            Map(i => i.ClassIndex).ToColumn("class_index");
        }
    }
}
