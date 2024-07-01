namespace Core.GeoEngine.Pathfinding;

public abstract class AbstractNode<TLoc> where TLoc : AbstractNodeLoc
{
    private TLoc _loc;
    private AbstractNode<TLoc> _parent;

    public AbstractNode(TLoc loc)
    {
        _loc = loc;
    }

    public void SetParent(AbstractNode<TLoc> parent)
    {
        _parent = parent;
    }

    public AbstractNode<TLoc> GetParent()
    {
        return _parent;
    }

    public TLoc GetLoc()
    {
        return _loc;
    }

    public void SetLoc(TLoc loc)
    {
        _loc = loc;
    }

    public override int GetHashCode()
    {
        return 31 * 1 + (_loc == null ? 0 : _loc.GetHashCode());
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
        {
            return true;
        }
        if (obj == null)
        {
            return false;
        }
        if (!(obj is AbstractNode<TLoc> other))
        {
            return false;
        }
        if (_loc == null)
        {
            return other._loc == null;
        }
        return _loc.Equals(other._loc);
    }
}
