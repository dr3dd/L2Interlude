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

        

        public async Task ValidatePositionAsync(int x, int y, int z, int heading)
        {
            
           
        }
    }
}