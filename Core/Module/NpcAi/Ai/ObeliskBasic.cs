using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class ObeliskBasic : Citizen
{
    public override async Task Talked(Talker talker)
    {
        if (talker.NoblessType == 1)
        {
            await MySelf.ShowPage(talker, "obelisk001.htm");
        }
        else
        {
            await MySelf.ShowPage(talker, "obelisk001a.htm");
        }
    }
}