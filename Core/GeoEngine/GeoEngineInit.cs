using System;
using System.IO;
using Config;
using Core.GeoEngine.Pathfinding.CellNodes;
using Core.GeoEngine.Regions;
using Core.Module.CharacterData;
using Core.Module.DoorData;
using Core.Module.WorldData;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.GeoEngine;

public class GeoEngineInit
{
    private readonly string _basePath;
    private readonly WorldInit _worldInit;
    private static int ELEVATED_SEE_OVER_DISTANCE = 2;
    private static int MAX_SEE_OVER_HEIGHT = 48;
    private static int SPAWN_Z_DELTA_LIMIT = 100;

    private const int TILE_X_MIN = 16;
    private const int TILE_X_MAX = 26;
    private const int TILE_Y_MIN = 10;
    private const int TILE_Y_MAX = 25;
        
    private GeoData _geodata;
    private CellPathFinding _pathFinding;

    private IServiceProvider _provider; 
    
    public GeoEngineInit(IServiceProvider provider)
    {
        _basePath = provider.GetRequiredService<GameConfig>().ServerConfig.StaticData;
        LoggerManager.Info("GeoEngine: Initializing...");
        // Initialize block container.
        _worldInit = provider.GetRequiredService<WorldInit>();
        _geodata = new GeoData();
        _provider = provider;
        _pathFinding = new CellPathFinding(this);
    }

    public WorldInit GetWorldInit() => _worldInit;

    public CellPathFinding CellPathFinding() => _pathFinding;

    public void Run()
    {
        int loaded = 0;
        try
        {
            for (int regionX = TILE_X_MIN; regionX <= TILE_X_MAX; regionX++)
            {
                for (int regionY = TILE_Y_MIN; regionY <= TILE_Y_MAX; regionY++)
                {
                    var geoFilePath = _basePath + $"/GeoData/{regionX}_{regionY}_conv.dat";
                    if (!File.Exists(geoFilePath))
                    {
                        continue;
                    }
                    _geodata.LoadRegion(geoFilePath, regionX, regionY);
                    loaded++;
                }
            }
        }
        catch (Exception ex)
        {
            LoggerManager.Error(ex.Message);
        }
        LoggerManager.Info("GeoEngine: Loaded " + loaded + " GeoData files.");
    }

    /// <summary>
    /// Check if geo coordinates has geo.
    /// </summary>
    /// <param name="geoX"></param>
    /// <param name="geoY"></param>
    /// <returns></returns>
    public bool HasGeoPos(int geoX, int geoY)
    {
        return _geodata.HasGeoPos(geoX, geoY);
    }
        
    public bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return _geodata.CheckNearestNswe(geoX, geoY, worldZ, nswe);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoX"></param>
    /// <param name="geoY"></param>
    /// <param name="worldZ"></param>
    /// <param name="nswe"></param>
    /// <returns></returns>
    public bool CheckNearestNsweAntiCornerCut(int geoX, int geoY, int worldZ, int nswe)
    {
        bool can = true;
        if ((nswe & Cell.NSWE_NORTH_EAST) == Cell.NSWE_NORTH_EAST)
        {
            // can = canEnterNeighbors(prevX, prevY - 1, prevGeoZ, Direction.EAST) && canEnterNeighbors(prevX + 1, prevY, prevGeoZ, Direction.NORTH);
            can = CheckNearestNswe(geoX, geoY - 1, worldZ, Cell.NSWE_EAST) && CheckNearestNswe(geoX + 1, geoY, worldZ, Cell.NSWE_NORTH);
        }
		
        if (can && ((nswe & Cell.NSWE_NORTH_WEST) == Cell.NSWE_NORTH_WEST))
        {
            // can = canEnterNeighbors(prevX, prevY - 1, prevGeoZ, Direction.WEST) && canEnterNeighbors(prevX - 1, prevY, prevGeoZ, Direction.NORTH);
            can = CheckNearestNswe(geoX, geoY - 1, worldZ, Cell.NSWE_WEST) && CheckNearestNswe(geoX, geoY - 1, worldZ, Cell.NSWE_NORTH);
        }
		
        if (can && ((nswe & Cell.NSWE_SOUTH_EAST) == Cell.NSWE_SOUTH_EAST))
        {
            // can = canEnterNeighbors(prevX, prevY + 1, prevGeoZ, Direction.EAST) && canEnterNeighbors(prevX + 1, prevY, prevGeoZ, Direction.SOUTH);
            can = CheckNearestNswe(geoX, geoY + 1, worldZ, Cell.NSWE_EAST) && CheckNearestNswe(geoX + 1, geoY, worldZ, Cell.NSWE_SOUTH);
        }
		
        if (can && ((nswe & Cell.NSWE_SOUTH_WEST) == Cell.NSWE_SOUTH_WEST))
        {
            // can = canEnterNeighbors(prevX, prevY + 1, prevGeoZ, Direction.WEST) && canEnterNeighbors(prevX - 1, prevY, prevGeoZ, Direction.SOUTH);
            can = CheckNearestNswe(geoX, geoY + 1, worldZ, Cell.NSWE_WEST) && CheckNearestNswe(geoX - 1, geoY, worldZ, Cell.NSWE_SOUTH);
        }
		
        return can && CheckNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    public void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        _geodata.SetNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    public void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        _geodata.UnsetNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    public int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return _geodata.GetNearestZ(geoX, geoY, worldZ);
    }
    
    public int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        return _geodata.GetNextLowerZ(geoX, geoY, worldZ);
    }
    
    public int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        return _geodata.GetNextHigherZ(geoX, geoY, worldZ);
    }
    
    public int GetGeoX(int worldX)
    {
        return _geodata.GetGeoX(worldX);
    }
    
    public int GetGeoY(int worldY)
    {
        return _geodata.GetGeoY(worldY);
    }

    public int GetWorldX(int geoX)
    {
        return _geodata.GetWorldX(geoX);
    }
    
    public int GetWorldY(int geoY)
    {
        return _geodata.GetWorldY(geoY);
    }
    
    public RegionAbstract GetRegion(int geoX, int geoY)
    {
        return _geodata.GetRegion(geoX, geoY);
    }
    
    public void SetRegion(int regionX, int regionY, Region region)
    {
        _geodata.SetRegion(regionX, regionY, region);
    }
    
    public int GetHeight(int x, int y, int z)
    {
        return GetNearestZ(GetGeoX(x), GetGeoY(y), z);
    }
    
    public int GetSpawnHeight(int x, int y, int z)
    {
        var geoX = GetGeoX(x);
        var geoY = GetGeoY(y);
		
        if (!HasGeoPos(geoX, geoY))
        {
            return z;
        }
		
        var nextLowerZ = GetNextLowerZ(geoX, geoY, z + 20);
        return Math.Abs(nextLowerZ - z) <= SPAWN_Z_DELTA_LIMIT ? nextLowerZ : z;
    }
    
    public int GetSpawnHeight(Location location)
    {
        return GetSpawnHeight(location.GetX(), location.GetY(), location.GetZ());
    }

    public Location GetValidLocation(Location origin, Location destination)
    {
        return GetValidLocation(origin.GetX(), origin.GetY(), origin.GetZ(), destination.GetX(), destination.GetY(), destination.GetZ(), 0);
    }
    
    public Location GetValidLocation(int x, int y, int z, int tx, int ty, int tz, int instanceId)
    {
        var geoX = GetGeoX(x);
        var geoY = GetGeoY(y);
        var nearestFromZ = GetNearestZ(geoX, geoY, z);
        var tGeoX = GetGeoX(tx);
        var tGeoY = GetGeoY(ty);
        var nearestToZ = GetNearestZ(tGeoX, tGeoY, tz);

        // Door checks.
        /*
        if (DoorData.getInstance().checkIfDoorsBetween(x, y, nearestFromZ, tx, ty, nearestToZ, instanceId, false))
        {
            return new Location(x, y, getHeight(x, y, nearestFromZ));
        }
        */
		
        // Fence checks.
        /*
        if (FenceData.getInstance().checkIfFenceBetween(x, y, nearestFromZ, tx, ty, nearestToZ, instanceId))
        {
            return new Location(x, y, getHeight(x, y, nearestFromZ));
        }
        */
		
        LinePointIterator pointIter = new LinePointIterator(geoX, geoY, tGeoX, tGeoY);
        // first point is guaranteed to be available
        pointIter.Next();
        int prevX = pointIter.X();
        int prevY = pointIter.Y();
        int prevZ = nearestFromZ;
		
        while (pointIter.Next())
        {
            int curX = pointIter.X();
            int curY = pointIter.Y();
            int curZ = GetNearestZ(curX, curY, prevZ);
            if (HasGeoPos(prevX, prevY) && !CheckNearestNsweAntiCornerCut(prevX, prevY, prevZ, GeoUtils.ComputeNswe(prevX, prevY, curX, curY)))
            {
                // Can't move, return previous location.
                return new Location(GetWorldX(prevX), GetWorldY(prevY), prevZ);
            }
            prevX = curX;
            prevY = curY;
            prevZ = curZ;
        }
        return HasGeoPos(prevX, prevY) && (prevZ != nearestToZ) ? new Location(x, y, nearestFromZ) : new Location(tx, ty, nearestToZ);
    }
    
    private int GetLosGeoZ(int prevX, int prevY, int prevGeoZ, int curX, int curY, int nswe)
    {
        if ((((nswe & Cell.NSWE_NORTH) != 0) && ((nswe & Cell.NSWE_SOUTH) != 0)) || (((nswe & Cell.NSWE_WEST) != 0) && ((nswe & Cell.NSWE_EAST) != 0)))
        {
            throw new Exception("Multiple directions!");
        }
        return CheckNearestNsweAntiCornerCut(prevX, prevY, prevGeoZ, nswe) ? GetNearestZ(curX, curY, prevGeoZ) : GetNextHigherZ(curX, curY, prevGeoZ);
    }
    
    public int TraceTerrainZ(int x, int y, int z1, int tx, int ty)
    {
        int geoX = GetGeoX(x);
        int geoY = GetGeoY(y);
        int nearestFromZ = GetNearestZ(geoX, geoY, z1);
        int tGeoX = GetGeoX(tx);
        int tGeoY = GetGeoY(ty);
		
        LinePointIterator pointIter = new LinePointIterator(geoX, geoY, tGeoX, tGeoY);
        // First point is guaranteed to be available.
        pointIter.Next();
        int prevZ = nearestFromZ;
		
        while (pointIter.Next())
        {
            prevZ = GetNearestZ(pointIter.X(), pointIter.Y(), prevZ);
        }
		
        return prevZ;
    }
    
    public bool HasGeo(int x, int y)
    {
        return HasGeoPos(GetGeoX(x), GetGeoY(y));
    }
    
    public bool CanSeeTarget(int x, int y, int z, int tx, int ty, int tz)
	{
		int geoX = GetGeoX(x);
		int geoY = GetGeoY(y);
		int tGeoX = GetGeoX(tx);
		int tGeoY = GetGeoY(ty);
		
		int nearestFromZ = GetNearestZ(geoX, geoY, z);
		int nearestToZ = GetNearestZ(tGeoX, tGeoY, tz);
		
		// Fastpath.
		if ((geoX == tGeoX) && (geoY == tGeoY))
		{
			return !HasGeoPos(tGeoX, tGeoY) || (nearestFromZ == nearestToZ);
		}
		
		int fromX = tx;
		int fromY = ty;
		int toX = tx;
		int toY = ty;
		if (nearestToZ > nearestFromZ)
		{
			int tmp = toX;
			toX = fromX;
			fromX = tmp;
			
			tmp = toY;
			toY = fromY;
			fromY = tmp;
			
			tmp = nearestToZ;
			nearestToZ = nearestFromZ;
			nearestFromZ = tmp;
			
			tmp = tGeoX;
			tGeoX = geoX;
			geoX = tmp;
			
			tmp = tGeoY;
			tGeoY = geoY;
			geoY = tmp;
		}
		
		LinePointIterator3D pointIter = new LinePointIterator3D(geoX, geoY, nearestFromZ, tGeoX, tGeoY, nearestToZ);
		// First point is guaranteed to be available, skip it, we can always see our own position.
		pointIter.next();
		int prevX = pointIter.x();
		int prevY = pointIter.y();
		int prevZ = pointIter.z();
		int prevGeoZ = prevZ;
		int ptIndex = 0;
		while (pointIter.next())
		{
			int curX = pointIter.x();
			int curY = pointIter.y();
			
			if ((curX == prevX) && (curY == prevY))
			{
				continue;
			}
			
			int beeCurZ = pointIter.z();
			int curGeoZ = prevGeoZ;
			
			// Check if the position has geodata.
			if (HasGeoPos(curX, curY))
			{
				int nswe = GeoUtils.ComputeNswe(prevX, prevY, curX, curY);
				curGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, curX, curY, nswe);
				int maxHeight = ptIndex < ELEVATED_SEE_OVER_DISTANCE ? nearestFromZ + MAX_SEE_OVER_HEIGHT : beeCurZ + MAX_SEE_OVER_HEIGHT;
				bool canSeeThrough = false;
				if (curGeoZ <= maxHeight)
				{
					if ((nswe & Cell.NSWE_NORTH_EAST) == Cell.NSWE_NORTH_EAST)
					{
						int northGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX, prevY - 1, Cell.NSWE_EAST);
						int eastGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX + 1, prevY, Cell.NSWE_NORTH);
						canSeeThrough = (northGeoZ <= maxHeight) && (eastGeoZ <= maxHeight) && (northGeoZ <= GetNearestZ(prevX, prevY - 1, beeCurZ)) && (eastGeoZ <= GetNearestZ(prevX + 1, prevY, beeCurZ));
					}
					else if ((nswe & Cell.NSWE_NORTH_WEST) == Cell.NSWE_NORTH_WEST)
					{
						int northGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX, prevY - 1, Cell.NSWE_WEST);
						int westGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX - 1, prevY, Cell.NSWE_NORTH);
						canSeeThrough = (northGeoZ <= maxHeight) && (westGeoZ <= maxHeight) && (northGeoZ <= GetNearestZ(prevX, prevY - 1, beeCurZ)) && (westGeoZ <= GetNearestZ(prevX - 1, prevY, beeCurZ));
					}
					else if ((nswe & Cell.NSWE_SOUTH_EAST) == Cell.NSWE_SOUTH_EAST)
					{
						int southGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX, prevY + 1, Cell.NSWE_EAST);
						int eastGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX + 1, prevY, Cell.NSWE_SOUTH);
						canSeeThrough = (southGeoZ <= maxHeight) && (eastGeoZ <= maxHeight) && (southGeoZ <= GetNearestZ(prevX, prevY + 1, beeCurZ)) && (eastGeoZ <= GetNearestZ(prevX + 1, prevY, beeCurZ));
					}
					else if ((nswe & Cell.NSWE_SOUTH_WEST) == Cell.NSWE_SOUTH_WEST)
					{
						int southGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX, prevY + 1, Cell.NSWE_WEST);
						int westGeoZ = GetLosGeoZ(prevX, prevY, prevGeoZ, prevX - 1, prevY, Cell.NSWE_SOUTH);
						canSeeThrough = (southGeoZ <= maxHeight) && (westGeoZ <= maxHeight) && (southGeoZ <= GetNearestZ(prevX, prevY + 1, beeCurZ)) && (westGeoZ <= GetNearestZ(prevX - 1, prevY, beeCurZ));
					}
					else
					{
						canSeeThrough = true;
					}
				}
				
				if (!canSeeThrough)
				{
					return false;
				}
			}
			
			prevX = curX;
			prevY = curY;
			prevGeoZ = curGeoZ;
			++ptIndex;
		}
		return true;
	}

	public bool CanMoveToTarget(int fromX, int fromY, int fromZ, int toX, int toY, int toZ, int instanceId)
	{
		int geoX = GetGeoX(fromX);
		int geoY = GetGeoY(fromY);
		int nearestFromZ = GetNearestZ(geoX, geoY, fromZ);
		int tGeoX = GetGeoX(toX);
		int tGeoY = GetGeoY(toY);
		int nearestToZ = GetNearestZ(tGeoX, tGeoY, toZ);
		
		// Door checks.
		/*
		if (DoorData.getInstance().checkIfDoorsBetween(fromX, fromY, nearestFromZ, toX, toY, nearestToZ, instanceId, false))
		{
			return false;
		}
		
		// Fence checks.
		if (FenceData.getInstance().checkIfFenceBetween(fromX, fromY, nearestFromZ, toX, toY, nearestToZ, instanceId))
		{
			return false;
		}
		*/
		
		LinePointIterator pointIter = new LinePointIterator(geoX, geoY, tGeoX, tGeoY);
		// First point is guaranteed to be available.
		pointIter.Next();
		int prevX = pointIter.X();
		int prevY = pointIter.Y();
		int prevZ = nearestFromZ;
		
		while (pointIter.Next())
		{
			int curX = pointIter.X();
			int curY = pointIter.Y();
			int curZ = GetNearestZ(curX, curY, prevZ);
			if (HasGeoPos(prevX, prevY) && !CheckNearestNsweAntiCornerCut(prevX, prevY, prevZ, GeoUtils.ComputeNswe(prevX, prevY, curX, curY)))
			{
				return false;
			}
			prevX = curX;
			prevY = curY;
			prevZ = curZ;
		}
		return !HasGeoPos(prevX, prevY) || (prevZ == nearestToZ);
	}
}