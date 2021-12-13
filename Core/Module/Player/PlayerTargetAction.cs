using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.NpcData;
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
        
        public async Task OnTargetAsync(WorldObject targetPlayer)
        {
            if (GetTarget() != targetPlayer)
            {
                _playerInstance.PlayerTargetAction().SetTarget(targetPlayer);
                await _playerInstance.SendPacketAsync(new MyTargetSelected(targetPlayer.ObjectId, 0));
                if (targetPlayer != _playerInstance)
                {
                    await _playerInstance.SendPacketAsync(new ValidateLocation((Character)targetPlayer));
                }
                return;
            }
            if (targetPlayer != _playerInstance)
            {
                await _playerInstance.SendPacketAsync(new ValidateLocation((Character)targetPlayer));
            }

            if (targetPlayer is NpcInstance npcInstance)
            {
                await npcInstance.OnActionAsync(_playerInstance);
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