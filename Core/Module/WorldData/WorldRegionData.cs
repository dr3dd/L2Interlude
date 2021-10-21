using System.Collections.Concurrent;
using Core.Module.AreaData;
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

        public WorldRegionData(int regionX, int regionY)
        {
            _regionX = regionX;
            _regionY = regionY;
            _visibleObjects = new ConcurrentDictionary<int, WorldObject>();
            _playerObjects = new ConcurrentDictionary<int, PlayerInstance>();
            _zoneManager = new ZoneManager();
        }

        public void AddZone(BaseArea baseArea)
        {
            _zoneManager.RegisterNewZone(baseArea);
        }

        public void RevalidateZones(PlayerInstance playerInstance)
        {
            _zoneManager?.RevalidateZones(playerInstance);
        }
    }
}