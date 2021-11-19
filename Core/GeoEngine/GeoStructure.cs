using Core.Module.WorldData;

namespace Core.GeoEngine
{
    internal static class GeoStructure
    {
        // Geo cell direction (nswe) flags.
        public static sbyte CellFlagNone = 0x00;
        public static sbyte CellFlagE = 0x01;
        public static sbyte CellFlagW = 0x02;
        public static sbyte CellFlagS = 0x04;
        public static sbyte CellFlagN = 0x08;
        public static sbyte CellFlagAll = 0x0F;
        
        // Geo cell height constants.
        public static readonly int CellSize = 16;
        public static readonly int CellHeight = 8;
        public static int CellIgnoreHeight = CellHeight * 6;
        
        // Geo block type identification.
        public static byte TypeFlatL2JL2Off = 0;
        public static byte TypeComplexL2J = 1;
        public static byte TypeComplexL2Off = 0x40;
        public static byte TypeMultilayerL2J = 2;
        // public static final byte TYPE_MULTILAYER_L2OFF = 0x41; // officially not does exist, is anything above complex block (0x41 - 0xFFFF)
	
        // Geo block dimensions.
        public static readonly int BlockCellsX = 8;
        public static readonly int BlockCellsY = 8;
        public static int BlockCells = BlockCellsX * BlockCellsY;
	
        // Geo region dimensions.
        public static readonly int RegionBlocksX = 256;
        public static readonly int RegionBlocksY = 256;
        public static int RegionBlocks = RegionBlocksX * RegionBlocksY;
	
        public static int RegionCellsX = RegionBlocksX * BlockCellsX;
        public static int RegionCellsY = RegionBlocksY * BlockCellsY;
	
        // Geo world dimensions.
        public static int GeoRegionsX = ((World.TileXMax - World.TileXMin) + 1);
        public static int GeoRegionsY = ((World.TileYMax - World.TileYMin) + 1);
	
        public static readonly int GeoBlocksX = GeoRegionsX * RegionBlocksX;
        public static readonly int GeoBlocksY = GeoRegionsY * RegionBlocksY;
	
        public static int GeoCellsX = GeoBlocksX * BlockCellsX;
        public static int GeoCellsY = GeoBlocksY * BlockCellsY;
    }
}