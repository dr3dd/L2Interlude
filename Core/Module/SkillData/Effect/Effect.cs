using System.Threading;
using System.Threading.Tasks;

namespace Core.Module.SkillData.Effect
{
    public abstract class Effect
    {
        private CancellationTokenSource _cts;

        protected Effect()
        {
            _cts = new CancellationTokenSource();
        }
        public abstract void Calc(params int[] param);

        public async Task StartEffectTaskAsync(int duration)
        {
            
        }
    }
}