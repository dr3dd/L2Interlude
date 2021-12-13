using System;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcCitizen.Elf
{
    public class Andellria : Citizen
    {
        public Andellria(IServiceProvider serviceProvider, NpcService npcService) :
            base(serviceProvider, npcService)
        {
        }
    }
}