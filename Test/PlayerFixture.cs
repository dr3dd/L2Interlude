using System;
using Config;
using Core;
using Core.Controller;
using Core.GeoEngine;
using Core.Module.AreaData;
using Core.Module.CharacterData.Template;
using Core.Module.ItemData;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.WorldData;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Test;

public class PlayerInstanceFixture
{
    private readonly IServiceProvider _serviceProvider;
    private PlayerInstance _playerInstance;

    public PlayerInstanceFixture()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        ConfigDependencyBinder.Bind(serviceCollection);
        CoreDependencyBinder.Bind(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _serviceProvider.GetRequiredService<ObjectIdInit>();
        _serviceProvider.GetRequiredService<WorldInit>().Run();
        _serviceProvider.GetRequiredService<AreaDataInit>().Run();
        _serviceProvider.GetRequiredService<GeoEngineInit>();
        _serviceProvider.GetRequiredService<GameTimeController>();
        _serviceProvider.GetRequiredService<PcParameterInit>().Run();
        _serviceProvider.GetRequiredService<ItemPchInit>().Run();
        _serviceProvider.GetRequiredService<ItemDataInit>().Run();
        _serviceProvider.GetRequiredService<SkillPchInit>().Run();
        _serviceProvider.GetRequiredService<SkillDataInit>().Run();
        _serviceProvider.GetRequiredService<SkillAcquireInit>().Run();
        _serviceProvider.GetRequiredService<EffectInit>();
        _serviceProvider.GetRequiredService<TemplateInit>();
        //_serviceProvider.GetRequiredService<NpcDataInit>().Run();
        //_serviceProvider.GetRequiredService<NpcPosInit>().Run();
    }

    /// <summary>
    /// Character Class is Human Fighter
    /// </summary>
    /// <returns></returns>
    public PlayerInstance GetPlayerInstance()
    {
        var mock = new Mock<IUnitOfWork>();
        var templateInit = new TemplateInit(_serviceProvider);
        var playerAppearance = new PlayerAppearance("Test1", "Test1", 0, 0, 0, 0);
        _playerInstance = new PlayerInstance(templateInit.GetTemplateByClassId(0), playerAppearance, _serviceProvider, mock.Object);
        _playerInstance.PlayerInventory().InitBodyParts();
        return _playerInstance;
    }  
}