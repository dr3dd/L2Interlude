using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Player
{
    internal class PlayerTargetAction
    {
        private PlayerInstance _currentTarget;
        private readonly PlayerInstance _playerInstance;

        public PlayerTargetAction(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        public PlayerInstance GetTarget()
        {
            return _currentTarget;
        }

        public void SetTarget(PlayerInstance playerInstance)
        {
            _currentTarget = playerInstance;
        }
        
        public async Task OnTargetAsync(PlayerInstance targetPlayer)
        {
            if (GetTarget() != targetPlayer)
            {
                _playerInstance.PlayerTargetAction().SetTarget(targetPlayer);
                await _playerInstance.SendPacketAsync(new MyTargetSelected(targetPlayer.ObjectId, 0));
                if (targetPlayer != _playerInstance)
                {
                    await _playerInstance.SendPacketAsync(new ValidateLocation(targetPlayer));
                }
                return;
            }
            if (targetPlayer != _playerInstance)
            {
                await _playerInstance.SendPacketAsync(new ValidateLocation(targetPlayer));
            }
            _playerInstance.PlayerDesire().AddDesire(Desire.InteractDesire, targetPlayer);
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