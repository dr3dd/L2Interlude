using System;
using Core.Module.ParserEngine;
using Core.Module.WorldData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData;

public class NpcPosInitNew : BaseParse
{
    private readonly IParse _parse;
    private readonly IServiceProvider _serviceProvider;
    private readonly ObjectIdInit _objectIdInit;
    private readonly NpcDataInit _npcDataInit;
    
    public NpcPosInitNew(IServiceProvider provider) : base(provider)
    {
        _serviceProvider = provider;
        _objectIdInit = provider.GetRequiredService<ObjectIdInit>();
        _npcDataInit = provider.GetRequiredService<NpcDataInit>();
        _parse = new ParseNpcPosNew(new Result());
    }

    public override void Run()
    {
        try
        {
            LoggerManager.Info("NpcPos New start...");
            IResult result = Parse("npcpos.txt", _parse);
            var d = result;
        }
        catch (Exception ex)
        {
            LoggerManager.Error(GetType().Name + ": " + ex.Message);
        }
    }
}