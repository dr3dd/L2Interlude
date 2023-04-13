using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class MageCoach : GuildCoach
{
    public override async Task LearnSkillRequested(Talker talker)
    {
        if (((((MySelf.Sm.Race == 0 && MySelf.IsInCategory(33, talker.Occupation)) ||
               (MySelf.Sm.Race == 1 && MySelf.IsInCategory(36, talker.Occupation))) ||
              (MySelf.Sm.Race == 2 && MySelf.IsInCategory(40, talker.Occupation))) ||
             (MySelf.Sm.Race == 3 && MySelf.IsInCategory(44, talker.Occupation))))
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
        if (((((MySelf.Sm.Race == 0 && MySelf.IsInCategory(33, talker.Occupation)) ||
               (MySelf.Sm.Race == 1 && MySelf.IsInCategory(36, talker.Occupation))) ||
              (MySelf.Sm.Race == 2 && MySelf.IsInCategory(40, talker.Occupation))) ||
             (MySelf.Sm.Race == 3 && MySelf.IsInCategory(44, talker.Occupation))))
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