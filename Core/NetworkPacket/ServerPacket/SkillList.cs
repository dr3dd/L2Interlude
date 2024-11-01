using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.SkillData;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class SkillList : Network.ServerPacket
    {
        private readonly IList<SkillDataModel> _skills;

        public SkillList()
        {
            _skills = new List<SkillDataModel>();
        }
        public void AddSkill(SkillDataModel skillDataModel)
        {
            _skills.Add(skillDataModel);
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x58);
            await WriteIntAsync(_skills.Count);
		
            foreach (SkillDataModel temp in _skills)
            {
                await WriteIntAsync(temp.OperateType == OperateType.P ? 1 : 0);
                await WriteIntAsync(temp.Level);
                await WriteIntAsync(temp.SkillId);
                await WriteByteAsync(0x00); // c5
            }
        }
    }
}