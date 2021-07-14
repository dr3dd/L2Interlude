using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class RaidBossSpawnListMap : EntityMap<RaidBossSpawnListEntity>
    {
        public RaidBossSpawnListMap()
        {
            Map(i => i.BossId).ToColumn("boss_id");
            Map(i => i.LocX).ToColumn("loc_x");
            Map(i => i.LocY).ToColumn("loc_y");
            Map(i => i.LocZ).ToColumn("loc_z");
            Map(i => i.Heading).ToColumn("heading");
            Map(i => i.RespawnMinDelay).ToColumn("respawn_min_delay");
            Map(i => i.RespawnMaxDelay).ToColumn("respawn_max_delay");
            Map(i => i.RespawnTime).ToColumn("respawn_time");
            Map(i => i.CurrentHp).ToColumn("currentHp");
            Map(i => i.CurrentMp).ToColumn("currentMp");
        }
    }
}