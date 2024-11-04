using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class InstantTeleporter : Citizen
{
    public override string FnHi { get; set; } = "thi.htm";

    public override async Task Talked(Talker talker)
    {
        await MySelf.ShowPage(talker, FnHi);
    }
}