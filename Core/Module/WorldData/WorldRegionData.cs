using System.Collections.Concurrent;
using System.Collections.Generic;
using Core.Module.AreaData;
using Core.Module.CharacterData;
using Core.Module.DoorData;
using Core.Module.NpcData;
using Core.Module.Player;

namespace Core.Module.WorldData
{
    public class WorldRegionData
    {
        private int _regionX;
        private int _regionY;
        
        private readonly ConcurrentDictionary<int, NpcInstance> _visibleNpc;
        private readonly ConcurrentDictionary<int, DoorInstance> _visibleDoor;
        private readonly ConcurrentDictionary<int, PlayerInstance> _playerObjects;
        
        private readonly ZoneManager _zoneManager;
        private IList<WorldRegionData> _surroundingRegions;

        public WorldRegionData(int regionX, int regionY)
        {
            _regionX = regionX;
            _regionY = regionY;
            _visibleNpc = new ConcurrentDictionary<int, NpcInstance>();
            _visibleDoor = new ConcurrentDictionary<int, DoorInstance>();
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

        public void RevalidateZones(Character character)
        {
            _zoneManager?.RevalidateZones(character);
        }

        public void AddVisibleObject(WorldObject worldObject)
        {
            switch (worldObject)
            {
                case NpcInstance npcInstance:
                    _visibleNpc.TryAdd(npcInstance.ObjectId, npcInstance);
                    break;
                case DoorInstance doorInstance:
                    _visibleDoor.TryAdd(doorInstance.ObjectId, doorInstance);
                    break;
                case PlayerInstance playerInstance:
                    _playerObjects.TryAdd(playerInstance.ObjectId, playerInstance);
                    break;
            }
        }

        public void RemoveVisibleObject(WorldObject worldObject)
        {
            switch (worldObject)
            {
                case NpcInstance:
                    _visibleNpc.TryRemove(worldObject.ObjectId, out _);
                    break;
                case DoorInstance doorInstance:
                    _visibleDoor.TryRemove(doorInstance.ObjectId, out _);
                    break;
                case PlayerInstance:
                    _playerObjects.TryRemove(worldObject.ObjectId, out _);
                    break;
            }
        }
        
        public IEnumerable<NpcInstance> GetVisibleNpc()
        {
            return _visibleNpc.Values;
        }
        
        public IEnumerable<PlayerInstance> GetAllPlayers()
        {
            return _playerObjects.Values;
        }
        
        public IEnumerable<DoorInstance> GetVisibleDoor()
        {
            return _visibleDoor.Values;
        }
        
        public void RemoveFromZones(Character character)
        {
            _zoneManager?.RemoveCharacter(character);
        }
    }
}