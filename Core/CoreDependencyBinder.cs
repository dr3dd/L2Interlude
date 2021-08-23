﻿using Core.Controller;
using Core.GeoEngine;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.ManualData;
using Core.Module.NpcData;
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
            services.AddSingleton<TemplateInit>();
            services.AddSingleton<ManualPchInit>();
            services.AddSingleton<PcParameterInit>();
            services.AddSingleton<GameTimeController>();
            services.AddSingleton<NpcDataInit>();
            services.AddSingleton<ItemPchInit>();
            services.AddSingleton<ItemDataInit>();
            services.AddSingleton<SkillPchInit>();
            services.AddSingleton<SkillDataInit>();
            services.AddSingleton<SkillAcquireInit>();
        }
    }
}