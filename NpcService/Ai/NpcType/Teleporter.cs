using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;

namespace NpcService.Ai.NpcType
{
    public class Teleporter : AnnounceRaidBossPosition
    {
        public virtual string ShopName => "";
        public virtual string FnHi { get; set; } = "thi.htm";
        public virtual string FnYouAreChaotic { get; set; } = "tcm.htm";
        public virtual string FnNobless { get; set; } = "";
        public virtual string FnNoNobless { get; set; } = "";
        public virtual string FnNoNoblessItem { get; set; } = "";
        
        public virtual int PrimeHours => 0;
        public virtual int PHfromHour => 20;
        public virtual int PHtoHour => 8;
        public virtual int PHfromDay => 1;
        public virtual int PHtoDay => 7;
        /*
        public virtual object[,] Position => new object[,]
        {
            {"Talking Island Village", -84169, 244693, -3729, 100000, 0 },
        };
        */
        public virtual IList<TeleportList> Position => new List<TeleportList>
        {
            {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
        };
        

        public virtual async Task TeleportRequested(Talker talker)
        {
            await MySelf.Teleport(talker, Position, ShopName, "", "", "", 57,
                MySelf.MakeFString(1000308, "", "", "", "", ""));
        }
        
        
        public override void Talked(Talker talker)
        {
            MySelf.ShowPage(talker, talker.Karma > 0 ? FnYouAreChaotic : FnHi);
        }
    }
}