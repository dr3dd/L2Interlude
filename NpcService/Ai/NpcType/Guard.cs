using System;

namespace NpcService.Ai.NpcType
{
    public class Guard : Citizen
    {
        protected Guard(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
    }
}