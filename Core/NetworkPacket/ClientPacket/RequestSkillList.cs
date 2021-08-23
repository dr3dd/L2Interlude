using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestSkillList : PacketBase
    {
        private readonly PlayerSkill _playerSkill;
        
        public RequestSkillList(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            var playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _playerSkill = playerInstance.PlayerSkill();
        }

        public override async Task Execute()
        {
            await _playerSkill.SendSkillListAsync();
        }
    }
}