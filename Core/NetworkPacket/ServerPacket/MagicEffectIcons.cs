using System.Collections.Generic;
using Helpers;

namespace Core.NetworkPacket.ServerPacket
{
    public class MagicEffectIcons : Network.ServerPacket
    {
        private readonly IList<Effect> _effects;
        private readonly IList<Effect> _deBuffs;
        
        public MagicEffectIcons()
        {
            _effects = new List<Effect>();
            _deBuffs = new List<Effect>();
        }
        
        private class Effect
        {
            public int SkillId { get; }
            public int Level { get; }
            public  int Duration { get; }
            public long PeriodStartTime { get; }
		
            public Effect(int pSkillId, int pLevel, int pDuration, long periodStartTime)
            {
                SkillId = pSkillId;
                Level = pLevel;
                Duration = pDuration;
                PeriodStartTime = periodStartTime;
            }
        }
        
        public void AddEffect(int skillId, int level, int duration, long periodStartTime, bool deBuff)
        {
            if (deBuff)
            {
                _deBuffs.Add(new Effect(skillId, level, duration, periodStartTime));
            }
            else
            {
                _effects.Add(new Effect(skillId, level, duration, periodStartTime));
            }
        }
        
        private int GetDelay(int duration, long periodStartTime)
        {
            return (int) (duration - (DateTimeHelper.GetCurrentUnixTimeMillis() - periodStartTime));
        }
        
        public override void Write()
        {
            WriteByte(0x7f);
		
            WriteShort(_effects.Count + _deBuffs.Count);
		
            foreach (Effect temp in _effects)
            {
                WriteInt(temp.SkillId);
                WriteShort(temp.Level);
			
                if (temp.Duration == -1)
                {
                    WriteInt(-1);
                }
                else
                {
                    //WriteInt(temp.Duration / 1000);
                    WriteInt(GetDelay(temp.Duration, temp.PeriodStartTime) / 1000);
                    //WriteInt(30000 / 1000);
                }
            }
		
            foreach (Effect temp in _deBuffs)
            {
                WriteInt(temp.SkillId);
                WriteShort(temp.Level);
			
                if (temp.Duration == -1)
                {
                    WriteInt(-1);
                }
                else
                {
                    WriteInt(temp.Duration / 1000);
                }
            }
        }
    }
}