using System;
using Config;
using Core;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.WorldData;
using DataBase;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public abstract class PlayerBase
    {
        private IServiceProvider _serviceProvider;
        private IServiceCollection _serviceCollection;
        private PlayerInstance _playerInstance;

        private void InitServices()
        {
            if (_serviceCollection != null) return;
            _serviceCollection = new ServiceCollection();
            ConfigDependencyBinder.Bind(_serviceCollection);
            DataBaseDependencyBinder.Bind(_serviceCollection);
            CoreDependencyBinder.Bind(_serviceCollection);

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProvider.GetRequiredService<ObjectIdInit>();
            _serviceProvider.GetRequiredService<PcParameterInit>().Run();
            _serviceProvider.GetRequiredService<ItemPchInit>().Run();
            _serviceProvider.GetRequiredService<ItemDataInit>().Run();
            _serviceProvider.GetRequiredService<SkillPchInit>().Run();
            _serviceProvider.GetRequiredService<SkillDataInit>().Run();
            _serviceProvider.GetRequiredService<SkillAcquireInit>().Run();
            _serviceProvider.GetRequiredService<EffectInit>();
        }

        protected PlayerInstance GetPlayerInstance()
        {
            if (_playerInstance != null)
            {
                return _playerInstance;
            }
            InitServices();
            var templateInit = new TemplateInit(_serviceProvider);
            var playerAppearance = new PlayerAppearance("Test1", "Test1", 0, 0, 0, 0);
            _playerInstance = new PlayerInstance(templateInit.GetTemplateByClassId(0), playerAppearance, _serviceProvider);
            return _playerInstance;
        }
    }
}