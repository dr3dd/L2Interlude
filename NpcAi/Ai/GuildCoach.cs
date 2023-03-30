using NpcAi.Ai.NpcType;

namespace NpcAi.Ai
{
    public class GuildCoach : Citizen
    {
        public override string FnHi { get; set; } = "master_aiken001.htm";
        public virtual string FnClassMismatch { get; set; } = "gcm.htm";
        public virtual string FnNotFourthClass { get; set; } = "skillenchant_notfourthclass.htm";
        public virtual string FnLevelMismatch { get; set; } = "skillenchant_levelmismatch.htm";
        public override string FnYouAreChaotic { get; set; } = "wyac.htm";

        public virtual void LearnSkillRequested(Talker talker)
        {
            MySelf.ShowSkillList(talker, "");
        }
        
        public virtual void OneSkillSelected(Talker talker, int skillNameId, bool needQuest)
        {
            MySelf.ShowGrowSkillMessage(talker, skillNameId, "");
        }
    }
}