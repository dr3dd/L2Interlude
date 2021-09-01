using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.Player;
using Core.Module.SkillData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestMagicSkillUse : PacketBase
    {
        private readonly int _skillId;
        private readonly bool _ctrlPressed;
        private readonly bool _shiftPressed;
        private readonly PlayerInstance _playerInstance;
        private readonly SkillDataInit _skillDataInit;
        
        public RequestMagicSkillUse(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) :
            base(serviceProvider)
        {
            _skillId = packet.ReadInt(); // Identifier of the used skill
            _ctrlPressed = packet.ReadInt() != 0; // True if it's a ForceAttack : Ctrl pressed
            _shiftPressed = packet.ReadByte() != 0; // True if Shift pressed
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _skillDataInit = serviceProvider.GetRequiredService<SkillDataInit>();
        }

        public override async Task Execute()
        {
            int level = _playerInstance.PlayerSkill().GetSkillLevel(_skillId);
            SkillDataModel skill = _skillDataInit.GetSkillBySkillIdAndLevel(_skillId, level);
            await _playerInstance.PlayerSkillMagic().UseMagicAsync(skill, _ctrlPressed, _shiftPressed);
        }
    }
}