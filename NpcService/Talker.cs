namespace NpcService
{
    public struct Talker
    {
        public int ObjectId { get; set; }
        public int Karma { get; set; }

        public Talker(int objectId, int karma)
        {
            ObjectId = objectId;
            Karma = karma;
        }
    }
}