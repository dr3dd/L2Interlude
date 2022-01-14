namespace NpcService
{
    public struct Talker
    {
        public int ObjectId { get; set; }
        public int Karma { get; set; }
        public int Level { get; set; }
        public int Occupation { get; set; }
        public int Race { get; set; }

        public Talker(int objectId, int karma)
        {
            ObjectId = objectId;
            Karma = karma;
            Level = 8;
            Occupation = 1;
            Race = 1;
        }
    }
}