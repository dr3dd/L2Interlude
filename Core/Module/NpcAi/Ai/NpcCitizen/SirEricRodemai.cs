using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class SirEricRodemai : SirEricRodemai1
{
    public override async Task MenuSelected(Talker talker, int ask, int reply)
    {
        if (ask == 503)
        {

        }
        if (ask == 508)
        {

            await base.MenuSelected(talker, ask, reply);
        }
    }
}