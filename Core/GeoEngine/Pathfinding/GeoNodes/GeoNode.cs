namespace Core.GeoEngine.Pathfinding.GeoNodes;

public class GeoNode : AbstractNode<GeoNodeLoc>
{
    private readonly int _neighborsIdx;
    private short _cost;
    private GeoNode[] _neighbors;

    public GeoNode(GeoNodeLoc loc, int neighborsIdx) : base(loc)
    {
        _neighborsIdx = neighborsIdx;
    }

    public short GetCost()
    {
        return _cost;
    }

    public void SetCost(int cost)
    {
        _cost = (short)cost;
    }

    public GeoNode[] GetNeighbors()
    {
        return _neighbors;
    }

    public void AttachNeighbors(GeoNode[] neighbors)
    {
        _neighbors = neighbors;
    }

    public int GetNeighborsIdx()
    {
        return _neighborsIdx;
    }
}
