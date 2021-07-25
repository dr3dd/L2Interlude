using System;
using Core.Controller;
using Core.Module.CharacterData.Template;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public abstract class PlayerBase
    {
        protected readonly IServiceProvider ServiceProvider;

        protected PlayerBase()
        {
            var services = new ServiceCollection();
            services.AddTransient<GameTimeController>();
            ServiceProvider = services.BuildServiceProvider();
        }

        protected PlayerInstance GetPlayerInstance()
        {
            var templateInit = new TemplateInit();
            var playerAppearance = new PlayerAppearance("Test", "Test", 0, 0, 0, 0);
            var playerInstance = new PlayerInstance(templateInit.GetTemplateByClassId(0), playerAppearance, ServiceProvider);
            return playerInstance;
        }
    }
}