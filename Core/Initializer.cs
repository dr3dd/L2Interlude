using System;
using Config;
using Core.Controller;
using Core.GeoEngine;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.ManualData;
using Core.Module.NpcData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using DataBase.Interfaces;
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

        public static IUnitOfWork UnitOfWork()
        {
            return ServiceProvider.GetService<IUnitOfWork>();
        }

        public static TemplateInit TemplateInit()
        {
            return ServiceProvider.GetService<TemplateInit>();
        }
        
        public static GameTimeController TimeController()
        {
            return ServiceProvider.GetService<GameTimeController>();
        }

        public static GeoEngineInit GeoEngineInit()
        {
            return ServiceProvider.GetService<GeoEngineInit>();
        }

        public void Load()
        {
            ServiceProvider.GetRequiredService<ObjectIdInit>();
            ServiceProvider.GetRequiredService<GameTimeController>().Run();
            ServiceProvider.GetRequiredService<NpcDataInit>().Run();
            ServiceProvider.GetRequiredService<GeoEngineInit>().Run();
            //ServiceProvider.GetRequiredService<NpcPosInit>().Run();
            LoggerManager.Info("----Html Cache----");
            LoggerManager.Info("----Json Teleports----");
            LoggerManager.Info("----Players----");
            ServiceProvider.GetRequiredService<ItemPchInit>().Run();
            ServiceProvider.GetRequiredService<ManualPchInit>().Run();
            ServiceProvider.GetRequiredService<PcParameterInit>().Run();
            ServiceProvider.GetService<TemplateInit>();
            LoggerManager.Info("----Bonus Stats----");
            LoggerManager.Info("----Items----");
            ServiceProvider.GetRequiredService<ItemDataInit>().Run();
           // ServiceProvider.GetService<ItemHandlerInit>();
            LoggerManager.Info("----Skills----");
            ServiceProvider.GetRequiredService<SkillPchInit>().Run();
            ServiceProvider.GetRequiredService<SkillDataInit>().Run();
            ServiceProvider.GetRequiredService<SkillAcquireInit>().Run();
            ServiceProvider.GetRequiredService<EffectInit>();
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