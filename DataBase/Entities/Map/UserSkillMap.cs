using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class UserSkillMap : EntityMap<UserSkillEntity>
    {
        public UserSkillMap()
        {
            Map(i => i.CharacterId).ToColumn("char_id");
            Map(i => i.SkillId).ToColumn("skill_id");
            Map(i => i.SkillLevel).ToColumn("skill_level");
            Map(i => i.ToEndTime).ToColumn("to_end_time");
        }
    }
}