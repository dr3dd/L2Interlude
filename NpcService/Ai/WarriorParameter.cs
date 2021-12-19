using System;

namespace NpcService.Ai
{
    public abstract class WarriorParameter : MonsterParameter
    {
        protected WarriorParameter(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
    }
}