namespace Core.Module.NpcAi
{
    public struct Talker
    {
        public int ObjectId { get; set; }
        public int Karma { get; set; }
        public int Level { get; set; }
        public int Occupation { get; set; }
        public int Race { get; set; }

        public Talker(int objectId, int level)
        {
            ObjectId = objectId;
            Karma = 0;
            Level = level;
            Occupation = 1;
            Race = 1;
        }
    }
}