using System.Threading.Tasks;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Player
{
    internal class PlayerTargetAction
    {
        private WorldObject _currentTarget;
        private readonly PlayerInstance _playerInstance;

        public PlayerTargetAction(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        public WorldObject GetTarget()
        {
            return _currentTarget;
        }

        public void SetTarget(WorldObject playerInstance)
        {
            _currentTarget = playerInstance;
        }
        
        public async Task CancelTargetAsync(int unselect)
        {
            if (unselect == 0)
            {
                await RemoveTargetAsync();
            }
            else if (GetTarget() != null)
            {
                await RemoveTargetAsync();
            }
        }
        
        public async Task RemoveTargetAsync()
        {
            await _playerInstance.SendPacketAsync(new TargetUnselected(_playerInstance));
            _currentTarget = null;
        }
    }
}