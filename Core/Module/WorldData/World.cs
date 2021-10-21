using System.Collections.Concurrent;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.WorldData
{
    internal abstract class World
    {
        public int ShiftBy = 12;

        // Geodata min/max tiles
        public int TileXMin = 16;
        public int TileXMax = 26;
        public int TileYMin = 10;
        public int TileYMax = 25;

        // Map dimensions
        public int TileSize = 32768;
        public int MapMinX;
        public int MapMaxX;
        public int MapMinY;
        public int MapMaxY;

        /** calculated offset used so top left region is 0,0. */
        protected int RegionsX;

        /** The Constant REGIONS_Y. */
        protected int RegionsY;

        /** calculated offset used so top left region is 0,0. */
        public int OffsetX;

        /** The Constant OFFSET_Y. */
        public int OffsetY;

        /** The _world regions. */
        protected WorldRegionData[,] _worldRegions;

        public WorldRegionData[,] GetAllWorldRegions() => _worldRegions;
        
        /// <summary>
        /// containing all the players in game
        /// </summary>
        private static ConcurrentDictionary<string, PlayerInstance> _allPlayers;
        
        /// <summary>
        /// containing all visible objects
        /// </summary>
        private static ConcurrentDictionary<int, WorldObject> _allObjects;

        protected World()
        {
            _allObjects = new ConcurrentDictionary<int, WorldObject>();
            _allPlayers = new ConcurrentDictionary<string, PlayerInstance>();
        }
        
        public WorldRegionData GetRegion(Location location)
        {
            return _worldRegions[(location.GetX() >> ShiftBy) + OffsetX,(location.GetY() >> ShiftBy) + OffsetY];
        }
    }
}