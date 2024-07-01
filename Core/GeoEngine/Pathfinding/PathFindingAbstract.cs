using System.Collections.Generic;
using Core.Module.WorldData;

namespace Core.GeoEngine.Pathfinding;

public abstract class PathFindingAbstract
{
    public abstract bool PathNodesExist(short regionOffset);
    public GeoEngineInit GeoEngineInit { get; set; }

    public abstract LinkedList<AbstractNodeLoc> FindPath(int x, int y, int z, int tx, int ty, int tz, int instanceId, bool playable);

    public short GetNodePos(int geoPos)
    {
        return (short)(geoPos >> 3);
    }

    public short GetNodeBlock(int nodePos)
    {
        return (short)(nodePos % 256);
    }

    public byte GetRegionX(int nodePos)
    {
        return (byte)((nodePos >> 8) + World.TileXMin);
    }

    public byte GetRegionY(int nodePos)
    {
        return (byte)((nodePos >> 8) + World.TileYMin);
    }

    public short GetRegionOffset(byte rx, byte ry)
    {
        return (short)((rx << 5) + ry);
    }

    public int CalculateWorldX(short nodeX)
    {
        return Initializer.WorldInit().MapMinX + (nodeX * 128) + 48;
    }

    public int CalculateWorldY(short nodeY)
    {
        return Initializer.WorldInit().MapMinY + (nodeY * 128) + 48;
    }

    public virtual string[] GetStat()
    {
        return null;
    }
}
