using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using NpcService.Ai.NpcType;

namespace NpcService.Ai
{
    public class NewbieGuide : Citizen
    {
        public override string FnHi { get; set; } = "";
        public virtual string FnHighLevel { get; set; } = "";
        public virtual string FnRaceMisMatch { get; set; } = "";
        public virtual string FnGuideF05 { get; set; } = "";
        public virtual string FnGuideF10 { get; set; } = "";
        public virtual string FnGuideF15 { get; set; } = "";
        public virtual string FnGuideF20 { get; set; } = "";
        public virtual string FnGuideM07 { get; set; } = "";
        public virtual string FnGuideM14 { get; set; } = "";
        public virtual string FnGuideM20 { get; set; } = "";
        public virtual string ShopName => "";
        
        public virtual IList<TeleportList> NewbieTokenTeleports => new List<TeleportList>
        {
            {new("Dark Elf Village", 9716, 15502, -4500, 0, 0 )},
            {new("Dwarven Village", 115120, -178112, -880, 0, 0 )},
            {new("Talking Island Village", -84141, 244623, -3729, 0, 0 )},
            {new("Elven Village",46890, 51531, -2976, 0, 0 )},
            {new("Orc Village - Newbie Travel Token", -45186, -112459, -236, 0, 0 )},
        };
        
        public virtual async Task TeleportRequested(Talker talker)
        {
            if(talker.Level > 20)
            {
                MySelf.ShowPage(talker, FnHighLevel);
                return;
            }
            await MySelf.Teleport(talker, NewbieTokenTeleports, ShopName, "", "", "", 8542, "Newbie Travel Token");
        }
    }
}