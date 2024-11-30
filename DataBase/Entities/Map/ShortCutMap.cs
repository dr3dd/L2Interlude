using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class ShortCutMap : EntityMap<ShortCutEntity>
    {
        public ShortCutMap()
        {
            Map(i => i.CharacterId).ToColumn("char_id");
            Map(i => i.SlotNum).ToColumn("slotnum");
            Map(i => i.ShortcutType).ToColumn("shortcut_type");
            Map(i => i.ShortcutId).ToColumn("shortcut_id");
            Map(i => i.ShortcutMacro).ToColumn("shortcut_macro");
            Map(i => i.SubjobId).ToColumn("subjob_id");
        }
    }
}