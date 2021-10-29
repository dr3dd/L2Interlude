using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using L2Logger;

namespace Core.Module.Player
{
    public class PlayerDesire : AbstractDesire
    {
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerDesireCast _playerDesireCast;
        public PlayerDesire(PlayerInstance playerInstance) : base(playerInstance)
        {
            _playerInstance = playerInstance;
            _playerDesireCast = new PlayerDesireCast(_playerInstance);
        }
        protected override async Task MoveToDesireAsync(Location destination)
        {
            // Move the actor to Location (x,y,z) server side AND client side by sending Server->Client packet CharMoveToLocation (broadcast)
            ChangeDesire(Desire.MoveToDesire);
            await MoveToAsync(destination.GetX(), destination.GetY(), destination.GetZ()).ContinueWith(HandleException);
        }

        protected override async Task CastDesireAsync(SkillDataModel skill)
        {
            ChangeDesire(Desire.CastDesire);
            await _playerDesireCast.DoCastAsync(skill);
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
            await _playerInstance.SendToKnownPlayers(new CharMoveToLocation(_playerInstance));
        }
    }
}