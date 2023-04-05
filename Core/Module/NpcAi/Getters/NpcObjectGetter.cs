namespace Core.Module.NpcAi.Getters;

public class NpcObjectGetter<T>
{
    private readonly T _npcObject;

    public NpcObjectGetter(T npcObject)
    {
        _npcObject = npcObject;
    }

    public T GetNpcObject()
    {
        return _npcObject;
    }
}