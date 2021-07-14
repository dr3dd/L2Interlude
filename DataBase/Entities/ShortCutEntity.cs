using Dapper.Contrib.Extensions;
namespace DataBase.Entities
{
    [Table("character_shortcuts")]
    public class ShortCutEntity
    {
        public int CharacterObjectId { get; set; }
        public int Slot { get; set; }
        public int Page { get; set; }
        public int Type { get; set; }
        public int ShortcutId { get; set; }
        public int Level { get; set; }
        public int ClassIndex { get; set; }
    }
}