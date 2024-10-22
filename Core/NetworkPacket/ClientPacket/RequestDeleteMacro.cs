using Core.Controller;
using Core.Module.Player;
using Network;
using System;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 22:09:08

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestDeleteMacro : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly PlayerInstance _playerInstance;
        const int maxMacro = 12;
        private int id;

        public RequestDeleteMacro(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;

            id = packet.ReadInt();
        }

        public override async Task Execute()
        {
            await _playerInstance.PlayerMacros().DeleteMacros(id);
        }
    }
}
