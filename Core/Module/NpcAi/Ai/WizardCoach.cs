using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class WizardCoach : GuildCoach
{
    public override async Task LearnSkillRequested(Talker talker)
    {
        if ((((MySelf.Sm.Race == 0 && MySelf.IsInCategory(32, talker.Occupation)) ||
              (MySelf.Sm.Race == 1 && MySelf.IsInCategory(37, talker.Occupation))) ||
             (MySelf.Sm.Race == 2 && MySelf.IsInCategory(41, talker.Occupation))))
        {
            await MySelf.ShowSkillList(talker, "");
        }
        else
            
        {
            await MySelf.ShowPage(talker, FnClassMismatch);
        }
    }
    
    public override async Task EnchantSkillRequested(Talker talker)
    {
        if ((((MySelf.Sm.Race == 0 && MySelf.IsInCategory(32, talker.Occupation)) ||
              (MySelf.Sm.Race == 1 && MySelf.IsInCategory(37, talker.Occupation))) ||
             (MySelf.Sm.Race == 2 && MySelf.IsInCategory(41, talker.Occupation))))
        {
            if(MySelf.IsInCategory(8, talker.Occupation))
            {
                if(talker.Level > 75)
                {
                    await MySelf.ShowEnchantSkillList(talker);
                }
                else
                {
                    await MySelf.ShowPage(talker, FnLevelMismatch);
                }
            }
            else
            {
                await MySelf.ShowPage(talker, FnNotFourthClass);
            }
        }
        else
        {
            await MySelf.ShowPage(talker, FnClassMismatch);
        }
    }
}