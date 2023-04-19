using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class MerchantForNewbie : Merchant
{
    public override async Task MenuSelected(Talker talker, int ask, int reply)
    {
        if(ask == -305)
        {
            if(MySelf.IsNewbie(talker) && MySelf.IsInCategory(7, talker.Occupation))
            {
                await MySelf.ShowMultiSell(201, talker);
            }
            else
            {
                await MySelf.ShowPage(talker, "merchant_for_newbie001.htm");
            }
        }
        await base.MenuSelected(talker, ask, reply);
    }
}