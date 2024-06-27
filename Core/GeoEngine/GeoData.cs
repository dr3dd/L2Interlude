using System.Collections.Concurrent;
using System.IO;
using Core.GeoEngine.Regions;

namespace Core.GeoEngine;

public class GeoData
{
    // World dimensions: 1048576 * 1048576 = 1099511627776
    private const int WORLD_MIN_X = -655360;
    private const int WORLD_MIN_Y = -589824;

    /** Regions in the world on the x axis */
    public const int GEO_REGIONS_X = 32;
    /** Regions in the world on the y axis */
    public const int GEO_REGIONS_Y = 32;
    /** Region in the world */
    public const int GEO_REGIONS = GEO_REGIONS_X * GEO_REGIONS_Y;

    private readonly ConcurrentDictionary<int, RegionAbstract> _regions = new ConcurrentDictionary<int, RegionAbstract>();
    
    public GeoData()
    {
        for (int i = 0; i < GEO_REGIONS; i++)
        {
            _regions[i] = NullRegion.Instance;
        }
    }
    
    public RegionAbstract GetRegion(int geoX, int geoY)
    {
        int index = ((geoX / RegionAbstract.REGION_CELLS_X) * GEO_REGIONS_Y) + (geoY / RegionAbstract.REGION_CELLS_Y);
        return _regions[index];
    }

    public void LoadRegion(string filePath, int regionX, int regionY)
    {
        int regionOffset = (regionX * GEO_REGIONS_Y) + regionY;

        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                for (int i = 0; i < 18; i++)
                {
                    var dd = reader.ReadByte();
                    //Console.WriteLine(dd);
                    //LoggerManager.Info("Byte: " + reader.ReadByte());
                }
                _regions[regionOffset] = new Region(reader);
                //Interlocked.Exchange(ref _regions[regionOffset], new Region(reader));
            }
        }
    }
    
    public void SetRegion(int regionX, int regionY, Region region)
    {
        int regionOffset = (regionX * GEO_REGIONS_Y) + regionY;
        _regions[regionOffset] = region;
    }
    
    public void UnloadRegion(int regionX, int regionY)
    {
        int regionOffset = (regionX * GEO_REGIONS_Y) + regionY;
        _regions[regionOffset] = NullRegion.Instance;
    }
    
    public bool HasGeoPos(int geoX, int geoY)
    {
        return GetRegion(geoX, geoY).HasGeo();
    }

    public bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return GetRegion(geoX, geoY).CheckNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    public void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        GetRegion(geoX, geoY).SetNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    public void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        GetRegion(geoX, geoY).UnsetNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    public int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return GetRegion(geoX, geoY).GetNearestZ(geoX, geoY, worldZ);
    }
    
    public int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        return GetRegion(geoX, geoY).GetNextLowerZ(geoX, geoY, worldZ);
    }
    
    public int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        return GetRegion(geoX, geoY).GetNextHigherZ(geoX, geoY, worldZ);
    }
    
    public int GetGeoX(int worldX)
    {
        return (worldX - WORLD_MIN_X) / 16;
    }
    
    public int GetGeoY(int worldY)
    {
        return (worldY - WORLD_MIN_Y) / 16;
    }
    
    public int GetWorldX(int geoX)
    {
        // checkGeoX(geoX);
        return (geoX * 16) + WORLD_MIN_X + 8;
    }
    
    public int GetWorldY(int geoY)
    {
        return (geoY * 16) + WORLD_MIN_Y + 8;
    }
}