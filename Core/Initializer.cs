using System;
using Config;
using Core.Module.NpcData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class Initializer
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        
        public Initializer(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        public static GameConfig Config()
        {
            return ServiceProvider.GetService<GameConfig>();
        }

        public void Load()
        {
            ServiceProvider.GetRequiredService<NpcDataInit>().Run();
            //ServiceProvider.GetRequiredService<NpcPosInit>().Run();
            LoggerManager.Info("----Html Cache----");
            LoggerManager.Info("----Json Teleports----");
            LoggerManager.Info("----Players----");
            LoggerManager.Info("----Bonus Stats----");
            LoggerManager.Info("----Items----");
            //ServiceProvider.GetService<ItemInit>();
           // ServiceProvider.GetService<ItemHandlerInit>();
            //LoggerManager.Info("----Parser XML----");
            //ServiceProvider.GetService<Parser>();
            LoggerManager.Info("----Skills----");
            //ServiceProvider.GetService<SkillInit>();
            //ServiceProvider.GetService<SkillTreeInit>();
            //ServiceProvider.GetService<SkillHandlerInit>();
            //ServiceProvider.GetService<SkillSpellBookInit>();
            LoggerManager.Info("----World----");
            LoggerManager.Info("----Npc----");
            //ServiceProvider.GetService<NpcTableInit>();
            //ServiceProvider.GetService<NpcWalkerRouteDataInit>();
            LoggerManager.Info("----Spawn List----");
            //ServiceProvider.GetService<SpawnInit>();
            //ServiceProvider.GetService<RaidBossSpawnManager>();
            LoggerManager.Info("----Zone----");
            //ServiceProvider.GetService<ZoneInit>();
        }
    }
}