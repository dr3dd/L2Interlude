using Helpers;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class TeleporterMultiList : Teleporter
{
    public override int PrimeHours => 0;
    public override int PHfromHour => 20;
    public override int PHtoHour => 8;
    public override int PHfromDay => 1;
    public override int PHtoDay => 7;
    public virtual IList<TeleportList> Position1 => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };
    public virtual IList<TeleportList> PositionPrimeHours => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> Position2 => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public virtual IList<TeleportList> Position3 => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == -8)
        {
            if (reply == 1)
            {/*
                if(PrimeHours == 1)
				{
					i0 = gg.GetDateTime(0, 3);
					i1 = gg.GetDateTime(0, 6);
					if(i1 >= PHfromDay && i1 <= PHtoDay)
					{
						if(i0 >= PHfromHour || i0 <= PHtoHour)
						{
							await MySelf.Teleport(talker, PositionPrimeHours, ShopName, "", "", "", 57, "Adena");
							return;
						}
					}
				}
                */
                await MySelf.Teleport(talker, Position1, ShopName, "", "", "", 57, MySelf.MakeFString(1000308, "", "", "", "", ""));
            }
            else if (reply == 2)
            {
                await MySelf.Teleport(talker, Position2, ShopName, "", "", "", 57, MySelf.MakeFString(1000308, "", "", "", "", ""));
            }
            else if (reply == 3)
            {
                await MySelf.Teleport(talker, Position3, ShopName, "", "", "", 57, MySelf.MakeFString(1000308, "", "", "", "", ""));
            }
        }

        await base.MenuSelected(talker, ask, reply, fhtml0);
    }

    public virtual IList<TeleportList> GetPositionList(int hashCode)
    {
        if (Position1.GetHashCodeByValue() == hashCode)
        {
            return Position1;
        }
        else if (PositionPrimeHours.GetHashCodeByValue() == hashCode)
        {
            return PositionPrimeHours;
        }
        else if (Position2.GetHashCodeByValue() == hashCode)
        {
            return Position2;
        }
        else if (Position3.GetHashCodeByValue() == hashCode)
        {
            return Position3;
        }

        return base.GetPositionList(hashCode);
    }
}