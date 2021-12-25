using System;

namespace NpcService.Ai
{
    public class Warrior : WarriorParameter
    {
        public virtual float Attack_DecayRatio { get; set; } = 6.600000f;
        public virtual float UseSkill_DecayRatio { get; set; } = 66000.000000f;
        public virtual float Attack_BoostValue { get; set; } = 300.000000f;
        public virtual float UseSkill_BoostValue { get; set; } = 100000.000000f;
        
        public virtual void NoDesire()
        {
            //MySelf.AddMoveAroundDesire(5, 5);
        }

        public override void Created()
        {
            //throw new NotImplementedException();
            MySelf.AddMoveAroundDesire(5, 5);
        }

        public override void Talked(Talker talker)
        {
            throw new NotImplementedException();
        }

        public override void TimerFiredEx(int timerId)
        {
            throw new NotImplementedException();
        }
    }
}