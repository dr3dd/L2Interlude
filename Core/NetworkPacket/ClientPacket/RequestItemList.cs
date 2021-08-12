using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestItemList : PacketBase
    {
        private readonly GameServiceController _controller;
        
        
        public RequestItemList(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
        }

        private PlayerInstance GetPlayerInstance()
        {
            return _controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            PlayerInstance playerInstance = GetPlayerInstance();
            await _controller.SendPacketAsync(new ItemList(playerInstance));
        }
    }
}