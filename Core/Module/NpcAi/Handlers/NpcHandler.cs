using System.Linq;
using Core.Module.NpcAi.Ai;
using Core.Module.NpcAi.Ai.NpcType;
using Core.Module.NpcAi.Factories;
using Core.Module.NpcAi.Getters;

namespace Core.Module.NpcAi.Handlers;

public static class NpcHandler
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static DefaultNpc GetNpcHandler(string npcName, string npcType)
    {
        var spl = npcName.Split("_");
        var className = spl.Aggregate("", (current, s) => current + (char.ToUpper(s[0]) + s.Substring(1)));
        return npcType switch
        {
            "citizen" => HandleCitizen(className, npcType),
            "teleporter" => HandleTeleport(className, npcType),
            "guard" => HandleGuard(className, npcType),
            "warrior" => HandleWarrior(className, npcType),
            _ => HandleCitizen(className, npcType)
        };
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="className"></param>
    /// <param name="npcType"></param>
    private static DefaultNpc HandleWarrior(string className, string npcType)
    {
        var warriorNpc = new CreateNpcObject<Warrior>(className, npcType);
        return new NpcObjectGetter<Warrior>(warriorNpc.CreateNpc()).GetNpcObject();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="className"></param>
    /// <param name="npcType"></param>
    /// <returns></returns>
    private static DefaultNpc HandleGuard(string className, string npcType)
    {
        var guardNpc = new CreateNpcObject<Guard>(className, npcType);
        return new NpcObjectGetter<Guard>(guardNpc.CreateNpc()).GetNpcObject();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="className"></param>
    /// <param name="npcType"></param>
    /// <returns></returns>
    private static DefaultNpc HandleCitizen(string className, string npcType)
    {
        var citizenNpc = new CreateNpcObject<Citizen>(className, npcType);
        return new NpcObjectGetter<Citizen>(citizenNpc.CreateNpc()).GetNpcObject();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="className"></param>
    /// <param name="npcType"></param>
    /// <returns></returns>
    private static DefaultNpc HandleTeleport(string className, string npcType)
    {
        var teleportNpc = new CreateNpcObject<Teleporter>(className, npcType);
        return new NpcObjectGetter<Teleporter>(teleportNpc.CreateNpc()).GetNpcObject();
    }
}