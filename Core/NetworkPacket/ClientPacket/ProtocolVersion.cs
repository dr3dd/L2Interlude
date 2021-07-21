using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.NetworkPacket.ServerPacket;
using L2Logger;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    public class ProtocolVersion : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly int _protocol;
        
        public ProtocolVersion(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) 
            : base(serviceProvider)
        {
            _controller = controller;
            _protocol = packet.ReadInt();
        }

        public override async Task Execute()
        {
            if (_protocol != 746 && _protocol != 251)
            {
                LoggerManager.Info($"Protocol fail {_protocol}");
                await _controller.SendPacketAsync(new KeyPacket(_controller, 0));
                _controller.CloseConnection();
                return;
            }

            if (_protocol == -1)
            {
                LoggerManager.Info($"Ping received {_protocol}");
                await _controller.SendPacketAsync(new KeyPacket(_controller, 0));
                _controller.CloseConnection();
                return;
            }

            LoggerManager.Info($"Accepted {_protocol} client");

            await _controller.SendPacketAsync(new KeyPacket(_controller, 1));
        }
    }
}