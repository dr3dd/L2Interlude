using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;

namespace Core.Module.NpcAi.Ai;

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

	public override async Task Talked(Talker talker)
	{
		MySelf.DeleteRadar(talker, -71073, 258711, -3099, 2);
		MySelf.DeleteRadar(talker, -84081, 243277, -3723, 2);
		MySelf.DeleteRadar(talker, 45492, 48359, -3060, 2);
		MySelf.DeleteRadar(talker, 12111, 16686, -4582, 2);
		MySelf.DeleteRadar(talker, -45042, -113598, -192, 2);
		MySelf.DeleteRadar(talker, 115632, -177996, -905, 2);
		await MySelf.ShowPage(talker, FnHi);
	}

	public override async Task TeleportRequested(Talker talker)
	{
		if(talker.Level > 20)
		{
			await MySelf.ShowPage(talker, FnHighLevel);
			return;
		}
		await MySelf.Teleport(talker, NewbieTokenTeleports, ShopName, "", "", "", 8542, "Newbie Travel Token");
	}

	public override async Task MenuSelected(Talker talker, int ask, int reply)
	{
		if (ask == -7 && reply == 1)
		{
			if(talker.Race != MySelf.Sm.Race)
			{
				await MySelf.ShowPage(talker, FnRaceMisMatch);
			}
			else if(talker.Level > 20 || MySelf.IsInCategory(5, talker.Occupation))
			{
				await MySelf.ShowPage(talker, FnHighLevel);
			}
			else if(MySelf.IsInCategory(0, talker.Occupation))
			{
				if(talker.Level <= 5)
				{
					await MySelf.ShowPage(talker, FnGuideF05);
				}
				else if(talker.Level <= 10)
				{
					await MySelf.ShowPage(talker, FnGuideF10);
				}
				else if(talker.Level <= 15)
				{
					await MySelf.ShowPage(talker, FnGuideF15);
				}
				else
				{
					await MySelf.ShowPage(talker, FnGuideF20);
				}
			}
			else if(talker.Level <= 7)
			{
				await MySelf.ShowPage(talker, FnGuideM07);
			}
			else if(talker.Level <= 14)
			{
				await MySelf.ShowPage(talker, FnGuideM14);
			}
			else
			{
				await MySelf.ShowPage(talker, FnGuideM20);
			}
		}
		if (ask == -7 && reply == 2)
		{
			if(MySelf.IsNewbie(talker) && MySelf.IsInCategory(7, talker.Occupation) == false)
			{
				if(talker.Level < 8)
				{
					await MySelf.ShowPage(talker, "guide_for_newbie002.htm");
				}
				else if (MySelf.IsInCategory(0, talker.Occupation))
				{
					if(talker.Level >= 8 && talker.Level <= 24)
					{
						MySelf.AddUseSkillDesire(talker, 1106433, 1, 0, 1000000);
					}
					if(talker.Level >= 11 && talker.Level <= 23)
					{
						MySelf.AddUseSkillDesire(talker, 1106689, 1, 0, 1000000);
					}
					if(talker.Level >= 12 && talker.Level <= 22)
					{
						MySelf.AddUseSkillDesire(talker, 1106945, 1, 0, 1000000);
					}
					if(talker.Level >= 13 && talker.Level <= 21)
					{
						MySelf.AddUseSkillDesire(talker, 1107201, 1, 0, 1000000);
					}
					if(talker.Level >= 14 && talker.Level <= 20)
					{
						MySelf.AddUseSkillDesire(talker, 1107457, 1, 0, 1000000);
					}
					if(talker.Level >= 15 && talker.Level <= 19)
					{
						MySelf.AddUseSkillDesire(talker, 1107713, 1, 0, 1000000);
					}
					if(talker.Level >= 16 && talker.Level <= 19)
					{
						MySelf.AddUseSkillDesire(talker, 1110529, 1, 0, 1000000);
					}
				}
				else if(talker.Level >= 8 && talker.Level <= 24)
				{
					MySelf.AddUseSkillDesire(talker, 1106433, 1, 0, 1000000);
				}
				if(talker.Level >= 11 && talker.Level <= 23)
				{
					MySelf.AddUseSkillDesire(talker, 1106689, 1, 0, 1000000);
				}
				if(talker.Level >= 12 && talker.Level <= 22)
				{
					MySelf.AddUseSkillDesire(talker, 1107969, 1, 0, 1000000);
				}
				if(talker.Level >= 13 && talker.Level <= 21)
				{
					MySelf.AddUseSkillDesire(talker, 1108225, 1, 0, 1000000);
				}
				if(talker.Level >= 14 && talker.Level <= 20)
				{
					MySelf.AddUseSkillDesire(talker, 1108481, 1, 0, 1000000);
				}
				if(talker.Level >= 15 && talker.Level <= 19)
				{
					MySelf.AddUseSkillDesire(talker, 1108737, 1, 0, 1000000);
				}
				if(talker.Level >= 16 && talker.Level <= 19)
				{
					MySelf.AddUseSkillDesire(talker, 1110529, 1, 0, 1000000);
				}
			}
			else
			{
				await MySelf.ShowPage(talker, "guide_for_newbie003.htm");
			}
		}
		if (ask == -7 && reply == 3)
		{
			if(MySelf.IsNewbie(talker) && talker.Level < 40)
			{
				MySelf.AddUseSkillDesire(talker, 1326593, 1, 0, 1000000);
			}
			else
			{
				await MySelf.ShowPage(talker, FnHighLevel);
			}
		}
	}
}