using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class UserQuestMap : EntityMap<UserQuestEntity>
    {
        public UserQuestMap()
        {
            Map(i => i.CharacterId).ToColumn("char_id");
            Map(i => i.QuestNo).ToColumn("quest_no");
            Map(i => i.Journal).ToColumn("journal");
            Map(i => i.State1).ToColumn("state1");
            Map(i => i.State2).ToColumn("state2");
            Map(i => i.State3).ToColumn("state3");
            Map(i => i.State4).ToColumn("state4");
            Map(i => i.Type).ToColumn("type");
        }
    }
}
