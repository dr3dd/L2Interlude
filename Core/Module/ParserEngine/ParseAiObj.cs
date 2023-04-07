using System;
using System.Collections.Generic;
using L2Logger;

namespace Core.Module.ParserEngine;

public class ParseAiObj : IParse
{
    private readonly IResult _result;
    public ParseAiObj()
    {
        _result = new Result();
    }
    
    public void ParseLine(string line)
    {
        try
        {
            if (line.StartsWith("class 1 "))
            {
                var words = line.Split(' ');
                var className = ToPascalCase(words[2]);
                var parentClassName = ToPascalCase(words[4]);

                if (words[2] == "messenger_jacquard")
                {
                    var d = 1;
                }
                var prepareClass = new Dictionary<string, string>
                {
                    ["class_name"] = className,
                    ["parent_class_name"] = parentClassName
                };
                _result.AddItem(words[2], prepareClass);
            }
        }
        catch (Exception ex)
        {
            LoggerManager.Error(GetType().Name + ": " + ex.Message);
        }
    }
    
    private string ToPascalCase(string input)
    {
        string[] words = input.Split('_');
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
        }
        return string.Join("", words);
    }

    public IResult GetResult()
    {
        return _result;
    }
}