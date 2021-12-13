using System;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcCitizen.Elf
{
    public class Rizraell : Citizen
    {
        public Rizraell(IServiceProvider serviceProvider, NpcService npcService) :
            base(serviceProvider, npcService)
        {
        }

        public override void Created()
        {
            base.Created();
        }
    }
}