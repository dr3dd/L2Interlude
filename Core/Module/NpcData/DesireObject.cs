using Core.Module.CharacterData;

namespace Core.Module.NpcData;
public class DesireObject
{
    public Desire Type { get; set; }
    public Character Target { get; set; }
    public float Priority { get; set; }
    public int ActionId { get; set; }
    public int TimeDesire { get; set; }
    public int StartX { get; set; }
    public int StartY { get; set; }
    public int StartZ { get; set; }

    public DesireObject(Desire type, int actionId, int timeDesire, float priority)
    {
        Type = type;
        ActionId = actionId;
        TimeDesire = timeDesire;
        Priority = priority;
    }

    public DesireObject(Desire type, Character target, float priority)
    {
        Type = type;
        Target = target;
        Priority = priority;
    }

    public DesireObject(Desire type, NpcInstance npcInstance, int startX, int startY, int startZ, int priority)
    {
        Type = type;
        Target = npcInstance;
        StartX = startX;
        StartY = startY;
        StartZ = startZ;
        Priority = priority;
    }
}