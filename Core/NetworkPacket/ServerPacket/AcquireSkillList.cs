using System.Collections.Generic;

namespace Core.NetworkPacket.ServerPacket
{
    internal sealed class AcquireSkillList : Network.ServerPacket
    {
        public enum SkillType
        {
            Usual,
            Fishing,
            Clan
        }
        private readonly List<Skill> _skills;
        private SkillType _fishingSkills;
        
        private class Skill
        {
            public int Id { get; }
            public int NextLevel { get; }
            public int MaxLevel { get; }
            public int SpCost { get; }
            public int Requirements { get; }
		
            public Skill(int pId, int pNextLevel, int pMaxLevel, int pSpCost, int pRequirements)
            {
                Id = pId;
                NextLevel = pNextLevel;
                MaxLevel = pMaxLevel;
                SpCost = pSpCost;
                Requirements = pRequirements;
            }
        }
        
        public AcquireSkillList(SkillType type)
        {
            _skills = new List<Skill>();
            _fishingSkills = type;
        }
	
        public void AddSkill(int id, int nextLevel, int maxLevel, int spCost, int requirements)
        {
            _skills.Add(new Skill(id, nextLevel, maxLevel, spCost, requirements));
        }
        
        public override void Write()
        {
            WriteByte(0x8a);
            WriteInt(1); // c4 : C5 : 0: usuall 1: fishing 2: clans
            WriteInt(_skills.Count);

            _skills.ForEach(skill =>
            {
                WriteInt(skill.Id);
                WriteInt(skill.NextLevel);
                WriteInt(skill.MaxLevel);
                WriteInt(skill.SpCost);
                WriteInt(skill.Requirements);
            });
        }
    }
}