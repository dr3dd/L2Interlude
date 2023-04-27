using System;
using Config;
using Core;
using Core.Module.CharacterData.Template;
using Core.Module.NpcData;
using Core.Module.SkillData;
using Core.Module.WorldData;
using Microsoft.Extensions.DependencyInjection;

namespace Test.NpcAi;

public class NpcInstanceFixture
{
    private readonly IServiceProvider _serviceProvider;

    public NpcInstanceFixture()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        ConfigDependencyBinder.Bind(serviceCollection);
        CoreDependencyBinder.Bind(serviceCollection);
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _serviceProvider.GetRequiredService<ObjectIdInit>();
        _serviceProvider.GetRequiredService<PcParameterInit>().Run();
        _serviceProvider.GetRequiredService<SkillPchInit>().Run();
        _serviceProvider.GetRequiredService<SkillDataInit>().Run();
        _serviceProvider.GetRequiredService<NpcDataInit>().Run();
    }

    public NpcInstance GetNpcInstance(string npcName)
    {
        var npcDataInit = _serviceProvider.GetRequiredService<NpcDataInit>();
        var npcTemplate = npcDataInit.GetNpcTemplate(npcName);
        var objectIdInit = _serviceProvider.GetRequiredService<ObjectIdInit>();
        return new NpcInstance(objectIdInit.NextObjectId(), npcTemplate, _serviceProvider);
    }
}