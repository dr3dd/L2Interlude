using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.NpcAi.Models;

namespace Core.Module.NpcAi.Ai;

public class Merchant : Citizen
{
    public string ShopName { get; set; } = "";
    public override string FnHi { get; set; } = "mhi.htm";
    public virtual string FnSell { get; set; } = "msell.htm";
    public virtual string FnBuy { get; set; } = "mbye.htm";
    public virtual string FnUnableItemSell { get; set; } = "muib.htm";
    public override string FnYouAreChaotic { get; set; } = "mcm.htm";
    public virtual string FnNowSiege { get; set; } = "mns.htm";

    public virtual IList<BuySellList> SellList0 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList1 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList2 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList3 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList4 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList5 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList6 => new List<BuySellList>();
    public virtual IList<BuySellList> SellList7 => new List<BuySellList>();

    public override async Task Talked(Talker talker)
    {
        if(talker.Karma > 0)
        {
            await MySelf.ShowPage(talker, FnYouAreChaotic);
        }
        else
        {
            await MySelf.ShowPage(talker, FnHi);
        }
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply)
    {
        if(ask == -1)
        {
            if(reply == 0)
            {
                await MySelf.Sell(talker, SellList0, ShopName, FnBuy, "", "");
            }
            if(reply == 1)
            {
                await MySelf.Sell(talker, SellList1, ShopName, FnBuy, "", "");
            }
            if(reply == 2)
            {
                await MySelf.SellPreview(talker, SellList0, ShopName, FnBuy, "", "");
            }
            if(reply == 3)
            {
                await MySelf.SellPreview(talker, SellList1, ShopName, FnBuy, "", "");
            }
            if(reply == 4)
            {
                await MySelf.Sell(talker, SellList4, ShopName, FnBuy, "", "");
            }
            if(reply == 5)
            {
                await MySelf.Sell(talker, SellList5, ShopName, FnBuy, "", "");
            }
            if(reply == 6)
            {
                await MySelf.Sell(talker, SellList6, ShopName, FnBuy, "", "");
            }
            if(reply == 7)
            {
                await MySelf.Sell(talker, SellList7, ShopName, FnBuy, "", "");
            }
            if(reply == 8)
            {
                //MySelf.Buy(talker, BuyList0, ShopName, FnBuy, FnUnableItemSell, -50);
            }
            if(reply == 9)
            {
                //MySelf.Buy(talker, BuyList1, ShopName, FnBuy, FnUnableItemSell, -50);
            }
            if(reply == 10)
            {
                //MySelf.Buy(talker, BuyList2, ShopName, FnBuy, FnUnableItemSell, -50);
            }
            if(reply == 11)
            {
                //MySelf.Buy(talker, BuyList3, ShopName, FnBuy, FnUnableItemSell, -50);
            }
            if(reply == 12)
            {
                //MySelf.Buy(talker, BuyList4, ShopName, FnBuy, FnUnableItemSell, -50);
            }
        }
        await base.MenuSelected(talker, ask, reply);
    }
}