using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestAcquireSkill : PacketBase
    {
        private readonly int _id;
        private readonly int _level;
        private readonly int _skillType;
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerSkill _playerSkill;
        private readonly SkillDataInit _skillService;

        public RequestAcquireSkill(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _id = packet.ReadInt();
            _level = packet.ReadInt();
            _skillType = packet.ReadInt();
            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
            _playerSkill = _playerInstance.PlayerSkill();
            _skillService = Initializer.SkillDataInit();
        }

        public override async Task Execute()
        {
            NpcInstance npcInstance = _playerInstance.LastTalkedNpc;
            if (npcInstance == null)
            {
                return;
            }

            if (_playerSkill.GetSkillLevel(_id) >= _level)
            {
                // already knows the skill with this level
                return;
            }
            AddSkillToPlayer();

            await SendStatusUpdateAsync();
            await SendSpDecreasedAsync();
            await SendLearnedSkillAsync();
            await npcInstance.NpcLearnSkill().ShowSkillList(_playerInstance);

            await _playerSkill.SendSkillListAsync();
        }

        private async Task SendLearnedSkillAsync()
        {
            var sm = new SystemMessage(SystemMessageId.LearnedSkillS1);
            sm.AddSkillName(_id, 1);
            await _playerInstance.SendPacketAsync(sm);
        }

        private async Task SendSpDecreasedAsync()
        {
            SystemMessage sp = new SystemMessage(SystemMessageId.SpDecreasedS1);
            sp.AddNumber(5);
            await _playerInstance.SendPacketAsync(sp);
        }

        private async Task SendStatusUpdateAsync()
        {
            var su = new StatusUpdate(_playerInstance.ObjectId);
            su.AddAttribute(StatusUpdate.Sp, (int) _playerInstance.PlayerCharacterInfo().Sp);
            await _playerInstance.SendPacketAsync(su);
        }

        private void AddSkillToPlayer()
        {
            var skill = _skillService.GetSkillBySkillIdAndLevel(_id, _level);
            _playerSkill.AddSkill(skill, true);
        }
    }
}