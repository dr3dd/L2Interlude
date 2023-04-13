using System;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class GuildMaster : Citizen
{
	public override string FnHi { get; set; } = "gmhi.htm";
	public string FnClassMismatch { get; set; } = "gmhi.htm";
	public string FnClassList1 { get; set; } = "gmhi.htm";
	public string FnClassList2 { get; set; } = "gmhi.htm";
	public string FnClassList3 { get; set; } = "gmhi.htm";
	public string FnClassList4 { get; set; } = "gmhi.htm";
	public string FnClassList5 { get; set; } = "gmhi.htm";
	public string FnYouAreSecondClass { get; set; } = "gmhi.htm";
	public string FnYouAreThirdClass { get; set; } = "gmhi.htm";
	public string FnYouAreFourthClass { get; set; } = "master_lv3_hef005.htm";
	public string FnLowLevelNoProof11 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof12 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof13 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof21 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof22 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof23 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof31 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof32 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof33 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof41 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof42 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof43 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof51 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof52 { get; set; } = "gmhi.htm";
	public string FnLowLevelNoProof53 { get; set; } = "gmhi.htm";
	public string FnLowLevel11 { get; set; } = "gmhi.htm";
	public string FnLowLevel12 { get; set; } = "gmhi.htm";
	public string FnLowLevel13 { get; set; } = "gmhi.htm";
	public string FnLowLevel21 { get; set; } = "gmhi.htm";
	public string FnLowLevel23 { get; set; } = "gmhi.htm";
	public string FnLowLevel31 { get; set; } = "gmhi.htm";
	public string FnLowLevel32 { get; set; } = "gmhi.htm";
	public string FnLowLevel41 { get; set; } = "gmhi.htm";
	public string FnLowLevel42 { get; set; } = "gmhi.htm";
	public string FnLowLevel43 { get; set; } = "gmhi.htm";
	public string FnLowLevel51 { get; set; } = "gmhi.htm";
	public string FnLowLevel52 { get; set; } = "gmhi.htm";
	public string FnLowLevel53 { get; set; } = "gmhi.htm";
	public string FnNoProof11 { get; set; } = "gmhi.htm";
	public string FnNoProof12 { get; set; } = "gmhi.htm";
	public string FnNoProof13 { get; set; } = "gmhi.htm";
	public string FnNoProof21 { get; set; } = "gmhi.htm";
	public string FnNoProof22 { get; set; } = "gmhi.htm";
	public string FnNoProof23 { get; set; } = "gmhi.htm";
	public string FnNoProof31 { get; set; } = "gmhi.htm";
	public string FnNoProof32 { get; set; } = "gmhi.htm";
	public string FnNoProof33 { get; set; } = "gmhi.htm";
	public string FnNoProof41 { get; set; } = "gmhi.htm";
	public string FnNoProof42 { get; set; } = "gmhi.htm";
	public string FnNoProof43 { get; set; } = "gmhi.htm";
	public string FnNoProof51 { get; set; } = "gmhi.htm";
	public string FnNoProof52 { get; set; } = "gmhi.htm";
	public string FnNoProof53 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange12 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange13 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange21 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange22 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange23 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange31 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange32 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange33 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange42 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange43 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange51 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange52 { get; set; } = "gmhi.htm";
	public string FnAfterClassChange53 { get; set; } = "gmhi.htm";
	public string FnYouAreFirstClass { get; set; } = "gmhi.htm";
	public override string FnYouAreChaotic { get; set; } = "wyac.htm";

	public override async Task Talked(Talker talker)
	{
		if (talker.Karma > 0)
		{
			await MySelf.ShowPage(talker, FnYouAreChaotic);
		}
		else
		{
			await MySelf.ShowPage(talker, FnHi);
		}
	}
	
	public async Task CreatePledge(Talker talker, int reply)
	{
		if(reply == 1)
		{
			await MySelf.ShowPage(talker, "pl006.htm");
		}
	}

	public async Task CreateAlliance(Talker talker, int reply)
	{
		if(reply == 1)
		{
			await MySelf.ShowPage(talker, "al006.htm");
		}
	}
	
	public async Task DismissPledge(Talker talker, int reply)
	{
		if(reply == 1)
		{
			await MySelf.ShowPage(talker, "pl009.htm");
		}
	}

	public Task LevelUpPledge(Talker talker)
	{
		throw new NotImplementedException();
	}
	
	public override async Task MenuSelected(Talker talker, int ask, int reply)
	{
		if (ask == -3)
		{
			if (reply == 0)
			{
				if (talker.Level < 10)
				{
					await MySelf.ShowPage(talker, "pl002.htm");
				}
				else if (talker.IsPledgeMaster != 0)
				{
					await MySelf.ShowPage(talker, "pl003.htm");
				}
				else if (talker.PledgeId != 0)
				{
					await MySelf.ShowPage(talker, "pl004.htm");
				}
				else
				{
					await MySelf.ShowPage(talker, "pl005.htm");
				}
			}
			else if (reply == 2)
			{
				if (talker.IsPledgeMaster != 0)
				{
					await MySelf.ShowPage(talker, "pl007.htm");
				}
				else
				{
					await MySelf.ShowPage(talker, "pl008.htm");
				}
			}
			else if (reply == 3)
			{
				if (talker.IsPledgeMaster != 0)
				{
					await MySelf.ShowPage(talker, "pl010.htm");
				}
				else
				{
					await MySelf.ShowPage(talker, "pl011.htm");
				}
			}
			else if (reply == 1)
			{
				if (talker.IsPledgeMaster != 0)
				{
					await MySelf.ShowPage(talker, "pl013.htm");
				}
				else
				{
					await MySelf.ShowPage(talker, "pl014.htm");
				}
			}
		}
		else if (ask == -4)
		{
			if (reply == 0)
			{
				await MySelf.ShowPage(talker, "al005.htm");
			}
		}
		if (ask == -5)
		{
			if (reply == 0)
			{
				if (talker.IsPledgeMaster != 0)
				{
					await MySelf.ShowPage(talker, "pl001a.htm");
				}
			}
		}
	}
}