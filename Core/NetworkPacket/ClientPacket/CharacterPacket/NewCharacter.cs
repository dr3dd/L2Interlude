using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket.CharacterPacket
{
    public class NewCharacter : PacketBase
    {
        private readonly GameServiceController _controller;
        public NewCharacter(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
        }

        public override async Task Execute()
        {
            await _controller.SendPacketAsync(new CharTemplates());
        }
    }
}