namespace Core.GeoEngine.Pathfinding.CellNodes;

public class NodeLoc : AbstractNodeLoc
{
    private int _x;
    private int _y;
    private bool _goNorth;
    private bool _goEast;
    private bool _goSouth;
    private bool _goWest;
    private int _geoHeight;
    private GeoEngineInit _geoEngineInit;

    public NodeLoc(GeoEngineInit geoEngineInit, int x, int y, int z)
    {
        _geoEngineInit = geoEngineInit;
        Set(x, y, z);
    }

    public void Set(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _goNorth = _geoEngineInit.CheckNearestNswe(x, y, z, Cell.NSWE_NORTH);
        _goEast = _geoEngineInit.CheckNearestNswe(x, y, z, Cell.NSWE_EAST);
        _goSouth = _geoEngineInit.CheckNearestNswe(x, y, z, Cell.NSWE_SOUTH);
        _goWest = _geoEngineInit.CheckNearestNswe(x, y, z, Cell.NSWE_WEST);
        _geoHeight = _geoEngineInit.GetNearestZ(x, y, z);
    }

    public bool CanGoNorth()
    {
        return _goNorth;
    }

    public bool CanGoEast()
    {
        return _goEast;
    }

    public bool CanGoSouth()
    {
        return _goSouth;
    }

    public bool CanGoWest()
    {
        return _goWest;
    }

    public bool CanGoNone()
    {
        return !CanGoNorth() && !CanGoEast() && !CanGoSouth() && !CanGoWest();
    }

    public bool CanGoAll()
    {
        return CanGoNorth() && CanGoEast() && CanGoSouth() && CanGoWest();
    }

    public override int GetX()
    {
        return _geoEngineInit.GetWorldX(_x);
    }

    public override int GetY()
    {
        return _geoEngineInit.GetWorldY(_y);
    }

    public override int GetZ()
    {
        return _geoHeight;
    }

    public override int GetNodeX()
    {
        return _x;
    }

    public override int GetNodeY()
    {
        return _y;
    }

    public override int GetHashCode()
    {
        int prime = 31;
        int result = 1;
        result = prime * result + _x;
        result = prime * result + _y;

        int nswe = 0;
        if (CanGoNorth())
        {
            nswe |= Cell.NSWE_NORTH;
        }
        if (CanGoEast())
        {
            nswe |= Cell.NSWE_EAST;
        }
        if (CanGoSouth())
        {
            nswe |= Cell.NSWE_SOUTH;
        }
        if (CanGoWest())
        {
            nswe |= Cell.NSWE_WEST;
        }

        result = prime * result + (((_geoHeight & 0xFFFF) << 1) | nswe);
        return result;
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
        if (!(obj is NodeLoc other))
        {
            return false;
        }
        return _x == other._x && _y == other._y && _goNorth == other._goNorth && _goEast == other._goEast && _goSouth == other._goSouth && _goWest == other._goWest && _geoHeight == other._geoHeight;
    }
}
