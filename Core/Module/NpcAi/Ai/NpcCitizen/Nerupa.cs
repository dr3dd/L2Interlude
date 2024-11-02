using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class Nerupa : Citizen
{
    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == 203)
        {
            MySelf.SetCurrentQuestID("elf_tutorial");
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
                return;
            }
        }
        else if (ask == 420)
        {
            if (reply == 31 && MySelf.OwnItemCount(talker, "leaf_of_mothertree") > 0)
            {
                if (MySelf.IsInCategory(0, talker.Occupation) && MySelf.OwnItemCount(talker, "soulshot_none_for_rookie") <= 200)
                {
                    MySelf.VoiceEffect(talker, "tutorial_voice_026", 1000);
                    MySelf.GiveItem1(talker, "soulshot_none_for_rookie", 200);
                    MySelf.IncrementParam(talker, 1, 50);
                }
                if (MySelf.IsInCategory(1, talker.Occupation) && MySelf.OwnItemCount(talker, "soulshot_none_for_rookie") <= 200 && MySelf.OwnItemCount(talker, "spiritshot_none_for_rookie") <= 100)
                {
                    MySelf.VoiceEffect(talker, "tutorial_voice_027", 1000);
                    MySelf.GiveItem1(talker, "spiritshot_none_for_rookie", 100);
                    MySelf.IncrementParam(talker, 1, 50);
                }
                await MySelf.ShowPage(talker, "nerupa002.htm");
                MySelf.DeleteItem1(talker, "leaf_of_mothertree", 1);
                MySelf.AddTimerEx((MySelf.GetIndexFromCreature(talker) + 1000000), (1000 * 60));
                await MySelf.ShowRadar(talker, 45475, 48359, -3060, 2);
                if (MySelf.GetMemoStateEx(talker, 255, "letters_of_love1") <= 3)
                {
                    MySelf.SetMemoStateEx(talker, 255, "letters_of_love1", 4);
                }
            }
        }
        else
        {
            await base.MenuSelected(talker, ask, reply, fhtml0);
        }
    }
}