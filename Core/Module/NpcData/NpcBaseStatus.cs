using System;
using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;

namespace Core.Module.NpcData;

public class NpcBaseStatus : ICharacterBaseStatus
{
    private readonly NpcInstance _npcInstance;
        
    public NpcBaseStatus(NpcInstance npcInstance)
    {
        _npcInstance = npcInstance;
    }

    private NpcTemplateInit GetTemplate() => _npcInstance.GetTemplate();
    private NpcStat GetStat() => GetTemplate().GetStat();
        
    /// <summary>
    /// MAX HP  = baseHp * mod_con * mod_per + mod_diff
    /// Con Mod = Con Bonus + 100 / 100f
    /// </summary>
    /// <returns></returns>
    public int GetMaxHp()
    {
        var baseHp = GetStat().OrgHp;
        var conBonus = GetStat().ConBonus;
        var modCon = (conBonus + 100) / 100f;
        var result = baseHp * modCon;
        var effects = GetPlayerEffects();
        result = CalculateStats.CalculateMaxHp(effects, result);
        return (int) Math.Round(result);
    }

    /// <summary>
    /// maxMp = baseMp * mod_men * mod_per + mod_diff
    /// Men Mod = Men Bonus + 100 / 100f
    /// </summary>
    /// <returns></returns>
    public int GetMaxMp()
    {
        var baseMp = GetStat().OrgMp;
        var menBonus = GetStat().MenBonus;
        var menCon = (menBonus + 100) / 100f;
        var result = baseMp * menCon;
        var effects = GetPlayerEffects();
        result = CalculateStats.CalculateMaxHp(effects, result);
        return (int) Math.Round(result);
    }

    private IEnumerable<EffectDuration> GetPlayerEffects()
    {
        return _npcInstance.CharacterEffect().GetEffects().Values;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetHpRegenRate()
    {
        var baseHpRegen = GetStat().OrgHpRegen;
        var conBonus = GetStat().ConBonus;
        var modCon = (conBonus + 100) / 100f;
        var result = baseHpRegen * modCon;
        return (float) Math.Round(result, 2);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetMpRegenRate()
    {
        var baseMpRegen = GetStat().OrgMpRegen;
        var menBonus = GetStat().MenBonus;
        var modMen = (menBonus + 100) / 100f;
        var result = baseMpRegen * modMen;
        return (float) Math.Round(result, 2);
    }
}