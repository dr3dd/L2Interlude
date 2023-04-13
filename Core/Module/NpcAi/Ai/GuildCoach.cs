using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class GuildCoach : Citizen
{
    public override string FnHi { get; set; } = "master_aiken001.htm";
    public virtual string FnClassMismatch { get; set; } = "gcm.htm";
    public virtual string FnNotFourthClass { get; set; } = "skillenchant_notfourthclass.htm";
    public virtual string FnLevelMismatch { get; set; } = "skillenchant_levelmismatch.htm";
    public override string FnYouAreChaotic { get; set; } = "wyac.htm";

    public override async Task Talked(Talker talker)
    {
        if (talker.Karma > 0)
        {
            await MySelf.ShowPage(talker, FnYouAreChaotic);
        }
        else
        {
            await MySelf.ShowPage(talker, FnHi);
        }
    }
    
    public virtual async Task LearnSkillRequested(Talker talker)
    {
        await MySelf.ShowSkillList(talker, "");
    }
        
    public virtual async Task OneSkillSelected(Talker talker, int skillNameId, bool needQuest)
    {
        await MySelf.ShowGrowSkillMessage(talker, skillNameId, "");
    }
    
    public virtual async Task EnchantSkillRequested(Talker talker)
    {
        await MySelf.ShowEnchantSkillList(talker);
    }
    
    public virtual async Task OneEnchantSkillSelected(Talker talker, int skillNameId)
    {
        await MySelf.ShowEnchantSkillMessage(talker, skillNameId);
    }
}