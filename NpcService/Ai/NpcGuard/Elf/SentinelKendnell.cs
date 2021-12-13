using System;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcGuard.Elf
{
    public class SentinelKendnell : Guard
    {
        public SentinelKendnell(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
    }
}