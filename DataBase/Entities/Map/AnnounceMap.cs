using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class AnnounceMap : EntityMap<AnnounceEntity>
    {
        public AnnounceMap()
        {
            Map(i => i.AnnounceId).ToColumn("announce_id");
            Map(i => i.AnnounceMsg).ToColumn("announce_msg");
            Map(i => i.Interval).ToColumn("interval");
        }
    }
}
