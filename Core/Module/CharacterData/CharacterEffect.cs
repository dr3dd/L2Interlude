using System;
using System.Collections.Concurrent;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.Module.SkillData.Effects;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.CharacterData;

public class CharacterEffect
{
    private readonly Character _character;
    private readonly ConcurrentDictionary<string, EffectDuration> _currentEffects;
    private readonly SkillDataInit _dataInit;
    private readonly EffectInit _effectInit;

    public CharacterEffect(Character character)
    {
        _character = character;
        _currentEffects = new ConcurrentDictionary<string, EffectDuration>();
        _dataInit = character.ServiceProvider.GetRequiredService<SkillDataInit>();
        _effectInit = character.ServiceProvider.GetRequiredService<EffectInit>();
    }

    public void AddEffect(Effect effect, int duration, long periodStartTime)
    {
        if (effect.SkillDataModel != null)
        {
            EffectDuration effectDuration = new EffectDuration(effect, duration, periodStartTime);
            _currentEffects.TryAdd(effect.SkillDataModel.SkillName, effectDuration);
        }
    }

    public void RemoveEffect(Effect effect)
    {
        _currentEffects.TryRemove(effect.SkillDataModel.SkillName, out _);
    }

    /// <summary>
    /// Remove Effects
    /// </summary>
    public void RemoveEffects()
    {
        _currentEffects.Clear();
    }

    public ConcurrentDictionary<string, EffectDuration> GetEffects()
    {
        return _currentEffects;
    }
}