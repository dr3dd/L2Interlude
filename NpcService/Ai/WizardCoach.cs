namespace NpcService.Ai
{
    public class WizardCoach : GuildCoach
    {
        public override void LearnSkillRequested(Talker talker)
        {
            if ((((MySelf.Sm.Race == 0 && MySelf.IsInCategory(32, talker.Occupation)) ||
                  (MySelf.Sm.Race == 1 && MySelf.IsInCategory(37, talker.Occupation))) ||
                 (MySelf.Sm.Race == 2 && MySelf.IsInCategory(41, talker.Occupation))))
            {
                MySelf.ShowSkillList(talker, "");
            }
            else
            
            {
                MySelf.ShowPage(talker, FnClassMismatch);
            }
        }
    }
}