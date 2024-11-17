using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Module.CharacterData;
using Core.Module.DoorData;
using Core.Module.NpcData;
using Core.Module.Player;

namespace Core.Module.WorldData
{
    public abstract class World
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

        public List<NpcInstance> GetVisibleNpc(WorldObject worldObject)
        {
            var region = worldObject.GetWorldRegion();
            
            // Create a list in order to contain all visible WorldObject
            var result = new List<NpcInstance>();
            // Go through the list of region
            foreach (var worldRegion in region.GetSurroundingRegions())
            {
                result.AddRange(worldRegion.GetVisibleNpc().Where(wo => wo != null)
                    .Where(wo => !wo.Equals(worldObject)));
            }
            return result;
        }
        
        public List<DoorInstance> GetVisibleDoor(WorldObject worldObject)
        {
            var region = worldObject.GetWorldRegion();
            
            // Create a list in order to contain all visible WorldObject
            var result = new List<DoorInstance>();
            // Go through the list of region
            foreach (var worldRegion in region.GetSurroundingRegions())
            {
                result.AddRange(worldRegion.GetVisibleDoor().Where(wo => wo != null)
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
            foreach (var worldRegion in region.GetSurroundingRegions())
            {
                if (worldRegion is null)
                    continue;
                // Go through visible object of the selected region
                result.AddRange(worldRegion.GetAllPlayers()
                    .Where(playable => !playable.Equals(worldObject)));
            }
            return result;
        }
        
        public WorldObject GetWorldObject(int objectId)
        {
            return _allObjects[objectId];
        }

        public NpcInstance GetNpcInstance(int objectId)
        {
            return _allObjects[objectId] as NpcInstance;
        }
        public IEnumerable<NpcInstance> GetAllNpcInstance()
        {
            return _allObjects.Values.OfType<NpcInstance>().OrderBy(d => d.NpcId).ToList();
        }
        public IEnumerable<NpcInstance> GetAllNpcInstanceByNpcId(int npcId)
        {
            return _allObjects.Values.OfType<NpcInstance>().OrderBy(d => d.NpcId).Where(n => n.NpcId == npcId).ToList();
        }

        public void RemoveFromAllPlayers(PlayerInstance playerInstance)
        {
            _allPlayers.TryRemove(playerInstance.PlayerAppearance().CharacterName, out _);
        }
        
        public void RemoveVisibleObject(WorldObject worldObject, WorldRegionData oldRegion)
        {
            oldRegion.RemoveVisibleObject(worldObject);
        }
    }
}