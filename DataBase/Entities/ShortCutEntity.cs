using Dapper.Contrib.Extensions;
namespace DataBase.Entities
{
    [Table("shortcut_data")]
    public class ShortCutEntity
    {
        public int CharacterId { get; set; }
        public int SlotNum { get; set; }
        public int ShortcutType { get; set; }
        public int ShortcutId { get; set; }
        public int ShortcutMacro { get; set; }
        public int SubjobId { get; set; }
    }
}