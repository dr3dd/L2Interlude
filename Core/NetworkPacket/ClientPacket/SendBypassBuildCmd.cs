using Core.Controller;
using Core.Controller.Handlers;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Network;
using System;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 16.08.2024 21:43:33

namespace Core.NetworkPacket.ClientPacket
{
    public class SendBypassBuildCmd : PacketBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PlayerInstance _playerInstance;
        private readonly string _bypass = "";
        public SendBypassBuildCmd(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _bypass = packet.ReadString().Trim();
        }

        public override async Task Execute()
        {
            var adminCommandHandler = _serviceProvider.GetRequiredService<AdminCommandHandler>();
            adminCommandHandler.Request(_playerInstance, _bypass);
        }
    }
}
