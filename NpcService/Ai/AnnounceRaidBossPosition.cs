using System;

namespace NpcService.Ai
{
    public class AnnounceRaidBossPosition : NpcType.Citizen
    {
        protected AnnounceRaidBossPosition(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
        }
        
        public override void Talked(Talker talker)
        {
            throw new NotImplementedException();
        }
    }
}