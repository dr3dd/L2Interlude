using Core.GeoEngine;
using Core.Module.Player;
using Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.SkillData.Helper
{
    public static class CheckUseSkillHelper
    {
        public static EffectResult CanPlayerUseSkill(SkillDataModel skill, PlayerInstance playerInstance, PlayerInstance targetInstance)
        {
            var effectiveRange = skill.EffectiveRange;
            var effectResult = new EffectResult
            {
                IsNotValid = false
            };
            if (!CheckIfInRange(effectiveRange, playerInstance, targetInstance))
            {
                effectResult.IsNotValid = true;
                effectResult.SystemMessageId = SystemMessageId.TargetTooFar;
            }
            if (!CanSeeTarget(playerInstance, targetInstance)) {
                effectResult.IsNotValid = true;
                effectResult.SystemMessageId = SystemMessageId.CantSeeTarget;
            }
            return effectResult;
        }

        private static bool CheckIfInRange(int effectiveRange, PlayerInstance playerInstance, PlayerInstance targetInstance)
        {
            return CalculateRange.CheckIfInRange(effectiveRange, playerInstance.GetX(), playerInstance.GetY(),
                playerInstance.GetZ(), 33, targetInstance.GetX(), targetInstance.GetY(), targetInstance.GetZ(), 33,
                true);
        }

        private static bool CanSeeTarget(PlayerInstance playerInstance, PlayerInstance targetInstance)
        {
            return playerInstance.ServiceProvider.GetRequiredService<GeoEngineInit>().CanSee(playerInstance.GetX(),
                playerInstance.GetY(), playerInstance.GetZ(), 33,
                targetInstance.GetX(), targetInstance.GetY(), targetInstance.GetZ(), 33
            );
        }
    }
}