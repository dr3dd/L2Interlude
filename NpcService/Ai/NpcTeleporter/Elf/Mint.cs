using System;
using NpcService.Ai.NpcType;

namespace NpcService.Ai.NpcTeleporter.Elf
{
    public class Mint : Teleporter
    {
        public override int PrimeHours => 1;
        public override string Position => @"{{'The Town of Gludio'; -12694; 122776; -3114; 9200; 1 };{'Dwarven Village'; 115120; -178112; -880; 23000; 0 };{'Talking Island Village'; -84141; 244623; -3729; 23000; 0 };{'Orc Village'; -45186; -112459; -236; 18000; 0 };{'Elven Forest'; 21362; 51122; -3688; 710; 0 };{'Elven Fortress'; 29294; 74968; -3776; 820; 0 };{'Neutral Zone'; -10612; 75881; -3592; 1700; 0 }}";

        public Mint(IServiceProvider serviceProvider, NpcService npcService) : base(serviceProvider, npcService)
        {
            
        }
    }
}