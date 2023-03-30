using Core.GeoEngine;
using Core.Module.CharacterData;
using Core.Module.Player;
using Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.SkillData.Helper
{
    public static class CheckUseSkillHelper
    {
        public static EffectResult CanPlayerUseSkill(SkillDataModel skill, Character currentInstance, Character targetInstance)
        {
            var effectiveRange = skill.EffectiveRange;
            var effectResult = new EffectResult
            {
                IsNotValid = false
            };
            if (!CheckIfInRange(effectiveRange, currentInstance, targetInstance))
            {
                effectResult.IsNotValid = true;
                effectResult.SystemMessageId = SystemMessageId.TargetTooFar;
            }
            if (!CanSeeTarget(currentInstance, targetInstance)) {
                effectResult.IsNotValid = true;
                effectResult.SystemMessageId = SystemMessageId.CantSeeTarget;
            }
            return effectResult;
        }

        private static bool CheckIfInRange(int effectiveRange, Character currentInstance, Character targetInstance)
        {
            return CalculateRange.CheckIfInRange(effectiveRange, currentInstance.GetX(), currentInstance.GetY(),
                currentInstance.GetZ(), 33, targetInstance.GetX(), targetInstance.GetY(), targetInstance.GetZ(), 33,
                true);
        }

        private static bool CanSeeTarget(Character currentInstance, Character targetInstance)
        {
            return currentInstance.ServiceProvider.GetRequiredService<GeoEngineInit>().CanSee(currentInstance.GetX(),
                currentInstance.GetY(), currentInstance.GetZ(), 33,
                targetInstance.GetX(), targetInstance.GetY(), targetInstance.GetZ(), 33
            );
        }
    }
}