using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class RequestBypass : PacketBase
    {
        private readonly PlayerInstance _playerInstance;
        private readonly string _command;
        
        public RequestBypass(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _command = packet.ReadString();
        }

        public override async Task Execute()
        {
            var id = _command;
            
        }
    }
}