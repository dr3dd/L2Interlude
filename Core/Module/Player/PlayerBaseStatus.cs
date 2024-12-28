using System;
using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player;

public class PlayerBaseStatus : IPlayerBaseStatus
{
    private readonly PlayerInstance _playerInstance;
    private readonly PcParameterInit _statBonusInit;
    private readonly CharacterMovement _characterMovement;
    private byte _level = 1;
    public float CurrentCp { get; set; }

    public byte Level
    {
        get => _level;
        set
        {
            _level = value;
            _playerInstance.Level = value;
        }
    }
    
    public PlayerBaseStatus(PlayerInstance playerInstance)
    {
        _playerInstance = playerInstance;
        _statBonusInit = playerInstance.ServiceProvider.GetRequiredService<PcParameterInit>();
        _characterMovement = _playerInstance.CharacterMovement();
    }

    /// <summary>
    /// maxСp = maxCp + (maxCp * CON bonus / 100)
    /// </summary>
    /// <returns></returns>
    public int GetMaxCp()
    {
        var cpBegin = _playerInstance.TemplateHandler().GetCpBegin(Level);
        var conStat = _playerInstance.TemplateHandler().GetCon();
        return (int) (cpBegin + (cpBegin * _statBonusInit.GetConBonus(conStat) / 100));
    }

    /// <summary>
    /// MAX HP  = base * mod_con * mod_per + mod_diff
    /// maxHp = maxHp + (maxHp * CON bonus / 100)
    /// </summary>
    /// <returns></returns>
    public int GetMaxHp()
    {
        var hpBegin = _playerInstance.TemplateHandler().GetHpBegin(Level);
        var conStat =  _playerInstance.TemplateHandler().GetCon();
        var conBonus = _statBonusInit.GetConBonus(conStat);
        var modCon = (conBonus + 100) / 100f;
        var result = hpBegin * modCon;
        var effects = GetPlayerEffects();
        result = CalculateStats.CalculateMaxHp(effects, result);
        return (int)Math.Round(result);
    }
        
    /// <summary>
    /// maxMp = maxMp + (maxMp * MEN bonus / 100)
    /// </summary>
    /// <returns></returns>
    public int GetMaxMp()
    {
        var mpBegin = _playerInstance.TemplateHandler().GetMpBegin(Level);
        var menStat =  _playerInstance.TemplateHandler().GetMen();
        return (int) (mpBegin + (mpBegin * _statBonusInit.GetMenBonus(menStat) / 100));
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetHpRegenRate()
    {
        var baseHpRegen = _playerInstance.TemplateHandler().GetBaseHpRegen(Level);
        var conStat = _playerInstance.TemplateHandler().GetCon();
        var modCon = (_statBonusInit.GetConBonus(conStat) + 100) / 100f;
        
        var regenBonus = GetRegenBonus();
        var result = baseHpRegen * modCon * regenBonus;
        return (float) Math.Round(result, 2);
    }

    /// <summary>
    /// HP/MP recovery speed condition bonus
    /// regen_move_mode_bonus_begin
    /// sit = 50%	//The case which is sitting
    /// stand = 10%	//From the case which is
    /// low = 0%	//Low speed the case which moves
    /// high = -30%	//High speed the case which moves
    /// regen_move_mode_bonus_end
    /// </summary>
    /// <returns></returns>
    private double GetRegenBonus()
    {
        var regenBonus = 1.0;
        if (_characterMovement.CharacterMovementStatus().IsSit())
        {
            regenBonus = 1.5; //50%
        }
        if (_characterMovement.CharacterMovementStatus().IsStand())
        {
            regenBonus = 1.1; //10%
        }
        if (_characterMovement.CharacterMovementStatus().IsGroundLow())
        {
            regenBonus = 1.0; //0%
        }
        if (_characterMovement.CharacterMovementStatus().IsGroundHigh())
        {
            regenBonus = 0.7; //-30%
        }
        return regenBonus;
    }

    public float GetMpRegenRate()
    {
        throw new NotImplementedException();
    }

    public int GetMaxLoad()
    {
        var conStat = _playerInstance.TemplateHandler().GetCon();
        if (conStat < 1)
        {
            return 31000;
        }
        if (conStat > 59)
        {
            return 176000;
        }
        var modCon = (_statBonusInit.GetConBonus(conStat) + 100) / 100f * 69000;
        return (int) modCon;
    }

    public int GetCurrentLoad()
    {
        return _playerInstance.PlayerInventory().GetTotalWeight();
    }

    private IEnumerable<EffectDuration> GetPlayerEffects()
    {
        return _playerInstance.CharacterEffect().GetEffects().Values;
    }
}