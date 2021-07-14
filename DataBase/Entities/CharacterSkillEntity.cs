namespace DataBase.Entities
{
    public class CharacterSkillEntity
    {
        public int CharacterObjectId { get; set; }
        public int SkillId { get; set; }
        public int SkillLevel { get; set; }
        public string SkillName { get; set; }
        public int ClassIndex { get; set; }
    }
}