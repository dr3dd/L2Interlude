using System.Threading.Tasks;

namespace Core.Module.Player
{
    public sealed class PlayerMoveToLocation
    {
        private readonly PlayerInstance _playerInstance;
        public PlayerMoveToLocation(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }
        
        public async Task MoveToLocationAsync(int targetX, int targetY, int targetZ, int originX, int originY, int originZ)
        {
            
            if ((targetX == originX) && (targetY == originY) && (targetZ == originZ))
            {
                //await _playerInstance.SendPacketAsync(new StopMove(_playerInstance));
                await _playerInstance.SendActionFailedPacketAsync();
            }

            double dx = targetX - _playerInstance.PlayerCharacterInfo().Location.GetX();
            double dy = targetY - _playerInstance.PlayerCharacterInfo().Location.GetY();

            if (((dx * dx) + (dy * dy)) > 98010000)
            {
                await _playerInstance.SendActionFailedPacketAsync();
                return;
            }
            
            
        }
        

        public async Task ValidatePositionAsync(int x, int y, int z, int heading)
        {
            
           
        }
    }
}