using Core.Module.NpcAi.Models;
using System.Collections.Generic;

namespace Core.Module.NpcAi.Ai.NpcMerchant;

public class ScrollSellerLynn : Merchant
{
    public override IList<BuySellList> SellList0 => new List<BuySellList>
    {
        new("sb_adv_defence_power1", 15, 0.000000, 0),
        new("sb_advanced_attack_power1", 15, 0.000000, 0),
        new("sb_might1", 15, 0.000000, 0),
        new("sb_battle_heal1", 15, 0.000000, 0),
        new("sb_vampiric_touch1", 15, 0.000000, 0),
        new("sb_ice_bolt1", 15, 0.000000, 0),
        new("sb_heal1", 15, 0.000000, 0),
        new("sb_group_heal1", 15, 0.000000, 0),
        new("sb_shield1", 15, 0.000000, 0),
        new("sb_breeze1", 15, 0.000000, 0),
        new("sb_wind_walk1", 15, 0.000000, 0),
        new("sb_curse:weakness", 15, 0.000000, 0),
        new("sb_curse:poison1", 15, 0.000000, 0),
        new("sb_cure_poison1", 15, 0.000000, 0),
        new("sb_flame_strike1", 15, 0.000000, 0),
        new("sb_drain_energy1", 15, 0.000000, 0),
        new("sb_elemental_heal1", 15, 0.000000, 0),
        new("sb_disrupt_undead1", 15, 0.000000, 0),
        new("sb_resurrection1", 15, 0.000000, 0),
        new("sb_blaze1", 15, 0.000000, 0),
        new("sb_summon_shadow1", 15, 0.000000, 0),
        new("sb_summon_silhouette1", 15, 0.000000, 0),
        new("sb_summon_unicorn_boxer1", 15, 0.000000, 0),
        new("sb_summon_blackcat1", 15, 0.000000, 0),
        new("sb_servitor_heal1", 15, 0.000000, 0),
        new("sb_aqua_swirl1", 15, 0.000000, 0),
        new("sb_arcane_acumen1", 15, 0.000000, 0),
        new("sb_energy_bolt1", 15, 0.000000, 0),
        new("sb_aura_burn1", 15, 0.000000, 0),
        new("sb_charm11", 15, 0.000000, 0),
        new("sb_concentration1", 15, 0.000000, 0),
        new("sb_water_breathing", 15, 0.000000, 0),
        new("sb_twister1", 15, 0.000000, 0),
        new("sb_poison1", 15, 0.000000, 0),
        new("sb_poison_recovery1", 15, 0.000000, 0)
    };
}