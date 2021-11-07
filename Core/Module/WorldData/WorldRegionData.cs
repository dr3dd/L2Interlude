using System.Collections.Concurrent;
using System.Collections.Generic;
using Core.Module.AreaData;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.WorldData
{
    public class WorldRegionData
    {
        private int _regionX;
        private int _regionY;
        
        private readonly ConcurrentDictionary<int, WorldObject> _visibleObjects;
        private readonly ConcurrentDictionary<int, PlayerInstance> _playerObjects;
        
        private readonly ZoneManager _zoneManager;
        private IList<WorldRegionData> _surroundingRegions;

        public WorldRegionData(int regionX, int regionY)
        {
            _regionX = regionX;
            _regionY = regionY;
            _visibleObjects = new ConcurrentDictionary<int, WorldObject>();
            _playerObjects = new ConcurrentDictionary<int, PlayerInstance>();
            _zoneManager = new ZoneManager();
        }
        
        public void SetSurroundingRegions(List<WorldRegionData> regions)
        {
            _surroundingRegions = regions;
        }
        
        public IEnumerable<WorldRegionData> GetSurroundingRegions()
        {
            return _surroundingRegions;
        }

        public void AddZone(BaseArea baseArea)
        {
            _zoneManager.RegisterNewZone(baseArea);
        }

        public void RevalidateZones(PlayerInstance playerInstance)
        {
            _zoneManager?.RevalidateZones(playerInstance);
        }

        public void AddVisibleObject(WorldObject worldObject)
        {
            _visibleObjects.TryAdd(worldObject.ObjectId, worldObject);
            if (worldObject is PlayerInstance playerInstance)
            {
                _playerObjects.TryAdd(playerInstance.ObjectId, playerInstance);
            }
        }

        public void RemoveVisibleObject(WorldObject worldObject)
        {
            _visibleObjects.TryRemove(worldObject.ObjectId, out worldObject);
            if (worldObject is PlayerInstance)
            {
                _playerObjects.TryRemove(worldObject.ObjectId, out _);
            }
        }
        
        public IEnumerable<WorldObject> GetVisibleObjects()
        {
            return _visibleObjects.Values;
        }
        
        public IEnumerable<PlayerInstance> GetAllPlayers()
        {
            return _playerObjects.Values;
        }
        
        public void RemoveFromZones(Character character)
        {
            _zoneManager?.RemoveCharacter(character);
        }
    }
}