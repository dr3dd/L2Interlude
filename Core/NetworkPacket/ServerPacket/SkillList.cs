using System.Collections.Generic;
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
        public override void Write()
        {
            WriteByte(0x58);
            WriteInt(_skills.Count);
		
            foreach (SkillDataModel temp in _skills)
            {
                WriteInt(temp.OperateType == OperateType.P ? 1 : 0);
                WriteInt(temp.Level);
                WriteInt(temp.SkillId);
                WriteByte(0x00); // c5
            }
        }
    }
}