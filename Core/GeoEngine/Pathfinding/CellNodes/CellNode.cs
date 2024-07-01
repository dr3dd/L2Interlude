namespace Core.GeoEngine.Pathfinding.CellNodes;

public class CellNode : AbstractNode<NodeLoc>
{
    private CellNode _next = null;
    private bool _isInUse = true;
    private float _cost = -1000;

    public CellNode(NodeLoc loc) : base(loc) { }

    public bool IsInUse()
    {
        return _isInUse;
    }

    public void SetInUse()
    {
        _isInUse = true;
    }

    public CellNode GetNext()
    {
        return _next;
    }

    public void SetNext(CellNode next)
    {
        _next = next;
    }

    public float GetCost()
    {
        return _cost;
    }

    public void SetCost(double cost)
    {
        _cost = (float)cost;
    }

    public void Free()
    {
        SetParent(null);
        _cost = -1000;
        _isInUse = false;
        _next = null;
    }
}
