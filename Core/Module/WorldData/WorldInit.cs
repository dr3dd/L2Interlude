using System;

namespace Core.Module.WorldData
{
	internal class WorldInit : World
    {
	    public WorldInit()
	    {
		    MapMinX = (TileXMin - 20) * TileSize;
		    MapMaxX = (TileXMax - 19) * TileSize;
		    MapMinY = (TileYMin - 18) * TileSize;
		    MapMaxY = (TileYMax - 17) * TileSize;

		    OffsetX = Math.Abs(MapMinX >> ShiftBy);
		    OffsetY = Math.Abs(MapMinY >> ShiftBy);

		    RegionsX = (MapMaxX >> ShiftBy) + OffsetX;
		    RegionsY = (MapMaxY >> ShiftBy) + OffsetY;
	    }

        public void Run()
        {
	        _worldRegions = new WorldRegionData[RegionsX + 1, RegionsY + 1];
	        for (var x = 0; x <= RegionsX; x++)
	        {
		        for (var y = 0; y <= RegionsY; y++)
		        {
			        _worldRegions[x,y] = new WorldRegionData(x, y);
		        }
	        }
        }
    }
}