using Core.Controller;
using Core.Module.Handlers;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Network;
using System;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 04.12.2024 10:13:58

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class BypassUserCmd : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly IServiceProvider _serviceProvider;
        private readonly PlayerInstance _playerInstance;
        private int _command;

        public BypassUserCmd(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _serviceProvider = serviceProvider;
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;

            _command = packet.ReadInt();
        }

        public override async Task Execute()
        {
            // /loc "Текущая локация: 45855, 50727, -3056 (возле Деревни Эльфов)
            // /time "Текущее время 14 ч 16 мин дня.
            // "[/summon] Неверная команда."
            var userCommandHandler = _serviceProvider.GetRequiredService<UserCommandHandler>();
            await userCommandHandler.Request(_playerInstance, _command);
        }
    }
}