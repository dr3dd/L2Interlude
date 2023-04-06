using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using Core.Module.WorldData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Utilities;

namespace Core.Module.DoorData;

public class DoorDataInit : BaseParse
{
    private readonly IParse _parse;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDictionary<string, DoorInstance> _doorDataCollection;
    private readonly ObjectIdInit _objectIdInit;
    
    public DoorDataInit(IServiceProvider provider) : base(provider)
    {
        _doorDataCollection = new Dictionary<string, DoorInstance>();
        _parse = new ParseDoorData();
        _objectIdInit = provider.GetRequiredService<ObjectIdInit>();
        _serviceProvider = provider;
    }

    public override void Run()
    {
        try
        {
            LoggerManager.Info("DoorData start...");
            IResult result = Parse("doordata.txt", _parse);

            foreach (var (key, value) in result.GetResult())
            {
                var doorTemplateInit = new DoorTemplateInit(value as IDictionary<string, object>);
                
                var doorInstance = new DoorInstance(_objectIdInit.NextObjectId(), doorTemplateInit, _serviceProvider);
                _doorDataCollection.Add(key.ToString(), doorInstance);
                if (doorTemplateInit.GetStat().Pos == Arrays.EmptyInts) continue;
                
                var x = doorTemplateInit.GetStat().Pos[0];
                var y = doorTemplateInit.GetStat().Pos[1];
                var z = doorTemplateInit.GetStat().Pos[2];
                doorInstance.SpawnMe(x, y, z);
            }
        } 
        catch (Exception ex)
        {
            LoggerManager.Error(GetType().Name + ": " + ex.Message);
        }
        LoggerManager.Info("Loaded DoorData: " + _doorDataCollection.Count);
    }
    
    public DoorInstance GetDoorInstance(string name)
    {
        return _doorDataCollection[name];
    }
}