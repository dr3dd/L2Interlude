using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public abstract class DefaultNpc
{
    protected int ResidenceId = 0;
    protected int DesirePqSize = 50;
    protected int FavorListSize = 30;
    public virtual int MoveAroundSocial { get; set; }
    public virtual int MoveAroundSocial1 { get; set; }
    public virtual int MoveAroundSocial2 { get; set; }
        
    protected double IdleDesireDecayRatio = 0.000000;
    protected double MoveAroundDecayRatio = 0.000000;
    protected double DoNothingDecayRatio = 0.000000;
    protected double AttackDecayRatio = 0.000000;
    protected double ChaseDecayRatio = 0.000000;
    protected double FleeDecayRatio = 0.000000;
    protected double GetItemDecayRatio = 0.000000;
    protected double FollowDecayRatio = 0.000000;
    protected double DecayingDecayRatio = 0.000000;
    protected double MoveToWayPointDecayRatio = 0.000000;
    protected double UseSkillDecayRatio = 0.000000;
    protected double MoveToDecayRatio = 0.000000;
    protected double EffectActionDecayRatio = 0.000000;
    protected double MoveToTargetDecayRatio = 0.000000;
    protected double IdleDesireBoostValue = 0.000000;
    protected double MoveAroundBoostValue = 0.000000;
    protected double DoNothingBoostValue = 0.000000;
    protected double AttackBoostValue = 0.000000;
    protected double ChaseBoostValue = 0.000000;
    protected double FleeBoostValue = 0.000000;
    protected double GetItemBoostValue = 0.000000;
    protected double FollowBoostValue = 0.000000;
    protected double DecayingBoostValue = 0.000000;
    protected double MoveToWayPointBoostValue = 0.000000;
    protected double UseSkillBoostValue = 0.000000;
    protected double MoveToBoostValue = 0.000000;
    protected double EffectActionBoostValue = 0.000000;
    protected double MoveToTargetBoostValue = 0.000000;
        
    public NpcData.NpcAi MySelf { get; set; }
    public Talker Talker { get; set; }

    public abstract void Created();
    public abstract Task Talked(Talker talker);
    public abstract void TimerFiredEx(int timerId);

        
    public virtual void NoDesire()
    {
    }
        
    public virtual void Attacked(Talker talker, int damage)
    {
    }

    public virtual Task TeleportRequested(Talker talker)
    {
        return Task.FromResult(1);
    }
    public virtual Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        return Task.FromResult(1);
    }
        
    public virtual async Task TalkSelected(Talker talker)
    {
        await MySelf.ShowPage(talker, "noquest.htm");
    }
}