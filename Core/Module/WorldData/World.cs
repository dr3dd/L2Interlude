using System;

namespace Core.Module.WorldData
{
    public class World
    {
        public static int ShiftBy = 11;
	
        public static int TileSize = 32768;
        
        /** Map dimensions. */
        public static int TileXMin = 16;
        public static int TileYMin = 10;
        public static int TileXMax = 26;
        public static int TileYMax = 25;
        public static int TileZeroCoordX = 20;
        public static int TileZeroCoordY = 18;
        public static int WorldXMin = (TileXMin - TileZeroCoordX) * TileSize;
        public static int WorldYMin = (TileYMin - TileZeroCoordY) * TileSize;
	
        public static int WorldXMax = ((TileXMax - TileZeroCoordX) + 1) * TileSize;
        public static int WorldYMax = ((TileYMax - TileZeroCoordY) + 1) * TileSize;
	
        /** Calculated offset used so top left region is 0,0 */
        public static int OffsetX = Math.Abs(WorldXMin >> ShiftBy);
        public static int OffsetY = Math.Abs(WorldYMin >> ShiftBy);
	
        /** Number of regions. */
        private static int _regionsX = (WorldXMax >> ShiftBy) + OffsetX;
        private static int _regionsY = (WorldYMax >> ShiftBy) + OffsetY;
    }
}