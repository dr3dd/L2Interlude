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
            services.AddSingleton<IUnitOfWorkGame, GameUnitOfWork>();
        }
    }
}