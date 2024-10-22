using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class Logout : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        public Logout(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            if (_playerInstance != null)
            {
                await _playerInstance.DeleteMeAsync();
            }
        }
    }
}