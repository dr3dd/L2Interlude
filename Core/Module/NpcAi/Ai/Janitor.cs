using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class Janitor : Citizen
{
    public override string FnHi { get; set; } = "defaultAgitInfo.htm";
    public override string FnFeudInfo { get; set; } = "defaultAgitInfo.htm";
    public override string FnNoFeudInfo { get; set; } = "noAgitInfo.htm";
    public virtual string FnOwner { get; set; } = "AgitJanitorHi.htm";
    public virtual string FnWyvernOwner { get; set; } = "WyvernAgitJanitorHi.htm";
    public virtual string FnNoAuthority { get; set; } = "noAuthority.htm";
    public virtual string FnDoor { get; set; } = "AgitDoor.htm";
    public virtual string FnAfterDoorOpen { get; set; } = "AgitJanitorAfterDoorOpen.htm";
    public virtual string FnAfterDoorClose { get; set; } = "AgitJanitorAfterDoorClose.htm";
    public virtual string FnMyLord { get; set; } = "wkeeper001_temp.htm";
    public virtual string FnNotMyLord { get; set; } = "wkeeper002_Agit.htm";
    public virtual string FnStriderHelp { get; set; } = "wkeeper003_temp.htm";
    public virtual string FnAfterRide { get; set; } = "wkeeper004.htm";
    public virtual string FnStriderNotReady { get; set; } = "wkeeper005.htm";
    public virtual string FnNotEnoughFee { get; set; } = "wkeeper006.htm";
    public virtual int RideWyvernLevel { get; set; } = 55;
    public virtual int RideWyvernFee { get; set; } = 25;
    public virtual int IsWyvern { get; set; } = 0;
    public virtual int AinX1 { get; set; } = 0;
    public virtual int AinY1 { get; set; } = 0;
    public virtual int AinZ1 { get; set; } = 0;
    public virtual int AinX2 { get; set; } = 0;
    public virtual int AinY2 { get; set; } = 0;
    public virtual int AinZ2 { get; set; } = 0;
    public virtual int AinX3 { get; set; } = 0;
    public virtual int AinY3 { get; set; } = 0;
    public virtual int AinZ3 { get; set; } = 0;

    public override async Task Talked(Talker talker)
    {
        var fhtml0 = ""; 
        if (MySelf.IsMyLord(talker) || (MySelf.HavePledgePower(talker) && MySelf.Castle_GetPledgeId() == talker.PledgeId && talker.PledgeId != 0))
        {
            if (IsWyvern == 1)
            {
                MySelf.FHTML_SetFileName(ref fhtml0, FnWyvernOwner);
            }
            else
            {
                MySelf.FHTML_SetFileName(ref fhtml0, FnOwner);
            }
            MySelf.FHTML_SetStr(ref fhtml0, "my_pledge_name", MySelf.Castle_GetPledgeName());
            await MySelf.ShowFHTML(talker, fhtml0);
        }
        else if (MySelf.Sm.ResidenceId > 0)
        {
            if (MySelf.Castle_GetPledgeId() > 0)
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
        }
        await MySelf.ShowFHTML(talker, fhtml0);
    }
}