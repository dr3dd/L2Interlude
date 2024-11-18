using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class BlueprintSellerDaeger : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new("sb_summon_mechanic_golem1", 10, 0.000000, 0)
    };

}