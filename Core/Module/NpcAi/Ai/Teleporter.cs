using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Enums;
using Helpers;

namespace Core.Module.NpcAi.Ai;

public class Teleporter : AnnounceRaidBossPosition
{
    public virtual string ShopName => "";
    public override string FnHi { get; set; } = "thi.htm";
    public override string FnYouAreChaotic { get; set; } = "tcm.htm";
    public virtual string FnNobless { get; set; } = "";
    public virtual string FnNoNobless { get; set; } = "";
    public virtual string FnNoNoblessItem { get; set; } = "";
        
    public virtual int PrimeHours => 0;
    public virtual int PHfromHour => 20;
    public virtual int PHtoHour => 8;
    public virtual int PHfromDay => 1;
    public virtual int PHtoDay => 7;

    public override IList<TeleportList> Position => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };
    
    public virtual IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> PositionNoblessNeedItemTown => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> PositionNoblessNoItemTown => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> PositionNoblessNeedItemField => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> PositionNoblessNoItemField => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> PositionNoblessNeedItemSSQ => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> PositionNoblessNoItemSSQ => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };
    
    public override async Task TeleportRequested(Talker talker)
    {
        /*
        if (PrimeHours == 1)
        {
            i0 = gg.GetDateTime(0, 3);
            i1 = gg.GetDateTime(0, 6);
            if (i1 >= PHfromDay && i1 <= PHtoDay)
            {
                if (i0 >= PHfromHour || i0 <= PHtoHour)
                {
                    await MySelf.Teleport(talker, PositionPrimeHours, ShopName, "", "", "", 57, "Adena");
                    return;
                }
            }
        }
        */
        await MySelf.Teleport(talker, Position, ShopName, "", "", "", 57,
            MySelf.MakeFString(1000308, "", "", "", "", ""));
    }

    public override async Task Talked(Talker talker)
    {
        await MySelf.ShowPage(talker, talker.Karma > 0 ? FnYouAreChaotic : FnHi);
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if(ask == 255)
        {
            /*
            if (MySelf.CanLotto == 1) { 
            }
            else */
            if (reply == 9)
            {
                await MySelf.ShowPage(talker, "race_gatekeeper1004.htm");
            }
            else
            {
                await MySelf.ShowPage(talker, "race_cant_teleport001.htm");
            }
        }
        else if (ask == -19)
        {
            await MySelf.ShowPage(talker, talker.NoblessType == NoblessType.NONE ? FnNobless : FnNoNobless);
        }
        else if (ask == -20)
        {
            if (reply == 1)
            {
                if (MySelf.OwnItemCount(talker, 6651) != 0)
                {
                    await MySelf.Teleport(talker, PositionNoblessNeedItemTown, ShopName, "", "", "", 6651, MySelf.MakeFString(1000454, "", "", "", "", ""));
                }
                else 
                { 
                    await MySelf.ShowPage(talker, FnNoNoblessItem);
                }
            }
            else if (reply == 2)
            {
                if (MySelf.OwnItemCount(talker, 6651) == 0)
                {
                    await MySelf.Teleport(talker, PositionNoblessNeedItemField, ShopName, "", "", "", 6651, MySelf.MakeFString(1000454, "", "", "", "", ""));
                }
                else
                {
                    await MySelf.ShowPage(talker, FnNoNoblessItem);
                }
            }
            else if (reply == 3)
            {
                if (MySelf.OwnItemCount(talker, 6651) != 0)
                {
                    await MySelf.Teleport(talker, PositionNoblessNeedItemSSQ, ShopName, "", "", "", 6651, MySelf.MakeFString(1000454, "", "", "", "", ""));
                }
                else
                {
                    await MySelf.ShowPage(talker, FnNoNoblessItem);
                }
            }
        }
        else if (ask == -21)
        {
            if (reply == 1)
            {
                await MySelf.Teleport(talker, PositionNoblessNoItemTown, ShopName, "", "", "", 57, MySelf.MakeFString(1000308, "", "", "", "", ""));
            }
            if (reply == 2)
            {
                await MySelf.Teleport(talker, PositionNoblessNoItemField, ShopName, "", "", "", 57, MySelf.MakeFString(1000308, "", "", "", "", ""));
            }
            if (reply == 3)
            {
                await MySelf.Teleport(talker, PositionNoblessNoItemSSQ, ShopName, "", "", "", 57, MySelf.MakeFString(1000308, "", "", "", "", ""));
            }
        }
        else if (ask == -22)
        {
            await MySelf.ShowPage(talker, MySelf.Sm.Name + "001.htm");
        }
        else if (ask == -23)
        {
            if (talker.Level < 52)
            {
                //await MySelf.UseSkill(talker, 1048076);
            }
            if (talker.Level < 40 && MySelf.IsInCategory(6, talker.Occupation))
            {

            }
            await MySelf.InstantTeleport(talker, 178900, 54600, -3088);
        }
        else if (ask == -24)
        {
            if (talker.Level < 52)
            {
                await MySelf.InstantTeleport(talker, -12782, 122862, -3114);
            }
        }

        await base.MenuSelected(talker, ask, reply, fhtml0);
    }

    public override IList<TeleportList> GetPositionList(int hashCode)
    {
        if (Position.GetHashCodeByValue() == hashCode)
        {
            return Position;
        }
        else if (PositionPrimeHours.GetHashCodeByValue() == hashCode)
        {
            return PositionPrimeHours;
        }
        else if (PositionNoblessNeedItemTown.GetHashCodeByValue() == hashCode)
        {
            return PositionNoblessNeedItemTown;
        }
        else if (PositionNoblessNoItemTown.GetHashCodeByValue() == hashCode)
        {
            return PositionNoblessNoItemTown;
        }
        else if (PositionNoblessNeedItemField.GetHashCodeByValue() == hashCode)
        {
            return PositionNoblessNeedItemField;
        }
        else if (PositionNoblessNoItemField.GetHashCodeByValue() == hashCode)
        {
            return PositionNoblessNoItemField;
        }
        else if (PositionNoblessNeedItemSSQ.GetHashCodeByValue() == hashCode)
        {
            return PositionNoblessNeedItemSSQ;
        }
        else if (PositionNoblessNoItemSSQ.GetHashCodeByValue() == hashCode)
        {
            return PositionNoblessNoItemSSQ;
        }
        else
        {
            return base.GetPositionList(hashCode);
        }
    }
}