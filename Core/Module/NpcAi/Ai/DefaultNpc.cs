using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class DefaultNpc
{
    protected int ResidenceId = 0;
    protected int DesirePqSize = 50;
    protected int FavorListSize = 30;
    public virtual int MoveAroundSocial { get; set; }
    public virtual int MoveAroundSocial1 { get; set; }
    public virtual int MoveAroundSocial2 { get; set; }

    protected double IdleDesireDecayRatio = 0.000000;
    protected double MoveAroundDecayRatio = 0.000000;
    public virtual float DoNothing_DecayRatio { get; set; } = 0.000000f;
    public virtual float Attack_DecayRatio { get; set; } = 0.000000f;
    protected double ChaseDecayRatio = 0.000000;
    protected double FleeDecayRatio = 0.000000;
    protected double GetItemDecayRatio = 0.000000;
    protected double FollowDecayRatio = 0.000000;
    protected double DecayingDecayRatio = 0.000000;
    protected double MoveToWayPointDecayRatio = 0.000000;
    public virtual float UseSkill_DecayRatio { get; set; } = 0.000000f;
    protected double MoveToDecayRatio = 0.000000;
    protected double EffectActionDecayRatio = 0.000000;
    protected double MoveToTargetDecayRatio = 0.000000;
    protected double IdleDesireBoostValue = 0.000000;
    protected double MoveAroundBoostValue = 0.000000;
    protected double DoNothingBoostValue = 0.000000;
    public virtual float Attack_BoostValue { get; set; } = 0.000000f;
    protected double ChaseBoostValue = 0.000000;
    protected double FleeBoostValue = 0.000000;
    protected double GetItemBoostValue = 0.000000;
    protected double FollowBoostValue = 0.000000;
    protected double DecayingBoostValue = 0.000000;
    protected double MoveToWayPointBoostValue = 0.000000;
    public virtual float UseSkill_BoostValue { get; set; } = 0.000000f;
    protected double MoveToBoostValue = 0.000000;
    protected double EffectActionBoostValue = 0.000000;
    protected double MoveToTargetBoostValue = 0.000000;

    public NpcData.NpcAi MySelf { get; set; }
    public Talker Talker { get; set; }

    public virtual void Created()
    {
    }

    public virtual async Task Talked(Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        await Talked(talker, _from_choice, _code);
    }
    public virtual async Task Talked(Talker talker, bool _from_choice, int _code)
    {
        await Talked(talker);
    }
    public virtual async Task Talked(Talker talker)
    {
        await MySelf.ShowPage(talker, "noquest.htm");
    }

    public virtual Task TimerFiredEx(int timerId)
    {
        return Task.FromResult(1);
    }

    public virtual Task NoDesire()
    {
        return Task.FromResult(1);
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
    
    public virtual Task MenuSelected(Talker talker, int ask, int reply)
    {
        return Task.FromResult(1);
    }

    public virtual async Task TalkSelected(Talker talker)
    {
        await TalkSelected("noquest.htm", talker, false, 0);
    }
    public virtual async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code)
    {
        await TalkSelected("noquest.htm", talker, false, 0, 0);
    }
    public virtual async Task TalkSelected(string fhtml0, Talker talker, bool _from_choice, int _code, int _choiceN)
    {
        await MySelf.ShowPage(talker, fhtml0);
    }
    public virtual Task QuestAccepted(int quest_id, Talker talker)
    {
        return Task.FromResult(1);
    }
}