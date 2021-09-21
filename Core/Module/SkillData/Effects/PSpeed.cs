using System;

namespace Core.Module.SkillData.Effects
{
    public class PSpeed : Effect
    {
        private int _effectSpeed;
        public override void Calc(params int[] param)
        {
            _effectSpeed = param[0] + param[1];
        }

        public override void Process(string[] param)
        {
            _effectSpeed = Convert.ToInt32(param[2]);
        }

        public int GetEffectSpeed()
        {
            return _effectSpeed;
        }
    }
}