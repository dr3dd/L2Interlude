namespace DataBase.Entities
{
    [Dapper.Contrib.Extensions.Table("spawnlist")]
    
    public class SpawnListEntity
    {
        public int SpawnId { get; set; }

        public int TemplateId { get; set; }
        
        public string SpawnLocation { get; set; }

        public int LocX { get; set; }

        public int LocY { get; set; }

        public int LocZ { get; set; }

        public int Heading { get; set; }

        public int RespawnDelay { get; set; }

        public int RespawnRandX { get; set; }

        public int RespawnRandY { get; set; }

        public byte PeriodOfDay { get; set; }
    }
}