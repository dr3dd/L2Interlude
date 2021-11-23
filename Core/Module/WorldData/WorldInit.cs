using System;
using System.Collections.Generic;
using L2Logger;

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
	        for (int x = 0; x <= RegionsX; x++)
	        {
		        for (int y = 0; y <= RegionsY; y++)
		        {
			        _worldRegions[x,y] = new WorldRegionData(x, y);
		        }
	        }
	        for (int rx = 0; rx <= RegionsX; rx++)
	        {
		        for (int ry = 0; ry <= RegionsY; ry++)
		        {
			        List<WorldRegionData> surroundingRegions = new List<WorldRegionData>();
			        for (int sx = rx - 1; sx <= (rx + 1); sx++)
			        {
				        for (int sy = ry - 1; sy <= (ry + 1); sy++)
				        {
					        if (((sx >= 0) && (sx < RegionsX) && (sy >= 0) && (sy < RegionsY)))
					        {
						        if (_worldRegions[sx, sy] == null)
							        continue;
						        surroundingRegions.Add(_worldRegions[sx,sy]);
					        }
				        }
			        }
			        _worldRegions[rx,ry].SetSurroundingRegions(surroundingRegions);
		        }
	        }
	        
	        /*
	        for (var x = 0; x <= RegionsX; x++)
	        {
		        for (var y = 0; y <= RegionsY; y++)
		        {
			        _worldRegions[x,y] = new WorldRegionData(x, y);
			        var surroundingRegions = new List<WorldRegionData>();
			        for (int sx = x - 1; sx <= (x + 1); sx++)
			        {
				        for (int sy = y - 1; sy <= (y + 1); sy++)
				        {
					        if (((sx >= 0) && (sx < RegionsX) && (sy >= 0) && (sy < RegionsY)))
					        {
							        surroundingRegions.Add(_worldRegions[sx,sy]);
					        }
				        }
			        }
			        _worldRegions[x,y].SetSurroundingRegions(surroundingRegions);
		        }
	        }
	        */
	        LoggerManager.Info("World: (" + RegionsX + "x" + RegionsY + ") World Region Grid set up.");
        }
    }
}