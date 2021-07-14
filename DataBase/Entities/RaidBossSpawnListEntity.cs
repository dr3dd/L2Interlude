namespace DataBase.Entities
{
    [Dapper.Contrib.Extensions.Table("raidboss_spawnlist")]
    public class RaidBossSpawnListEntity
    {
        public int BossId { get; set; }
        public int LocX { get; set; }
        public int LocY { get; set; }
        public int LocZ { get; set; }
        public int Heading { get; set; }
        public int RespawnMinDelay { get; set; }
        public int RespawnMaxDelay { get; set; }
        public long RespawnTime { get; set; }
        public int CurrentHp { get; set; }
        public int CurrentMp { get; set; }
    }
}