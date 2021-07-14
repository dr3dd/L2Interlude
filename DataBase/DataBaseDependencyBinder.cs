using Dapper.FluentMap;
using DataBase.Entities.Map;
using DataBase.Interfaces;
using DataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase
{
    public static class DataBaseDependencyBinder
    {
        public static void Bind(IServiceCollection services)
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserAuthMap());
                config.AddMap(new CharacterMap());
                config.AddMap(new SpawnListMap());
                config.AddMap(new RaidBossSpawnListMap());
                config.AddMap(new UserItemMap());
                config.AddMap(new SkillTreeMap());
                config.AddMap(new CharacterSkillMap());
                config.AddMap(new ShortCutMap());
            });
            
            services.AddSingleton<ConnectionFactory>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<ISpawnListRepository, SpawnListRepository>();
            services.AddTransient<IRaidBossSpawnListRepository, RaidBossSpawnListRepository>();
            services.AddTransient<IUserItemRepository, UserUserItemRepository>();
            services.AddTransient<ISkillTreeRepository, SkillTreeRepository>();
            services.AddTransient<ICharacterSkillRepository, CharacterSkillRepository>();
            services.AddTransient<IShortCutRepository, ShortCutRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}