using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Core.Module.CharacterData;
using Core.Module.WorldData;
using L2Logger;

namespace Core.GeoEngine
{
    public class GeoEngineInit
    {
        private readonly string _basePath;
        protected byte[] _buffer;
        private readonly ABlock[,] _blocks;
        public GeoEngineInit()
        {
            _basePath = Initializer.Config().ServerConfig.StaticData;
            LoggerManager.Info("GeoEngine: Initializing...");
            // Initialize block container.
            _blocks = new ABlock[GeoStructure.GeoBlocksX, GeoStructure.GeoBlocksY];
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
        
        
        public static int GetGeoX(int worldX)
        {
            return (worldX - World.WorldXMin) >> 4;
        }
        
        public static int GetGeoY(int worldY)
        {
            return (worldY - World.WorldYMin) >> 4;
        }
        
        public static int GetWorldX(int geoX)
        {
            return (geoX << 4) + World.WorldXMin + 8;
        }
        
        public static int GetWorldY(int geoY)
        {
            return (geoY << 4) + World.WorldYMin + 8;
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


            return true;
        }
    }
}