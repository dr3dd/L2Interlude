using Core.Module.WorldData;

namespace Core.GeoEngine.Pathfinding.GeoNodes;

public class GeoNodeLoc : AbstractNodeLoc
{
    private readonly short _x;
    private readonly short _y;
    private readonly short _z;

    public GeoNodeLoc(short x, short y, short z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public override int GetX()
    {
        return Initializer.WorldInit().MapMinX + (_x * 128) + 48;
    }

    public override int GetY()
    {
        return Initializer.WorldInit().MapMinY + (_y * 128) + 48;
    }

    public override int GetZ()
    {
        return _z;
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
        result = prime * result + _z;
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
        if (!(obj is GeoNodeLoc))
        {
            return false;
        }
        var other = (GeoNodeLoc)obj;
        return _x == other._x && _y == other._y && _z == other._z;
    }
}
