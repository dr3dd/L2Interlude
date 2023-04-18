using System;
using System.Threading.Tasks;
using Helpers;

namespace Core.Module.NpcAi.Ai;

public class Warrior : WarriorParameter
{
    public virtual float Attack_DecayRatio { get; set; } = 6.600000f;
    public virtual float UseSkill_DecayRatio { get; set; } = 66000.000000f;
    public virtual float Attack_BoostValue { get; set; } = 300.000000f;
    public virtual float UseSkill_BoostValue { get; set; } = 100000.000000f;
        
    public override async Task NoDesire()
    {
        await MySelf.AddMoveAroundDesire(5, 5);
    }

    public override void Created()
    {
        if(ShoutMsg1 > 0)
        {
            //MySelf.Shout(MySelf.MakeFString(ShoutMsg1, "", "", "", "", ""));
        }
        if (MoveAroundSocial > 0 || ShoutMsg2 > 0 || ShoutMsg3 > 0)
        {
            MySelf.AddTimerEx(1001, 10000);
        }
    }

    public override Task Talked(Talker talker)
    {
        throw new NotImplementedException();
    }

    public override async Task TimerFiredEx(int timerId)
    {
        if (MoveAroundSocial > 0 && Rnd.Next(100) < 40)
        {
            await MySelf.AddEffectActionDesire(MySelf.Sm, 3, ((MoveAroundSocial * 1000) / 30), 50);
        }
        else if (MoveAroundSocial1 > 0 && Rnd.Next(100) < 40)
        {
            await MySelf.AddEffectActionDesire(MySelf.Sm, 2, ((MoveAroundSocial1 * 1000) / 30), 50);
        }
        MySelf.AddTimerEx(1001, 10000);
    }
}