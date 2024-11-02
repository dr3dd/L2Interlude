using System.Threading.Tasks;
using Helpers;

namespace Core.Module.NpcAi.Ai;

public class Citizen : DefaultNpc
{
    public virtual string FnTradeSkill0 { get; set; } = "";
    public virtual string FnTradeSkill1 { get; set; } = "";
    public virtual string FnTradeSkill2 { get; set; } = "";
    public virtual string FnHi { get; set; } = "chi.htm";
    public virtual string FnFeudInfo { get; set; } = "defaultfeudinfo.htm";
    public virtual string FnNoFeudInfo { get; set; } = "nofeudinfo.htm";
    public virtual string FnYouAreChaotic { get; set; } = "wyac.htm";
    public virtual string FnBracketL { get; set; } = "[";
    public virtual string FnBracketR { get; set; } = "]";
    public override int MoveAroundSocial { get; set; }
    public override int MoveAroundSocial1 { get; set; }
    public override int MoveAroundSocial2 { get; set; }
    public virtual int HavePet { get; set; } = 0;
    public virtual int Silhouette { get; set; } = 1000130;
    public virtual int FriendShip1 { get; set; } = 0;
    public virtual int FriendShip2 { get; set; } = 0;
    public virtual int FriendShip3 { get; set; } = 0;
    public virtual int FriendShip4 { get; set; } = 0;
    public virtual int FriendShip5 { get; set; } = 0;
    public virtual int NoFnHi { get; set; }
    public virtual string FnNoFriend { get; set; }
        
    public override void Created()
    {
        if (MoveAroundSocial > 0 || MoveAroundSocial1 > 0)
        {
            MySelf.AddTimerEx(1671, 10000);
        }
    }

    public override async Task Talked(Talker talker)
    {
        if (talker.Karma > 0)
        {
            await MySelf.ShowPage(talker, FnYouAreChaotic);
            return;
        }
        if (NoFnHi == 1)
        {
            return;
        }
        if (FriendShip1 == 0)
        {
            await MySelf.ShowPage(talker, FnHi);
        }
        else if (MySelf.OwnItemCount(talker, FriendShip1) > 0 || MySelf.OwnItemCount(talker, FriendShip2) > 0 ||
                 MySelf.OwnItemCount(talker, FriendShip3) > 0 || MySelf.OwnItemCount(talker, FriendShip4) > 0 ||
                 MySelf.OwnItemCount(talker, FriendShip5) > 0)
        {
            await MySelf.ShowPage(talker, FnHi);
        }
        else
        {
            await MySelf.ShowPage(talker, FnNoFriend);
        }
    }

    public override async Task TimerFiredEx(int timerId)
    {
        if (MoveAroundSocial > 0 && Rnd.Next(100) < 40)
        {
            await MySelf.AddEffectActionDesire(MySelf.Sm, 3, ((MoveAroundSocial * 1000) / 30), 50);
        }
        else if (MoveAroundSocial1 > 0 && Rnd.Next(100) < 40)
        {
            await MySelf.AddEffectActionDesire(MySelf.Sm, 2, ((MoveAroundSocial1 * 1000) / 30), 50);
        }
        MySelf.AddTimerEx(1671, 10000);
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == -1000)
        {
            switch (reply)
            {
                case 0:
                    await MySelf.ShowPage(talker, FnHi);
                    break;
                case 1:
                    if (MySelf.Sm.ResidenceId >= 0)
                    {
                        if (MySelf.Castle_GetPledgeId())
                        {
                            MySelf.FHTML_SetFileName(ref fhtml0, FnFeudInfo);
                            MySelf.FHTML_SetStr(ref fhtml0, "my_pledge_name", MySelf.Castle_GetPledgeName());
                            MySelf.FHTML_SetStr(ref fhtml0, "my_owner_name", MySelf.Castle_GetOwnerName());
                            MySelf.FHTML_SetInt(ref fhtml0, "current_tax_rate", MySelf.Residence_GetTaxRateCurrent());
                        }
                        else
                        {
                            MySelf.FHTML_SetFileName(ref fhtml0, FnNoFeudInfo);
                        }
                        if (MySelf.Sm.ResidenceId < 7)
                        {
                            MySelf.FHTML_SetStr(ref fhtml0, "kingdom_name", MySelf.MakeFString(1001000, "", "", "", "", ""));
                        }
                        else
                        {
                            MySelf.FHTML_SetStr(ref fhtml0, "kingdom_name", MySelf.MakeFString(1001100, "", "", "", "", ""));
                        }
                        MySelf.FHTML_SetStr(ref fhtml0, "feud_name", MySelf.MakeFString((1001000 + MySelf.Sm.ResidenceId), "", "", "", "", ""));
                        await MySelf.ShowFHTML(talker, fhtml0);
                    }
                    break;
                default:
                    break;
            }
        }

        else if (ask == -303)
        {
            if (reply == 203)
            {
                if (MySelf.GetSSQStatus() == 3)
                {
                    if (MySelf.GetSSQPart(talker) != 0)
                    {
                        await MySelf.ShowMultiSell(reply, talker);
                    }
                }
            }
            else if (reply == 532 || reply == 549)
            {
                /*
                if (gg.IsEventServer())
                {
                    await MySelf.ShowMultiSell(reply, talker);
                }
                */
            }
            else
            {
                await MySelf.ShowMultiSell(reply, talker);
            }
        }
        else
        {
            await base.MenuSelected(talker, ask, reply, fhtml0);
        }
    }
}