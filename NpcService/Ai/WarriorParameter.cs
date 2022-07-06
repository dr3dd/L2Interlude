using System;

namespace NpcService.Ai
{
    public abstract class WarriorParameter : MonsterParameter
    {

        public virtual void Attacked(Talker attacked, int damage)
        {
            int f0 = 0;
            f0 = (int)(((1.000000 * damage) / (MySelf.Level + 7)) + ((f0 / 100) * ((1.000000 * damage) / (MySelf.Level + 7))));
            MySelf.AddAttackDesire(attacked, 1, (f0 * 100));
        }
    }
}