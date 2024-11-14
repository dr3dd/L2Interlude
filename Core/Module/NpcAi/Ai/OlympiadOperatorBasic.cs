using Core.Enums;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class OlympiadOperatorBasic : Citizen
{
    public override async Task Talked(Talker talker)
    {
        if (talker.Karma > 0)
        {
            await MySelf.ShowPage(talker, "wyac.htm");
            return;
        }
        
        if (talker.NoblessType == NoblessType.ACTIVE)
        {
            await MySelf.ShowPage(talker, "olympiad_operator001.htm");
        }
        else
        {
            await MySelf.ShowPage(talker, "olympiad_operator002.htm");
        }
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == -50)
        {
            if (talker.NoblessType == NoblessType.ACTIVE)
            {
                await MySelf.ShowPage(talker, "olympiad_operator001.htm");
            }
            else
            {
                await MySelf.ShowPage(talker, "olympiad_operator002.htm");
            }
        }
        else if (ask == -51)
        {
            await MySelf.ShowPage(talker, "olympiad_operator010.htm");
        }
        else if (ask == -52)
        {
            switch (reply)
            {
                case 0:
                    await MySelf.ShowPage(talker, "olympiad_operator001.htm");
                    break;
                case 1:
                    if (Gg.GetDateTime(0, 3) >= 23 && Gg.GetDateTime(0, 4) >= 50)
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator010k.htm");
                    }
                    else
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator010a.htm");
                    }
                    break;
                case 2:
                    if (Gg.GetDateTime(0, 3) >= 23 && Gg.GetDateTime(0, 4) >= 50)
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator010k.htm");
                    }
                    else
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator010b.htm");
                    }
                    break;
                case 3:
                    MySelf.FHTML_SetFileName(ref fhtml0, "olympiad_operator010f.htm");
                    if (MySelf.GetOlympiadWaitingCount() < 100)
                    {
                        MySelf.FHTML_SetStr(ref fhtml0, "WaitingCount", MySelf.MakeFString(1000504, "100", "", "", "", ""));
                    }
                    else
                    {
                        MySelf.FHTML_SetStr(ref fhtml0, "WaitingCount", MySelf.MakeFString(1000505, "100", "", "", "", ""));
                    }
                    if (MySelf.GetClassFreeOlympiadWaitingCount() < 100)
                    {
                        MySelf.FHTML_SetStr(ref fhtml0, "WaitingCount", MySelf.MakeFString(1000504, "100", "", "", "", ""));
                    }
                    else
                    {
                        MySelf.FHTML_SetStr(ref fhtml0, "WaitingCount", MySelf.MakeFString(1000505, "100", "", "", "", ""));
                    }
                    await MySelf.ShowFHTML(talker, fhtml0);
                    break;
                case 4:
                    await MySelf.ShowPage(talker, "olympiad_operator010g.htm");
                    break;
                case 5:
                    MySelf.FHTML_SetFileName(ref fhtml0, "olympiad_operator010h.htm");
                    MySelf.FHTML_SetInt(ref fhtml0, "WaitingCount", MySelf.GetOlympiadPoint(talker));
                    await MySelf.ShowFHTML(talker, fhtml0);
                    break;
                default:
                    break;
            }

        }
        else if (ask == -53)
        {
            if (reply == 0)
            {
                await MySelf.ShowPage(talker, "olympiad_operator001.htm");
            }
            else if (reply == 1)
            {
                if (MySelf.IsMainClass(talker) == 1)
                {
                    if (MySelf.IsInCategory(8, talker.Occupation))
                    {
                        if (MySelf.GetOlympiadPoint(talker) > 0)
                        {
                            MySelf.AddClassFreeOlympiad(talker);
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "olympiad_operator010i.htm");
                        }
                    }
                    else
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator010j.htm");
                    }
                }
                else
                {
                    await MySelf.ShowPage(talker, "olympiad_operator010c.htm");
                }
            }
        }
        else if (ask == -54)
        {
            if (reply == 0)
            {
                await MySelf.ShowPage(talker, "olympiad_operator001.htm");
            }
            else if (reply == 1)
            {
                if (MySelf.IsMainClass(talker) == 1)
                {
                    if (MySelf.IsInCategory(8, talker.Occupation))
                    {
                        if (MySelf.GetOlympiadPoint(talker) > 0)
                        {
                            MySelf.AddOlympiad(talker);
                        }
                        else
                        {
                            await MySelf.ShowPage(talker, "olympiad_operator010i.htm");
                        }
                    }
                    else
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator010j.htm");
                    }
                }
                else
                {
                    await MySelf.ShowPage(talker, "olympiad_operator010c.htm");
                }
            }
        }
        else if (ask == -55)
        {
            await MySelf.ShowPage(talker, "olympiad_operator030.htm");
        }
        else if (ask == -56)
        {
        }
        else if (ask == -57)
        {
        }
        else if (ask == -58)
        {
            MySelf.RemoveOlympiad(talker);
        }
        else if (ask == -59)
        {
            MySelf.FHTML_SetFileName(ref fhtml0, "olympiad_operator020.htm");
            for (var i0 = 1; i0 <= 22; i0 = (i0 + 1))
            {
                var s0 = "";
                var s1 = "";
                MySelf.FHTML_SetInt(ref fhtml0, "FI" + i0, i0);
                if (MySelf.GetStatusForOlympiadField(i0) == 0)
                {
                    MySelf.FHTML_SetStr(ref fhtml0, "Status" + i0, "&$906;");
                }
                else
                {
                    s0 = "&$829;" + "&nbsp;&nbsp;&nbsp;" + MySelf.GetPlayer1ForOlympiadField(i0) + "&nbsp; : &nbsp;" + MySelf.GetPlayer2ForOlympiadField(i0);
                    MySelf.FHTML_SetStr(ref fhtml0, "Status" + i0, s0);
                }
            }
            await MySelf.ShowFHTML(talker, fhtml0);
        }
        else if (ask == -60)
        {
            if (reply == 0)
            {
                if (talker.NoblessType == NoblessType.ACTIVE)
                {
                    await MySelf.ShowPage(talker, "olympiad_operator001.htm");
                }
                else
                {
                    await MySelf.ShowPage(talker, "olympiad_operator002.htm");
                }
            }
        }
        else if (ask == -61)
        {
            await MySelf.ShowPage(talker, "olympiad_operator020.htm");
        }
        else if (ask == -70)
        {
            if (reply == 0)
            {
                await MySelf.ShowPage(talker, "olympiad_operator001.htm");
            }
            else if (reply == 1)
            {
                if (MySelf.GetPreviousOlympiadPoint(talker) == 0)
                {
                    await MySelf.ShowPage(talker, "olympiad_operator031a.htm");
                }
                else if (MySelf.GetPreviousOlympiadPoint(talker) < 50)
                {
                    if (talker.HeroType == HeroType.WAITING || talker.HeroType == HeroType.ACTIVE)
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator031.htm");
                    }
                    else
                    {
                        await MySelf.ShowPage(talker, "olympiad_operator031a.htm");
                    }
                }
                else
                {
                    await MySelf.ShowPage(talker, "olympiad_operator031.htm");
                }
            }
            else if (reply == 513)
            {
                await MySelf.ShowMultiSell(reply, talker);
            }
        }
        else if (ask == -71)
        {
            if (reply == 0)
            {
                await MySelf.ShowPage(talker, "olympiad_operator030.htm");
            }
            else if (reply == 1)
            {
                MySelf.DeletePreviousOlympiadPoint(talker, MySelf.GetPreviousOlympiadPoint(talker));
            }
        }
        else if (ask == -80)
        {
        }
        else if (ask == -110)
        {
            MySelf.FHTML_SetFileName(ref fhtml0, "olympiad_operator_rank_class.htm");
            for (var i0 = 1; i0 <= 15; i0 = (i0 + 1))
            {
                if (MySelf.GetRankByOlympiadRankOrder(reply, i0) == 0)
                {
                }
                else
                {
                    MySelf.FHTML_SetInt(ref fhtml0, "Rank" + i0, MySelf.GetRankByOlympiadRankOrder(reply, i0));
                    MySelf.FHTML_SetStr(ref fhtml0, "Name" + i0, MySelf.GetNameByOlympiadRankOrder(reply, i0));
                }
                
            }
            await MySelf.ShowFHTML(talker, fhtml0);

        }
        else if (ask == -130)
        {
            MySelf.ObserveOlympiad(talker, reply);
        }
        else
        {
            await base.MenuSelected(talker, ask, reply, fhtml0);
        }
    }

    public virtual async Task DeletePreviousOlympiadPointReturned(Talker talker, int ask, int reply, int i0, int i1)
    {
        i1 = 0;
        if (reply != 0)
        {
            if (talker.HeroType == HeroType.WAITING || talker.HeroType == HeroType.ACTIVE)
            {
                i1 = 300;
            }
            if (ask > 1000)
            {
                i0 = ((1000 + i1) * 1000);
            }
            else if (ask < 50)
            {
                i0 = (i1 * 1000);
            }
            else
            {
                i0 = ((ask + i1) * 1000);
            }
            await MySelf.AddLogEx(1, talker, ask, i0);
            await MySelf.GiveItem1(talker, "nobless_gate_pass", i0);
        }
    }


}