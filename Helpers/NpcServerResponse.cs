namespace Helpers
{
    public struct NpcServerResponse
    {
        public EventName EventName { get; set; }
        public int PlayerObjectId { get; set; }
        public int NpcObjectId { get; set; }
        public int SocialId { get; set; }
        public string FnHi { get; set; }
    }
}