using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class Guard : Citizen
{
    public override string FnHi { get; set; } = "chi.htm";
    public override string FnYouAreChaotic { get; set; } = "chi.htm";
    public override float Attack_DecayRatio { get; set; } = 6.600000F;
    public override float UseSkill_DecayRatio { get; set; } = 66000.000000F;
    public override float Attack_BoostValue { get; set; } = 500.000000F;
    public override float UseSkill_BoostValue { get; set; } = 100000.000000F;

    public override async Task Talked(Talker talker)
    {
        if (NoFnHi == 1)
        {
            return;
        }
        if (talker.Karma > 0)
        {

        }
        else
        {
            await MySelf.ShowPage(talker, FnHi);
        }
    }

    public override void Attacked(Talker attacker, int damage)
    {
        MySelf.AddAttackDesire(attacker, 1, 2000);
    }
    /*
    protected void SeeCreature(Talker creature)
    {
        if (creature.Karma > 0)
        {
            MySelf.AddAttackDesire(creature, 1, 1500);
        }
    }
    */
}