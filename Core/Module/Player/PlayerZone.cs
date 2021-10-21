using Core.Module.AreaData;

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

        public void RevalidateZone()
        {
            _playerInstance.GetWorldRegion().RevalidateZones(_playerInstance);
        }
    }
}