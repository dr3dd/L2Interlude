using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcCitizen;

public class FisherPerelin : Fisher
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new("green_lure_easy", 20, 0.000000, 0),
        new("violet_lure_easy", 20, 0.000000, 0),
        new("yellow_lure_easy", 20, 0.000000, 0),
        new("green_lure_low", 20, 0.000000, 0),
        new("violet_lure_low", 20, 0.000000, 0),
        new("yellow_lure_low", 20, 0.000000, 0),
        new("green_luminous_lure", 20, 0.000000, 0),
        new("purple_luminous_lure", 20, 0.000000, 0),
        new("yellow_luminous_lure", 20, 0.000000, 0),
        new("prizewinning_fishing_lure", 20, 0.000000, 0),
        new("prizewinning_night_fishing_lure", 20, 0.000000, 0),
        new("prizewinning_novice_fishing_lure", 20, 0.000000, 0),
        new("fp_babyduck_rod", 20, 0.000000, 0),
        new("fp_albatros_rod", 20, 0.000000, 0),
        new("fp_pelican_rod", 20, 0.000000, 0),
        new("fp_kingfisher_rod", 20, 0.000000, 0),
        new("fp_cygnus_pole", 20, 0.000000, 0),
        new("fp_triton_pole", 20, 0.000000, 0),
        new("fishing_manual", 20, 0.000000, 0),
        new("fishermans_potion_green", 20, 0.000000, 0),
        new("fishermans_potion_jade", 20, 0.000000, 0),
        new("fishermans_potion_blue", 20, 0.000000, 0),
        new("fishermans_potion_yellow", 20, 0.000000, 0),
        new("fishermans_potion_orange", 20, 0.000000, 0),
        new("fishermans_potion_purple", 20, 0.000000, 0),
        new("fishermans_potion_red", 20, 0.000000, 0),
        new("fishermans_potion_white", 20, 0.000000, 0),
        new("fishermans_potion_black", 20, 0.000000, 0),
        new("fishing_potion", 20, 0.000000, 0)
    };
}