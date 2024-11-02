using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.FStringData;

public class FStringInit(IServiceProvider provider) : BaseParse(provider)
{
    private readonly IParse _parse = new ParseFString();
    private readonly IDictionary<int, string> _fStringCollection = new Dictionary<int, string>();

    public override void Run()
    {
        try
        {
            LoggerManager.Info("FString start...");
            var result = Parse("fstring.txt", _parse);
            foreach (var (key, value) in result.GetResult())
            {
                _fStringCollection.Add(Convert.ToInt32(key), Convert.ToString(value));
            }
        }
        catch (Exception ex)
        {
            LoggerManager.Error(GetType().Name + ": " + ex.Message);
        }
        LoggerManager.Info("Loaded FString: " + _fStringCollection.Count);
    }

    public string GetFString(int id)
    {
        if (_fStringCollection.TryGetValue(id, out var fString))
        {
            return fString;
        }
        LoggerManager.Info("Error FString: No Item Id Found " + id);
        return "Not Found";
    }
}