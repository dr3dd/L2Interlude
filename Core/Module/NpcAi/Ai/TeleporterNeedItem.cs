using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class TeleporterNeedItem : Teleporter
{
    public virtual string FnNoItem => "";
    public virtual int ItemNeeded => 1;
    public override async Task MenuSelected(Talker talker, int ask, int reply)
    {
        if (ask == -6)
        {
            if (MySelf.OwnItemCount(talker, "small_sword") != 0)
            {
                MySelf.DeleteItem1(talker, "small_sword", ItemNeeded);
            }
            else
            {
                await MySelf.ShowPage(talker, FnNoItem);
            }
        }
        await base.MenuSelected(talker, ask, reply);
    }
}