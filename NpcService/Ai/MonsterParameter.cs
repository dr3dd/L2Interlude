using System;

namespace NpcService.Ai
{
    public abstract class MonsterParameter : DefaultNpc
    {
        protected MonsterParameter(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
    }
}