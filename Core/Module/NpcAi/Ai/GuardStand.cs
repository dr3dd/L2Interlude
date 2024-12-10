using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class GuardStand : Guard
{
    public override float DoNothing_DecayRatio { get; set; } = 000000F;
    public override async Task NoDesire()
    {
        //MySelf.AddMoveToDesire(MySelf.StartX, MySelf.StartY, MySelf.StartZ, 30);
    }
    /*
    public override void MoveToFinished(int x, int y, int z)
    {
        if (x == MySelf.start_x && y == MySelf.start_y && z == MySelf.start_z)
        {
            MySelf.AddDoNothingDesire(40, 30);
        }
        else
        {
            MySelf.AddMoveToDesire(MySelf.start_x, MySelf.start_y, MySelf.start_z, 30);
        }
    }
    */
}