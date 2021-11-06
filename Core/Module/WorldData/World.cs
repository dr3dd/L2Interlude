using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket.CharacterPacket;

namespace Core.Module.WorldData
{
    internal abstract class World
    {
        public const int ShiftBy = 12;

        // Geodata min/max tiles
        public const int TileXMin = 16;
        public const int TileXMax = 26;
        public const int TileYMin = 10;
        public const int TileYMax = 25;

        // Map dimensions
        public const int TileSize = 32768;
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
        
        /// <summary>
        /// Add WorldObject object in _allObjects.
        /// </summary>
        /// <param name="worldObject"></param>
        public void StoreWorldObject(WorldObject worldObject)
        {
            _allObjects.TryAdd(worldObject.ObjectId, worldObject);
        }

        public void StorePlayerObject(PlayerInstance playerInstance)
        {
            _allPlayers.TryAdd(playerInstance.PlayerAppearance().CharacterName, playerInstance);
        }
        
        /// <summary>
        /// Removes the objects
        /// </summary>
        /// <param name="worldObject"></param>
        public void RemoveObject(WorldObject worldObject)
        {
            _allObjects.TryRemove(worldObject.ObjectId, value: out _);
        }

        public List<WorldObject> GetVisibleObjects(WorldObject worldObject, int radius)
        {
            if ((worldObject == null))
            {
                return new List<WorldObject>();
            }
            
            var region = worldObject.GetWorldRegion();
            
            var x = worldObject.GetX();
            var y = worldObject.GetY();
            var sqRadius = radius * radius;
            
            // Create a list in order to contain all visible WorldObject
            var result = new List<WorldObject>();
            // Go through the list of region
            foreach (var worldRegion in region.GetSurroundingRegions())
            {
                result.AddRange(from wo in worldRegion.GetVisibleObjects()
                    where !wo.Equals(worldObject)
                    let x1 = wo.GetX()
                    let y1 = wo.GetY()
                    let dx = x1 - x
                    let dy = y1 - y
                    where ((dx * (double)dx) + (dy * (double)dy)) < sqRadius
                    select wo);
            }
            return result;
        }
        
        public List<WorldObject> GetVisibleObjects(WorldObject worldObject)
        {
            var region = worldObject.GetWorldRegion();
            
            // Create a list in order to contain all visible WorldObject
            var result = new List<WorldObject>();
            // Go through the list of region
            foreach (WorldRegionData worldRegion in region.GetSurroundingRegions())
            {
                result.AddRange(worldRegion.GetVisibleObjects().Where(wo => wo != null)
                    .Where(wo => !wo.Equals(worldObject)));
            }
            return result;
        }
        
        public List<PlayerInstance> GetVisiblePlayers(WorldObject worldObject)
        {
            WorldRegionData region = worldObject.GetWorldRegion();
            // Create a list in order to contain all visible WorldObject
            List<PlayerInstance> result = new List<PlayerInstance>();
		
            // Go through the list of region
            foreach (WorldRegionData worldRegion in region.GetSurroundingRegions())
            {
                // Go through visible object of the selected region
                result.AddRange(worldRegion.GetAllPlayers().Where(playable => !playable.Equals(worldObject)));
            }
            return result;
        }
        
        public WorldObject GetWorldObject(int objectId)
        {
            return _allObjects[objectId];
        }
    }
}