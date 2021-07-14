namespace DataBase.Entities
{
    public class SkillTreeEntity
    {
        public int ClassId { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public int Sp { get; set; }
        public int MinLevel { get; set; }
    }
}