using System;

namespace Core.GeoEngine.Regions;

public class NullRegion : RegionAbstract
{
    public static NullRegion Instance { get; set; } = new();

    public override bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return true;
    }

    public override void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        throw new NotImplementedException();
    }

    public override void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        throw new NotImplementedException();
    }

    public override int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return worldZ;
    }

    public override int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        return worldZ;
    }

    public override int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        return worldZ;
    }

    public override bool HasGeo()
    {
        return false;
    }

    public override bool SaveToFile(string fileName)
    {
        return false;
    }
}