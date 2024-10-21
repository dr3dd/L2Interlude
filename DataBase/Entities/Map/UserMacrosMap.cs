using Dapper.FluentMap.Mapping;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 23:09:09

namespace DataBase.Entities.Map
{
    public class UserMacrosMap : EntityMap<UserMacrosEntity>
    {
        public UserMacrosMap()
        {
            Map(i => i.MacrosId).ToColumn("macros_id");
            Map(i => i.UserMacrosId).ToColumn("user_macros_id");
            Map(i => i.CharacterObjectId).ToColumn("char_id");
            Map(i => i.Icon).ToColumn("icon");
            Map(i => i.Name).ToColumn("name");
            Map(i => i.Description).ToColumn("description");
            Map(i => i.Acronym).ToColumn("acronym");
            Map(i => i.Commands).ToColumn("commands");
        }
    }
}
