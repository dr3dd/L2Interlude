using Dapper.FluentMap;
using DataBase.Entities.Map;
using DataBase.Interfaces;
using DataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase
{
    public static class GameDataBaseDependencyBinder
    {
        public static void Bind(IServiceCollection services)
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new CharacterMap());
                config.AddMap(new SpawnListMap());
                config.AddMap(new RaidBossSpawnListMap());
                config.AddMap(new UserItemMap());
                config.AddMap(new UserSkillMap());
                config.AddMap(new SkillTreeMap());
                config.AddMap(new CharacterSkillMap());
                config.AddMap(new ShortCutMap());
                config.AddMap(new UserMacrosMap());
                config.AddMap(new UserQuestMap());
                config.AddMap(new AnnounceMap());
            });
            
            services.AddSingleton<GameConnectionFactory>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<ISpawnListRepository, SpawnListRepository>();
            services.AddTransient<IRaidBossSpawnListRepository, RaidBossSpawnListRepository>();
            services.AddTransient<IUserItemRepository, UserItemRepository>();
            services.AddTransient<IUserSkillRepository, UserSkillRepository>();
            services.AddTransient<ISkillTreeRepository, SkillTreeRepository>();
            services.AddTransient<ICharacterSkillRepository, CharacterSkillRepository>();
            services.AddTransient<IShortCutRepository, ShortCutRepository>();
            services.AddTransient<IMacrosRepository, MacrosRepository>();
            services.AddTransient<IUserQuestRepository, UserQuestRepository>();
            services.AddTransient<IAnnounceRepository, AnnounceRepository>();
            services.AddSingleton<IUnitOfWorkGame, GameUnitOfWork>();
        }
    }
}