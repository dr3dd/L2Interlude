using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using Config;
using Core.GeoEngine.PathFinding;
using Core.Module.CharacterData;
using Core.Module.WorldData;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.GeoEngine
{
    public class GeoEngineInit
    {
        private readonly string _basePath;
        protected byte[] _buffer;
        private readonly ABlock[,] _blocks;
        // Pre-allocated buffers.
        private BufferHolder[] _buffers;
        private readonly WorldInit _worldInit;
        public GeoEngineInit(IServiceProvider provider)
        {
            _basePath = provider.GetRequiredService<GameConfig>().ServerConfig.StaticData;
            LoggerManager.Info("GeoEngine: Initializing...");
            // Initialize block container.
            _blocks = new ABlock[GeoStructure.GeoBlocksX, GeoStructure.GeoBlocksY];
            _worldInit = provider.GetRequiredService<WorldInit>();
            // Initialize multilayer temporarily buffer.
            BlockMultilayer.Initialize();
        }

        public void Run()
        {
            int loaded = 0;
            try
            {
                for (int regionX = 16; regionX <= 26; regionX++)
                {
                    for (int regionY = 10; regionY <= 25; regionY++)
                    {
                        var geoFilePath = _basePath + $"/GeoData/{regionX}_{regionY}_conv.dat";
                        if (!File.Exists(geoFilePath))
                        {
                            continue;
                        }
                        if (LoadGeoBlocks(regionX, regionY, geoFilePath))
                        {
                            loaded++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
            LoggerManager.Info("GeoEngine: Loaded " + loaded + " GeoData files.");
            BlockMultilayer.Release();
            
            String[] array = "500x10;1000x10;3000x5;5000x3;10000x3".Split(";");
            _buffers = new BufferHolder[array.Length];
		
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
	            String buf = array[i];
	            String[] args = buf.Split("x");
			
	            try
	            {
		            int size = int.Parse(args[1]);
		            count += size;
		            _buffers[i] = new BufferHolder(int.Parse(args[0]), size);
	            }
	            catch (Exception)
	            {
		            //LOGGER.warning("Could not load buffer setting:" + buf + ". " + e);
	            }
            }
        }

        private bool LoadGeoBlocks(int regionX, int regionY, string geoFileName)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(geoFileName)))
                {
                    
                }
                
                MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(geoFileName);
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    // perform stream operations
                    BinaryReader reader = new BinaryReader(stream);
                    for (int i = 0; i < 18; i++)
                    {
                        reader.ReadByte();
                        //LoggerManager.Info("Byte: " + reader.ReadByte());
                    }

                    // Get block indexes.
                    int blockX = (regionX - World.TileXMin) * GeoStructure.RegionBlocksX;
                    int blockY = (regionY - World.TileYMin) * GeoStructure.RegionBlocksY;

                    for (int ix = 0; ix < GeoStructure.RegionBlocksX; ix++)
                    {
                        for (int iy = 0; iy < GeoStructure.RegionBlocksY; iy++)
                        {
                            var type = reader.ReadInt16();
                            switch (type)
                            {
                                case 64:
                                    _blocks[blockX + ix, blockY + iy] = new BlockComplex(reader);
                                    break;
                                case 0:
                                    _blocks[blockX + ix, blockY + iy] = new BlockFlat(reader);
                                    break;
                                default:
                                    _blocks[blockX + ix, blockY + iy] = new BlockMultilayer(reader);
                                    break;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggerManager.Error("Error loading " + geoFileName + " region file. " + ex);
                return false;
            }
        }
        
        /// <summary>
        /// Returns block of geodata on given coordinates.
        /// </summary>
        /// <param name="geoX"></param>
        /// <param name="geoY"></param>
        /// <returns></returns>
        public ABlock GetBlock(int geoX, int geoY)
        {
            int x = geoX / GeoStructure.BlockCellsX;
            if ((x < 0) || (x >= GeoStructure.GeoBlocksX))
            {
                return null;
            }
            int y = geoY / GeoStructure.BlockCellsY;
            if ((y < 0) || (y >= GeoStructure.GeoBlocksY))
            {
                return null;
            }
            return _blocks[x,y];
        }
        
        /// <summary>
        /// Check if geo coordinates has geo.
        /// </summary>
        /// <param name="geoX"></param>
        /// <param name="geoY"></param>
        /// <returns></returns>
        public bool HasGeoPos(int geoX, int geoY)
        {
            ABlock block = GetBlock(geoX, geoY);
            return (block != null) && block.HasGeoPos();
        }
        
        /// <summary>
        /// Returns the height of cell, which is closest to given coordinates.
        /// </summary>
        /// <param name="geoX"></param>
        /// <param name="geoY"></param>
        /// <param name="worldZ"></param>
        /// <returns>Cell geodata Z coordinate, closest to given coordinates.</returns>
        public short GetHeightNearest(int geoX, int geoY, int worldZ)
        {
            ABlock block = GetBlock(geoX, geoY);
            if (block == null)
            {
                return (short) worldZ;
            }
            return block.GetHeightNearest(geoX, geoY, worldZ);
        }
        
        
        public sbyte GetNsweNearest(int geoX, int geoY, int worldZ)
        {
            ABlock block = GetBlock(geoX, geoY);
            if (block == null)
            {
                return GeoStructure.CellFlagAll;
            }
            return block.GetNsweNearest(geoX, geoY, worldZ);
        }
        
        public bool HasGeo(int worldX, int worldY) => HasGeoPos(GetGeoX(worldX), GetGeoY(worldY));
        
        public short GetHeight(Location loc) => GetHeightNearest(GetGeoX(loc.GetX()), GetGeoY(loc.GetY()), loc.GetZ());
        public short GetHeight(int worldX, int worldY, int worldZ) => GetHeightNearest(GetGeoX(worldX), GetGeoY(worldY), worldZ);
        
        
        public int GetGeoX(int worldX)
        {
            return (worldX - _worldInit.MapMinX) >> 4;
        }
        
        public int GetGeoY(int worldY)
        {
            return (worldY - _worldInit.MapMinY) >> 4;
        }
        
        public int GetWorldX(int geoX)
        {
            return (geoX << 4) + _worldInit.MapMinX + 8;
        }
        
        public int GetWorldY(int geoY)
        {
            return (geoY << 4) + _worldInit.MapMinY + 8;
        }

        public LinkedList<Location> FindPath(int ox, int oy, int oz, int tx, int ty, int tz)
        {
	        // Get origin and check existing geo coords.
	        int gox = GetGeoX(ox);
	        int goy = GetGeoY(oy);
	        if (!HasGeoPos(gox, goy))
	        {
		        return new LinkedList<Location>();
	        }
		
	        int goz = GetHeightNearest(gox, goy, oz);
		
	        // Get target and check existing geo coords.
	        int gtx = GetGeoX(tx);
	        int gty = GetGeoY(ty);
	        if (!HasGeoPos(gtx, gty))
	        {
		        return new LinkedList<Location>();
	        }

	        var oBlock = GetBlock(gox, goy);
	        var tBlock = GetBlock(gtx, gty);
		
	        int gtz = GetHeightNearest(gtx, gty, tz);
		
	        // Prepare buffer for pathfinding calculations.
	        NodeBuffer buffer = GetBuffer(300 + (10 * (Math.Abs(gox - gtx) + Math.Abs(goy - gty) + Math.Abs(goz - gtz))));
	        if (buffer == null)
	        {
		        return new LinkedList<Location>();
	        }
		
	        // Find path.
	        LinkedList<Location> path = null;
	        try
	        {
		        path = buffer.FindPath(gox, goy, goz, gtx, gty, gtz);
		        if (!path.Any())
		        {
			        return new LinkedList<Location>();
		        }
	        }
	        catch (Exception)
	        {
		        return new LinkedList<Location>();
	        }
	        finally
	        {
		        buffer.Free();
	        }
		
	        // Check path.
	        if (path.Count < 3)
	        {
		        return path;
	        }
	        return path;
        }
        
        private class BufferHolder
        {
	        public int _size;
	        public List<NodeBuffer> _buffer;
		
	        public BufferHolder(int size, int count)
	        {
		        _size = size;
		        _buffer = new List<NodeBuffer>(count);
			
		        for (int i = 0; i < count; i++)
		        {
			        _buffer.Add(new NodeBuffer(size));
		        }
	        }
        }
        
        private NodeBuffer GetBuffer(int size)
        {
	        NodeBuffer current = null;
	        foreach (BufferHolder holder in _buffers)
	        {
		        // Find proper size of buffer.
		        if (holder._size < size)
		        {
			        continue;
		        }
			
		        // Find unlocked NodeBuffer.
		        foreach (NodeBuffer buffer in holder._buffer)
		        {
			        if (!buffer.IsLocked())
			        {
				        continue;
			        }
				
			        return buffer;
		        }
			
		        // NodeBuffer not found, allocate temporary buffer.
		        current = new NodeBuffer(holder._size);
		        current.IsLocked();
	        }
		
	        return current;
        }

        public bool CanMoveToTarget(int originX, int originY, int originZ, int targetX, int targetY, int targetZ)
        {
            // Get geodata coordinates.
            int gox = GetGeoX(originX);
            int goy = GetGeoY(originY);
            ABlock block = GetBlock(gox, goy);
            if ((block == null) || !block.HasGeoPos())
            {
                return true; // No Geodata found.
            }
            int goz = GetHeightNearest(gox, goy, originZ);
            int gtx = GetGeoX(targetX);
            int gty = GetGeoY(targetY);
		
            // Check movement within same cell.
            if ((gox == gtx) && (goy == gty))
            {
                return goz == GetHeight(targetX, targetY, targetZ);
            }
            // Get nswe flag.
            int nswe = GetNsweNearest(gox, goy, goz);
		
            // Get delta coordinates, slope of line and direction data.
            int dx = targetX - originX;
            int dy = targetY - originY;
            double m = (double) dy / dx;

            MoveDirection mdt = MoveDirection.GetDirection(gtx - gox, gty - goy);
            // Get cell grid coordinates.
            int gridX = (originX & 0xFFFFFF0);
            int gridY = (originY & 0xFFFFFF0);
		
            // Run loop.
            sbyte dir;
            int nx = gox;
            int ny = goy;

            
            while ((gox != gtx) || (goy != gty))
            {

                // Calculate intersection with cell's X border.
                int checkX = gridX + mdt.OffsetX;
                int checkY = (int) (originY + (m * (checkX - originX)));

                if ((mdt.StepX != 0) && (GetGeoY(checkY) == goy))
                {
                    // Set next cell is in X direction.
                    gridX += mdt.StepX;
                    nx += mdt.SignumX;
                    dir = mdt.DirectionX;
                }
                else
                {
                    // Calculate intersection with cell's Y border.
                    checkY = gridY + mdt.OffsetY;
                    checkX = (int) (originX + ((checkY - originY) / m));

                    // Set next cell in Y direction.
                    gridY += mdt.StepY;
                    ny += mdt.SignumY;
                    dir = mdt.DirectionY;
                }

                // Check point heading into obstacle, if so return current point.
                if ((nswe & dir) == 0)
                {
                    return false;
                }

                block = GetBlock(nx, ny);
                if ((block == null) || !block.HasGeoPos())
                {
                    return true; // No Geodata found.
                }

                // Check next point for extensive Z difference, if so return current point.
                int i = block.GetIndexBelow(nx, ny, goz + GeoStructure.CellIgnoreHeight);
                if (i < 0)
                {
                    return false;
                }

                // Update current point's coordinates and nswe.
                gox = nx;
                goy = ny;
                goz = block.GetHeight(i);
                nswe = block.GetNswe(i);
            }
            return goz == GetHeight(targetX, targetY, targetZ);
        }

        public Location GetValidLocation(int ox, int oy, int oz, int tx, int ty, int tz)
        {
            // Get geodata coordinates.
			int gox = GetGeoX(ox);
			int goy = GetGeoY(oy);
			ABlock block = GetBlock(gox, goy);
			if ((block == null) || !block.HasGeoPos())
			{
				return new Location(tx, ty, tz); // No Geodata found.
			}
			int gtx = GetGeoX(tx);
			int gty = GetGeoY(ty);
			int gtz = GetHeightNearest(gtx, gty, tz);
			int goz = GetHeightNearest(gox, goy, oz);
			int nswe = GetNsweNearest(gox, goy, goz);
			
			// Get delta coordinates, slope of line and direction data.
			int dx = tx - ox;
			int dy = ty - oy;
			double m = (double) dy / dx;
			MoveDirection mdt = MoveDirection.GetDirection(gtx - gox, gty - goy);
			
			// Get cell grid coordinates.
			int gridX = ox;
			int gridY = oy;
			
			// Run loop.
			sbyte dir;
			int nx = gox;
			int ny = goy;
			while ((gox != gtx) || (goy != gty))
			{
				// Calculate intersection with cell's X border.
				int checkX = gridX + mdt.OffsetX;
				int checkY = (int) (oy + (m * (checkX - ox)));
				
				if ((mdt.StepX != 0) && (GetGeoY(checkY) == goy))
				{
					// Set next cell is in X direction.
					gridX += mdt.StepX;
					nx += mdt.SignumX;
					dir = mdt.DirectionX;
				}
				else
				{
					// Calculate intersection with cell's Y border.
					checkY = gridY + mdt.OffsetY;
					checkX = (int) (ox + ((checkY - oy) / m));
					checkX = Utility.Limit(checkX, gridX, gridX + 15);
					
					// Set next cell in Y direction.
					gridY += mdt.StepY;
					ny += mdt.SignumY;
					dir = mdt.DirectionY;
				}
				
				// Check target cell is out of geodata grid (world coordinates).
				if ((nx < 0) || (nx >= GeoStructure.GeoCellsX) || (ny < 0) || (ny >= GeoStructure.GeoCellsY))
				{
					return new Location(checkX, checkY, goz);
				}
				
				// Check point heading into obstacle, if so return current (border) point.
				if ((nswe & dir) == 0)
				{
					return new Location(checkX, checkY, goz);
				}
				
				block = GetBlock(nx, ny);
				if ((block == null) || !block.HasGeoPos())
				{
					return new Location(tx, ty, tz); // No Geodata found.
				}
				
				// Check next point for extensive Z difference, if so return current (border) point.
				int i = block.GetIndexBelow(nx, ny, goz + GeoStructure.CellIgnoreHeight);
				if (i < 0)
				{
					return new Location(checkX, checkY, goz);
				}
				
				// Update current point's coordinates and nswe.
				gox = nx;
				goy = ny;
				goz = block.GetHeight(i);
				nswe = block.GetNswe(i);
			}
			
			// Compare Z coordinates:
			// If same, path is okay, return target point and fix its Z geodata coordinate.
			// If not same, path is does not exist, return origin point.
			return goz == gtz ? new Location(tx, ty, gtz) : new Location(ox, oy, oz);
        }
        
        public bool CanSee(int ox, int oy, int oz, float oheight, int tx, int ty, int tz, float theight)
        {
			// Get origin geodata coordinates.
			int gox = GetGeoX(ox);
			int goy = GetGeoY(oy);
			ABlock block = GetBlock(gox, goy);
			if ((block == null) || !block.HasGeoPos())
			{
				return true; // No Geodata found.
			}
			
			// Get target geodata coordinates.
			int gtx = GetGeoX(tx);
			int gty = GetGeoY(ty);
			
			// Check being on same cell and layer (index).
			// Note: Get index must use origin height increased by cell height, the method returns index to height exclusive self.
			int index = block.GetIndexNearest(gox, goy, oz + GeoStructure.CellHeight); // getIndexBelow
			if (index < 0)
			{
				return false;
			}
			
			if ((gox == gtx) && (goy == gty))
			{
				return index == block.GetIndexNearest(gtx, gty, tz + GeoStructure.CellHeight); // getIndexBelow
			}
			
			// Get ground and nswe flag.
			int groundZ = block.GetHeight(index);
			int nswe = block.GetNswe(index);
			
			// Get delta coordinates, slope of line (XY, XZ) and direction data.
			int dx = tx - ox;
			int dy = ty - oy;
			double dz = (tz + theight) - (oz + oheight);
			double m = (double) dy / dx;
			double mz = dz / Math.Sqrt((dx * dx) + (dy * dy));
			MoveDirection mdt = MoveDirection.GetDirection(gtx - gox, gty - goy);
			
			// Get cell grid coordinates.
			int gridX = (int)(ox & 0xFFFFFFF0);
			int gridY = (int)(oy & 0xFFFFFFF0);
			
			// Run loop.
			sbyte dir;
			while ((gox != gtx) || (goy != gty))
			{
				// Calculate intersection with cell's X border.
				int checkX = gridX + mdt.OffsetX;
				int checkY = (int) (oy + (m * (checkX - ox)));
				
				if ((mdt.StepX != 0) && (GetGeoY(checkY) == goy))
				{
					// Set next cell in X direction.
					gridX += mdt.StepX;
					gox += mdt.SignumX;
					dir = mdt.DirectionX;
				}
				else
				{
					// Calculate intersection with cell's Y border.
					checkY = gridY + mdt.OffsetY;
					checkX = (int) (ox + ((checkY - oy) / m));
					checkX = Utility.Limit(checkX, gridX, gridX + 15);
					
					// Set next cell in Y direction.
					gridY += mdt.StepY;
					goy += mdt.SignumY;
					dir = mdt.DirectionY;
				}
				
				// Get block of the next cell.
				block = GetBlock(gox, goy);
				if ((block == null) || !block.HasGeoPos())
				{
					return true; // No Geodata found.
				}
				
				// Get line of sight height (including Z slope).
				double losz = oz + oheight + 32; //add to config MAX_OBSTACLE_HEIGHT
				losz += mz * Math.Sqrt(((checkX - ox) * (checkX - ox)) + ((checkY - oy) * (checkY - oy)));
				
				// Check line of sight going though wall (vertical check).
				
				// Get index of particular layer, based on last iterated cell conditions.
				bool canMove = (nswe & dir) != 0;
				if (canMove)
				{
					// No wall present, get next cell below current cell.
					index = block.GetIndexBelow(gox, goy, groundZ + GeoStructure.CellIgnoreHeight);
				}
				else
				{
					// Wall present, get next cell above current cell.
					index = block.GetIndexAbove(gox, goy, groundZ - (2 * GeoStructure.CellHeight));
				}
				// Next cell's does not exist (no geodata with valid condition), return fail.
				if (index < 0)
				{
					return false;
				}
				// Get next cell's layer height.
				int z = block.GetHeight(index);
				// Perform sine of sight check (next cell is above line of sight line), return fail.
				if (z > losz)
				{
					return false;
				}
				// Next cell is accessible, update z and NSWE.
				groundZ = z;
				nswe = block.GetNswe(index);
			}
			// Iteration is completed, no obstacle is found.
			return true;
		}
    }
}