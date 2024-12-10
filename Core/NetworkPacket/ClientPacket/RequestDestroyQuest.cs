using Core.Controller;
using Core.Module.Player;
using Network;
using System;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: L2Repository
//DATE: 26.11.2024 12:52:09

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestDestroyQuest : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly PlayerInstance _playerInstance;
        private int _questId;

        public RequestDestroyQuest(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;

            _questId = packet.ReadInt();
        }

        public override async Task Execute()
        {
            await _playerInstance.PlayerQuest().DestroyQuest(_questId);
        }
    }
}