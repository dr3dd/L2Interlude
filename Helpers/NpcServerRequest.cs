namespace Helpers
{
    public class NpcServerRequest
    {
        public EventName EventName { get; set; }
        public string NpcName { get; set; }
        public string NpcType { get; set; }
        public int PlayerObjectId { get; set; }
        public string Race { get; set; }
        public int NpcObjectId { get; set; }
        public short MoveAroundSocial { get; set; }
        public short MoveAroundSocial1 { get; set; }
        public short MoveAroundSocial2 { get; set; }
        public string FnHi { get; set; }
        public string FnNobless { get; set; }
        public string FnNoNobless { get; set; }
        public string FnNoNoblessItem { get; set; }
        public string FnYouAreChaotic { get; set; }
    }
}
