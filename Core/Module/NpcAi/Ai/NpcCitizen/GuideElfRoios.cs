using Helpers;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class GuideElfRoios : NewbieGuide
{

    public override IList<TeleportList> NewbieTokenTeleports => new List<TeleportList>
    {
        new("Dark Elf Village", 9716, 15502, -4500, 1, 0),
        new("Dwarven Village", 115120, -178112, -880, 1, 0),
        new("Talking Island Village", -84141, 244623, -3729, 1, 0),
        new("Orc Village", -45186, -112459, -236, 1, 0)
    };

    public override async Task MenuSelected(Talker talker, int ask, int reply)
    {
        if (ask == 203)
        {
            /*
            MySelf.SetCurrentQuestID(@elf_tutorial);
            if (MySelf.GetInventoryInfo(talker, 0) >= (MySelf.GetInventoryInfo(talker, 1) * 0.800000) || MySelf.GetInventoryInfo(talker, 2) >= (MySelf.GetInventoryInfo(talker, 3) * 0.800000))
            {
                await MySelf.ShowSystemMessage(talker, 1118);
            }
            */
            return;
        }
        if (ask == 255)
        {
            if (reply == 10)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04.htm");
            }
            else if (reply == 11)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04a.htm");
            }
            else if (reply == 12)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04b.htm");
            }
            else if (reply == 13)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04c.htm");
            }
            else if (reply == 14)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04d.htm");
            }
            else if (reply == 15)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04e.htm");
            }
            else if (reply == 16)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04f.htm");
            }
            else if (reply == 17)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04g.htm");
            }
            else if (reply == 18)
            {
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_04h.htm");
            }
            else if (reply == 31)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 46926, 51511, -2977, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 32)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 44995, 51706, -2803, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 33)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45727, 51721, -2803, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 34)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 42812, 51138, -2996, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 35)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45487, 46511, -2996, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 36)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 47401, 51764, -2996, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 37)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 42971, 51372, -2996, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 38)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 47595, 51569, -2996, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 39)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45778, 46534, -2996, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 40)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 44476, 47153, -2984, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 41)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 42700, 50057, -2984, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 42)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 42766, 50037, -2984, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 43)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 44683, 46952, -2981, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 44)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 44667, 46896, -2982, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 45)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45725, 52105, -2795, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 46)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 44823, 52414, -2795, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 47)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45000, 52101, -2795, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 48)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45919, 52414, -2795, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 49)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 44692, 52261, -2795, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 50)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 47780, 49568, -2983, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 51)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 47912, 50170, -2983, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 52)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 47868, 50167, -2983, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 53)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 28928, 74248, -3773, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 54)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 43673, 49683, -3046, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 55)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45610, 49008, -3059, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 56)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 50592, 54986, -3376, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 57)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 42978, 49115, -2994, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 58)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 46475, 50495, -3058, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 59)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 45859, 50827, -3058, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 60)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 51210, 82474, -3283, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
            else if (reply == 61)
            {
                await MySelf.DeleteAllRadar(talker, 2);
                await MySelf.ShowRadar(talker, 49262, 53607, -3216, 2);
                await MySelf.ShowPage(talker, "guide_elf_roios_q0255_05.htm");
            }
        }
        await base.MenuSelected(talker, ask, reply);
    }

}