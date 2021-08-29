namespace Core.Module.SkillData.Effect
{
    public class PSpeed : Effect
    {
        private int _effectSpeed;
        public override void Calc(params int[] param)
        {
            _effectSpeed = param[0] + param[1];
        }

        public int GetEffectSpeed()
        {
            return _effectSpeed;
        }
    }
}