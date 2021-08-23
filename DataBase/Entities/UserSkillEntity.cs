namespace DataBase.Entities
{
    public class UserSkillEntity
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
        public int SkillLevel { get; set; }
        public int ToEndTime { get; set; }
    }
}