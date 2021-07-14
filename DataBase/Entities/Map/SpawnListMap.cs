using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class SpawnListMap : EntityMap<SpawnListEntity>
    {
        public SpawnListMap()
        {
            Map(i => i.SpawnId).ToColumn("id");
            Map(i => i.SpawnLocation).ToColumn("location");
            Map(i => i.TemplateId).ToColumn("npc_templateid");
            Map(i => i.LocX).ToColumn("locx");
            Map(i => i.LocY).ToColumn("locy");
            Map(i => i.LocZ).ToColumn("locz");
            Map(i => i.RespawnRandX).ToColumn("randomx");
            Map(i => i.RespawnRandY).ToColumn("randomy");
            Map(i => i.Heading).ToColumn("heading");
            Map(i => i.RespawnDelay).ToColumn("respawn_delay");
            Map(i => i.PeriodOfDay).ToColumn("periodOfDay");
        }
    }
}