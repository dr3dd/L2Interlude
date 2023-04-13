using System;
using Core.Module.NpcAi.Handlers;

namespace Core.Module.NpcAi.Factories;

public class CreateNpcObject<T>
{
    private readonly string _className;
    private readonly string _npcType;
    
    public CreateNpcObject(string className, string npcType)
    {
        _className = className;
        _npcType = npcType;
    }

    /// <summary>
    /// Create NPC AI
    /// </summary>
    /// <returns></returns>
    public T CreateNpc()
    {
        var namespaceName = "Core.Module.NpcAi.Ai.Npc" + NpcHandler.ConvertToPascalCase(_npcType);
        var className = namespaceName + "." + _className;
        var objectType = Type.GetType(className);
        if (objectType == null)
        {
            var defaultClassName = "Core.Module.NpcAi.Ai.DefaultNpc";
            return (T) Activator.CreateInstance(Type.GetType(defaultClassName)!);
        }
        try
        {
            return (T) Activator.CreateInstance(objectType)!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}