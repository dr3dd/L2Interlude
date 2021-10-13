using System.Threading;
using System.Threading.Tasks;

namespace Core.Module.SkillData.Effects
{
    internal struct EffectTask
    {
        public Task CurrentTask { get; set; }
        public CancellationTokenSource CurrentTaskSource { get; set; }
    }
}