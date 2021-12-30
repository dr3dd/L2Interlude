using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class Appearing : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        
        public Appearing(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            if (_playerInstance.PlayerAction().IsTeleporting())
            {
                _playerInstance.PlayerAction().SetTeleporting(false);
                _playerInstance.PlayerZone().RevalidateZone();
                await _playerInstance.UpdateKnownObjects();
            }
            await _playerInstance.SendPacketAsync(new UserInfo(_playerInstance));
        }
    }
}