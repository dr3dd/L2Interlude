using System.Collections.Concurrent;
using System.Linq;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Effects
{
    public class EffectIcon
    {
        public static MagicEffectIcons UpdateEffectIcons(ConcurrentDictionary<string, EffectDuration> effectDurations)
        {
            MagicEffectIcons mi = new MagicEffectIcons();
            foreach (var effect in effectDurations.Values.Where( e => e.Effect.SkillDataModel.OperateType == OperateType.A2))
            {
                var skillDataModel = effect.Effect.SkillDataModel;
                mi.AddEffect(skillDataModel.SkillId, skillDataModel.Level, effect.Duration, effect.PeriodStartTime, skillDataModel.IsDeBuff);
            }
            return mi;
        }
    }
}