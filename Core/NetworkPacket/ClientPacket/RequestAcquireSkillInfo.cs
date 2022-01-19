using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Network;

namespace Core.NetworkPacket.ClientPacket
{
    internal sealed class RequestAcquireSkillInfo : PacketBase
    {
        private readonly int _id;
        private readonly int _level;
        private readonly int _skillType;
        private readonly PlayerInstance _playerInstance;
        
        public RequestAcquireSkillInfo(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _id = packet.ReadInt();
            _level = packet.ReadInt();
            _skillType = packet.ReadInt();

            _playerInstance = controller.GameServiceHelper.CurrentPlayer;
        }

        public override async Task Execute()
        {
            NpcInstance npcInstance = _playerInstance.LastTalkedNpc;
            if (npcInstance == null)
            {
                return;
            }
            SkillDataModel skill = Initializer.SkillDataInit().GetSkillBySkillIdAndLevel(_id, _level);
            await GetSkillType(skill);
        }
        
        private async Task GetSkillType(SkillDataModel skill)
        {
            AcquireSkillInfo acquireSkillInfo;
            switch (_skillType)
            {
                case 0:
                    acquireSkillInfo = BasicSkills(skill);
                    await _playerInstance.SendPacketAsync(acquireSkillInfo);
                    break;
                case 2:
                    //Todo Pledge Skills
                    //PledgeSkills();
                    break;
                default:
                    acquireSkillInfo = BasicSkills(skill);
                    await _playerInstance.SendPacketAsync(acquireSkillInfo);
                    break;
            }
        }
        
        private AcquireSkillInfo BasicSkills(SkillDataModel skill)
        {
            var classKey = _playerInstance.PlayerCharacterInfo().ClassName;
            var skillList = Initializer.SkillAcquireInit().GetSkillAcquireListByClassKey(classKey);

            var skillLearn = skillList.FirstOrDefault(s => s.SkillName == skill.SkillName);
            if (skillLearn == null)
                return new AcquireSkillInfo(0, 0, 0, 0);
            
            AcquireSkillInfo acquireSkillInfo = new AcquireSkillInfo(skill.SkillId, skill.Level, skillLearn.LevelUpSp, 0);
            //TODO check if skill require a book
            int spbId = -1;
            if (spbId > -1)
            {
                acquireSkillInfo.AddRequirement(99, spbId, 1, 50);
            }
            return acquireSkillInfo;
        }
    }
}