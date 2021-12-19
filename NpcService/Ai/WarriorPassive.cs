using System;

namespace NpcService.Ai
{
    public class WarriorPassive : Warrior
    {
        public WarriorPassive(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
    }
}