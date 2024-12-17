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
using Core.Module.SettingData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core
{
    public static class CoreDependencyBinder
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddSingleton<ObjectIdInit>();
            services.UseGeoEngine();
            services.AddSingleton<NetworkWriter>();
            services.AddTransient<ClientManager>();
            services.AddSingleton<AreaDataInit>();
            services.AddSingleton<CategoryPchInit>();
            services.AddSingleton<CategoryDataInit>();
            services.AddSingleton<WorldInit>();
            services.AddSingleton<SettingDataInit>();
            services.AddSingleton<TemplateInit>();
            services.AddSingleton<ManualPchInit>();
            services.AddSingleton<FStringInit>();
            services.AddSingleton<PcParameterInit>();
            services.AddSingleton<GameTimeController>();
            services.AddSingleton<HtmlCacheInit>();
            services.AddSingleton<NpcPosInit>();
            services.AddSingleton<NpcPosInitNew>();
            services.AddSingleton<NpcDataInit>();
            services.AddSingleton<NpcAiObj>();
            services.AddSingleton<DoorDataInit>();
            services.AddSingleton<ItemPchInit>();
            services.AddSingleton<QuestPch2Init>();
            services.AddSingleton<QuestPchInit>();
            services.AddSingleton<ItemDataInit>();
            services.AddSingleton<SkillPchInit>();
            services.AddSingleton<SkillDataInit>();
            services.AddSingleton<SkillAcquireInit>();
            services.AddSingleton<EffectInit>();
            services.AddSingleton<ChatHandler>();
            services.AddSingleton<UserCommandHandler>();
            services.AddSingleton<AdminCommandHandler>();
            services.AddSingleton<AdminAccessManager>();
        }
    }
}