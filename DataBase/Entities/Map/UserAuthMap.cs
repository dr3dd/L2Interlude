using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class UserAuthMap : EntityMap<UserAuthEntity>
    {
        public UserAuthMap()
        {
            Map(i => i.AccountId).ToColumn("account_id");
            Map(i => i.AccountName).ToColumn("account_name");
            Map(i => i.Password).ToColumn("password");
            Map(i => i.LastLogin).ToColumn("last_login");
            Map(i => i.LastWorld).ToColumn("last_world");
        }
    }
}