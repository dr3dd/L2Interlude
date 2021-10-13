using System.Collections.Concurrent;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Effects
{
    public class EffectIcon
    {
        public static MagicEffectIcons UpdateEffectIcons(ConcurrentDictionary<string, EffectDuration> effectDurations)
        {
            MagicEffectIcons mi = new MagicEffectIcons();
            foreach (var effect in effectDurations)
            {
                var skillDataModel = effect.Value.Effect.SkillDataModel;
                mi.AddEffect(skillDataModel.SkillId, skillDataModel.Level, effect.Value.Duration, effect.Value.PeriodStartTime, skillDataModel.IsDeBuff);
            }
            return mi;
        }
    }
}