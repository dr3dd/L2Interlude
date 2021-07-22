using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;

namespace Core.Module.Player
{
    public class PlayerDesire : AbstractDesire
    {
        private readonly PlayerInstance _playerInstance;
        public PlayerDesire(PlayerInstance playerInstance) : base(playerInstance)
        {
            _playerInstance = playerInstance;
        }
        protected override async Task MoveToDesireAsync(Location destination)
        {
            // Move the actor to Location (x,y,z) server side AND client side by sending Server->Client packet CharMoveToLocation (broadcast)
            ChangeDesire(Desire.MoveToDesire);
            await MoveToAsync(destination.GetX(), destination.GetY(), destination.GetZ()).ContinueWith(HandleException);
        }
        
        private void HandleException(Task obj)
        {
            if (obj.IsFaulted)
            {
                LoggerManager.Error(GetType().Name + ": " + obj.Exception);
            }
        }
        
        private async Task MoveToAsync(int x, int y, int z)
        {
            _playerInstance.PlayerMovement().MoveToLocation(x, y, z, 0);
            await _playerInstance.SendPacketAsync(new CharMoveToLocation(_playerInstance));
        }
    }
}