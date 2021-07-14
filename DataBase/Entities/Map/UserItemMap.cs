using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class UserItemMap : EntityMap<UserItemEntity>
    {
        public UserItemMap()
        {
            Map(i => i.CharacterId).ToColumn("char_id");
            Map(i => i.ItemId).ToColumn("item_id");
            Map(i => i.ItemType).ToColumn("item_type");
            Map(i => i.Amount).ToColumn("amount");
            Map(i => i.Enchant).ToColumn("enchant");
        }
    }
}