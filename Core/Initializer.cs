using System;
using System.Threading.Tasks;
using Config;
using Core.Controller;
using Core.GeoEngine;
using Core.Manager;
using Core.Module.AreaData;
using Core.Module.CategoryData;
using Core.Module.CharacterData.Template;
using Core.Module.DoorData;
using Core.Module.FStringData;
using Core.Module.Handlers;
using Core.Module.HtmlCacheData;
using Core.Module.ItemData;
using Core.Module.ManualData;
using Core.Module.NpcData;
using Core.Module.QuestData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using DataBase.Interfaces;
using Helpers;
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

        public static IUnitOfWorkGame UnitOfWork()
        {
            return ServiceProvider.GetService<IUnitOfWorkGame>();
        }

        public static TemplateInit TemplateInit()
        {
            return ServiceProvider.GetService<TemplateInit>();
        }
        
        public static GameTimeController TimeController()
        {
            return ServiceProvider.GetService<GameTimeController>();
        }

        public static HtmlCacheInit HtmlCacheInit()
        {
            return ServiceProvider.GetService<HtmlCacheInit>();
        }

        public static GeoEngineInit GeoEngineInit()
        {
            return ServiceProvider.GetService<GeoEngineInit>();
        }
        
        internal static WorldInit WorldInit()
        {
            return ServiceProvider.GetService<WorldInit>();
        }
        internal static ItemDataInit ItemDataInit()
        {
            return ServiceProvider.GetService<ItemDataInit>();
        }
        internal static QuestPchInit QuestPchInit()
        {
            return ServiceProvider.GetService<QuestPchInit>();
        }
        internal static NpcDataInit NpcDataInit()
        {
            return ServiceProvider.GetService<NpcDataInit>();
        }
        
        internal static ChatHandler ChatHandler()
        {
            return ServiceProvider.GetService<ChatHandler>();
        }
        internal static AdminAccessManager AdminAccessManager()
        {
            return ServiceProvider.GetService<AdminAccessManager>();
        }
        internal static AdminCommandHandler AdminCommandHandler()
        {
            return ServiceProvider.GetService<AdminCommandHandler>();
        }

        public static SkillPchInit SkillPchInit()
        {
            return ServiceProvider.GetService<SkillPchInit>();
        }
        
        public static SkillDataInit SkillDataInit()
        {
            return ServiceProvider.GetService<SkillDataInit>();
        }
        
        public static SkillAcquireInit SkillAcquireInit()
        {
            return ServiceProvider.GetService<SkillAcquireInit>();
        }
        
        public static async Task SendMessageToNpcService(NpcServerRequest npcServerRequest)
        {
            await ServiceProvider.GetRequiredService<NpcServiceController>().SendMessageToNpcService(npcServerRequest);
        }

        public static PcParameterInit PcParameterInit()
        {
            return ServiceProvider.GetService<PcParameterInit>();
        }

        public static FStringInit FStringInit()
        {
            return ServiceProvider.GetRequiredService<FStringInit>();
        }

        public void Load()
        {
            ServiceProvider.GetRequiredService<ObjectIdInit>();
            ServiceProvider.GetRequiredService<GameTimeController>().Run();
            ServiceProvider.GetRequiredService<CategoryPchInit>().Run();
            ServiceProvider.GetRequiredService<CategoryDataInit>().Run();
            ServiceProvider.GetRequiredService<PcParameterInit>().Run();
            ServiceProvider.GetRequiredService<WorldInit>().Run();
            ServiceProvider.GetRequiredService<AreaDataInit>().Run();
            ServiceProvider.GetRequiredService<NpcDataInit>().Run();
            ServiceProvider.GetRequiredService<FStringInit>().Run();
            //ServiceProvider.GetRequiredService<NpcAiObj>().Run(); use only once 
            ServiceProvider.GetRequiredService<DoorDataInit>().Run();
            //ServiceProvider.GetRequiredService<NpcPosInitNew>().Run();
            ServiceProvider.GetRequiredService<NpcPosInit>().Run();
            ServiceProvider.GetRequiredService<GeoEngineInit>().Run();
            LoggerManager.Info("----Html Cache----");
            ServiceProvider.GetRequiredService<HtmlCacheInit>().Run();
            LoggerManager.Info("----Json Teleports----");
            LoggerManager.Info("----Players----");
            ServiceProvider.GetRequiredService<ItemPchInit>().Run();
            ServiceProvider.GetRequiredService<QuestPchInit>().Run();
            ServiceProvider.GetRequiredService<ManualPchInit>().Run();
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
            ServiceProvider.GetRequiredService<ChatHandler>();
            ServiceProvider.GetRequiredService<AdminAccessManager>();
            ServiceProvider.GetRequiredService<AdminCommandHandler>();
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