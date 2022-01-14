namespace NpcService.Model
{
    public class NpcDesire
    {
        public int ObjectId { get; set; }
        public int ActionId { get; set; }
        public int PlayerObjectId { get; set; }
        public ActionDesire ActionDesire { get; set; }
        public int PchSkillId { get; set; }
    }
}