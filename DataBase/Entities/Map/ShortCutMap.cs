using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class ShortCutMap : EntityMap<ShortCutEntity>
    {
        public ShortCutMap()
        {
            Map(i => i.CharacterObjectId).ToColumn("char_obj_id");
            Map(i => i.Slot).ToColumn("slot");
            Map(i => i.Page).ToColumn("page");
            Map(i => i.Type).ToColumn("type");
            Map(i => i.ShortcutId).ToColumn("shortcut_id");
            Map(i => i.Level).ToColumn("level");
            Map(i => i.ClassIndex).ToColumn("class_index");
        }
    }
}