using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestSkillCoolTime : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly PlayerInstance _playerInstance;
        public RequestSkillCoolTime(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _playerInstance = _controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            await _controller.SendPacketAsync(new SkillCoolTime());
        }
    }
}