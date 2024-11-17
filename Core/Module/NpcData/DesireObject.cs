using Core.Module.CharacterData;

namespace Core.Module.NpcData;
public class DesireObject(Desire type, int actionId, int timeDesire, float priority)
{
    public Desire Type { get; set; } = type;
    public Character Target { get; set; }
    public float Priority { get; set; } = priority;
    public int ActionId { get; set; } = actionId;
    public int TimeDesire { get; set; } = timeDesire;

    public DesireObject(Desire type, Character target, float priority) : this(type, 0, 0, priority)
    {
        Target = target;
    }
}