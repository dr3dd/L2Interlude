using Dapper.Contrib.Extensions;

namespace DataBase.Entities
{
    [Table("user_quest")]
    public class UserQuestEntity
    {
        public int CharacterId { get; set; }
        public int QuestNo { get; set; }
        public int Journal { get; set; }
        public int State1{ get; set; }
        public int State2{ get; set; }
        public int State3{ get; set; }
        public int State4{ get; set; }
        public byte Type{ get; set; }
    }
}