using Core.Module.SkillData.Effects;

namespace Core.Module.Player
{
    public readonly struct EffectDuration
    {
        public Effect Effect { get; }
        public int Duration { get; }
        public long PeriodStartTime { get; }

        public EffectDuration(Effect effect, int duration, long periodStartTime)
        {
            Effect = effect;
            Duration = duration;
            PeriodStartTime = periodStartTime;
        }
    }
}