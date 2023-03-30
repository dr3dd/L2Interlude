namespace NpcAi.Ai
{
    public interface IWarrior
    {
        void NoDesire();
        void Created();
        void TimerFiredEx();
        void ClanAttacked();
        void SeeSpell();
        void DesireManipulation();
        void MyDying();
    }
}