using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal class RequestTargetCancel : PacketBase
    {
        private readonly int _unselect;
        private readonly PlayerInstance _playerInstance;
        
        public RequestTargetCancel(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _unselect = packet.ReadShort();
        }

        public override async Task Execute()
        {
            await _playerInstance.PlayerTargetAction().CancelTargetAsync(_unselect);
        }
    }
}