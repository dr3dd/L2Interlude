using Core.Module.AreaData;
using Core.Module.WorldData;
using Helpers;

namespace Core.Module.Player
{
    public class PlayerZone
    {
        private readonly PlayerInstance _playerInstance;
        
        private readonly byte[] _areas;
        public PlayerZone(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _areas = new byte[AreaId.GetAreaCount()];
            
        }

        public bool IsInsideZone(AreaId area)
        {
            return _areas[(int)area.Id] > 0;
        }
        
        public bool IsInsideRadius2D(WorldObject worldObject, int radius)
        {
            return IsInsideRadius2D(worldObject.GetX(), worldObject.GetY(), radius);
        }
        
        public bool IsInsideRadius2D(int x, int y, int radius)
        {
            return CalculateRange.CalculateDistanceSq2D(x, y, _playerInstance.GetX(), _playerInstance.GetY()) < (radius * radius);
        }

        public void RevalidateZone()
        {
            _playerInstance.GetWorldRegion().RevalidateZones(_playerInstance);
        }
    }
}