using System;

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
        var namespaceName = "Core.Module.NpcAi.Ai.Npc" + char.ToUpper(_npcType[0]) + _npcType.Substring(1);
        var className = namespaceName + "." + _className;
        var objectType = Type.GetType(className);
        if (objectType == null) return default;
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