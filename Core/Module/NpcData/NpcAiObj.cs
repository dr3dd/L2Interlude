using System;
using System.Collections.Generic;
using System.IO;
using Core.Module.ParserEngine;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.NpcData;

public class NpcAiObj : BaseParse
{
    private readonly IParse _parse;
    private readonly IDictionary<string, string> _dataCollection;
    private readonly NpcDataInit _npcDataInit; 
    
    public NpcAiObj(IServiceProvider provider) : base(provider)
    {
        _parse = new ParseAiObj();
        _npcDataInit = provider.GetRequiredService<NpcDataInit>();
        _dataCollection = new Dictionary<string, string>();
    }

    public override void Run()
    {
        try
        {
            LoggerManager.Info("NpcAiObj start...");
            IResult result = Parse("ai.obj", _parse);

            var res = result.GetResult();
            foreach (var (key, value) in res)
            {
                var npcName = key.ToString();
                _dataCollection.Add(npcName, npcName);
                if (npcName == "messenger_jacquard")
                {
                    var d = 1;
                }
                
                var npcValue = value as IDictionary<string, string>;
                var templateName = npcName;
                var className = npcValue?["class_name"];
                var parentClassName = npcValue?["parent_class_name"];
                string fileContent = "namespace {namespaceName};\n\npublic class {className} : {parentClassName}\n{\n}";

                string replace = "";
                string filePath = "";
                if (_npcDataInit.IsNpcTemplateExist(templateName))
                {
                    var npcTemplate = _npcDataInit.GetNpcTemplate(templateName);
                    var npcType = npcTemplate.GetStat().Type;

                    var npcDirectoryName = "Npc" + char.ToUpper(npcType[0]) + npcType.Substring(1);
                    var namespaceName = "Core.NpcAiTest.Ai." + npcDirectoryName;
                    
                    string path = @"C:/Users/Viacheslav/RiderProjects/L2Interlude/Core/NpcAiTest/Ai/" + npcDirectoryName + "/" ;
                    if (File.Exists(path + className + ".cs"))
                    {
                        continue;
                    }
                    filePath = path + className + ".cs";
                    replace = fileContent.Replace("{namespaceName}", namespaceName);
                    replace = replace.Replace("{className}", className);
                    replace = replace.Replace("{parentClassName}", parentClassName);
                    
                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath!);
                    }
                }
                else
                {
                    //create class in main directory
                    var namespaceName = "Core.NpcAiTest.Ai";
                    string path = @"C:/Users/Viacheslav/RiderProjects/L2Interlude/Core/NpcAiTest/Ai/";
                    if (File.Exists(path + className + ".cs"))
                    {
                        continue;
                    }
                    filePath = path + className + ".cs";
                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath!);
                    }
                    replace = fileContent.Replace("{namespaceName}", namespaceName);
                    replace = replace.Replace("{className}", className);
                    replace = replace.Replace("{parentClassName}", parentClassName);
                }
                File.WriteAllText(filePath, replace);
            }
        }
        catch (Exception ex)
        {
            LoggerManager.Error(GetType().Name + ": " + ex.Message);
        }
        LoggerManager.Info("Loaded NpcAiObj: " + _dataCollection.Count);
    }
}