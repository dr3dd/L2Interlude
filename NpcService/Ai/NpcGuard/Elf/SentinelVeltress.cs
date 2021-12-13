using System;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcGuard.Elf
{
    public class SentinelVeltress : Guard
    {
        public SentinelVeltress(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
    }
}