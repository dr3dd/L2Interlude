using Core.Enums;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class Doorkeeper : Citizen
{
    public virtual string DoorName1 { get; set; } = "partisan001";
    public virtual string DoorName2 { get; set; } = "partisan002";
    public override string FnHi { get; set; } = "gludio_outter_doorman001.htm";
    public string FnNotMyLord { get; set; } = "gludio_outter_doorman002.htm";
    public string FnUnderSiege { get; set; } = "gludio_outter_doorman003.htm";
    public virtual int PosX01 { get; set; } = 1;
    public virtual int PosY01 { get; set; } = 1;
    public virtual int PosZ01 { get; set; } = 1;
    public virtual int PosX02 { get; set; } = 1;
    public virtual int PosY02 { get; set; } = 1;
    public virtual int PosZ02 { get; set; } = 1;

    public override async Task Talked(Talker talker)
    {
        if (MySelf.IsMyLord(talker) || (MySelf.HavePledgePower(talker, PledgePower.OPEN_CASTLE_DOOR) && MySelf.CastleGetPledgeId() == talker.PledgeId && talker.PledgeId != 0))
        {
            if (MySelf.CastleIsUnderSiege())
            {
                if (MySelf.IsMyLord(talker) || MySelf.CastleGetPledgeState(talker) == 2 || (MySelf.CastleGetPledgeId() == talker.PledgeId && talker.PledgeId != 0))
                {
                    await MySelf.ShowPage(talker, FnHi);
                }
                else
                {
                    await MySelf.ShowPage(talker, FnUnderSiege);
                }
            }
            else
            {
                await MySelf.ShowPage(talker, FnHi);
            }
        }
        else
        {
            await MySelf.ShowPage(talker, FnNotMyLord);
        }
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == -201)
        {
            if (MySelf.IsMyLord(talker) || (MySelf.HavePledgePower(talker, PledgePower.OPEN_CASTLE_DOOR) && MySelf.CastleGetPledgeId() == talker.PledgeId && talker.PledgeId != 0))
            {
                if (MySelf.CastleIsUnderSiege())
                {
                    await MySelf.ShowPage(talker, FnUnderSiege);
                }
                else
                {
                    switch (reply)
                    {
                        case 1:
                            await MySelf.CastleGateOpenClose2(DoorName1, 0);
                            await MySelf.CastleGateOpenClose2(DoorName2, 0);
                            break;
                        case 2:
                            await MySelf.CastleGateOpenClose2(DoorName1, 1);
                            await MySelf.CastleGateOpenClose2(DoorName2, 1);
                            break;
                        default:
                            break;
                        }
                }
            }
            else
            {
                await MySelf.ShowPage(talker, FnNotMyLord);
            }
        }
        if (ask == -202)
        {
            if (MySelf.IsMyLord(talker) || MySelf.CastleGetPledgeState(talker) == 2 || (MySelf.HavePledgePower(talker, PledgePower.OPEN_CASTLE_DOOR) && MySelf.CastleGetPledgeId() == talker.PledgeId && talker.PledgeId != 0))
            {
                switch (reply)
                {
                    case 1:
                        await MySelf.InstantTeleport(talker, PosX01, PosY01, PosZ01);
                        break;
                    case 2:
                        await MySelf.InstantTeleport(talker, PosX02, PosY02, PosZ02);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                await MySelf.ShowPage(talker, FnNotMyLord);
            }
        }
    }
}