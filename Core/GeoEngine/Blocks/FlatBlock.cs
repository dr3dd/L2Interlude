using System;
using System.IO;

namespace Core.GeoEngine.Blocks;

public class FlatBlock : IBlock
{
    private readonly short _height;
    private readonly short _height1;
    public FlatBlock(BinaryReader reader)
    {
        _height1 = reader.ReadInt16();
        _height = reader.ReadInt16();
    }

    public bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return true;
    }

    public void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        throw new Exception("Cannot set NSWE on a flat block!");
    }

    public void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        throw new Exception("Cannot unset NSWE on a flat block!");
    }

    public int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return _height;
    }

    public int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        return _height <= worldZ ? _height : worldZ;
    }

    public int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        return _height >= worldZ ? _height : worldZ;
    }
    
    public short GetHeight()
    {
        return _height;
    }
}
