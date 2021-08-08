using System;
using Config;
using Core.Controller;
using Core.Module.CharacterData.Template;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public abstract class PlayerBase
    {
        private IServiceProvider _serviceProvider;
        private IServiceCollection _serviceCollection;

        protected PlayerBase()
        {
            InitServices();
        }

        private void InitServices()
        {
            if (_serviceCollection != null) return;
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton<GameTimeController>();
            _serviceCollection.AddSingleton<PcParameterInit>();
            ConfigDependencyBinder.Bind(_serviceCollection);

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProvider.GetRequiredService<PcParameterInit>().Run();
        }

        protected PlayerInstance GetPlayerInstance()
        {
            var templateInit = new TemplateInit(_serviceProvider);
            var playerAppearance = new PlayerAppearance("Test1", "Test1", 0, 0, 0, 0);
            var playerInstance = new PlayerInstance(templateInit.GetTemplateByClassId(0), playerAppearance, _serviceProvider);
            return playerInstance;
        }
    }
}