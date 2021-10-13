using System.Threading;
using System.Threading.Tasks;
using Core.Module.Effects;
using Core.Module.Player;
using Core.TaskManager;
using Helpers;
using L2Logger;

namespace Core.Module.SkillData.Effects
{
    public abstract class Effect
    {
        private Task _currentTask;
        private CancellationTokenSource _cts;
        private long PeriodStartTime { get; set; }
        private int Duration { get; set; }
        public SkillDataModel SkillDataModel { get; protected set; }

        public abstract Task Process(PlayerInstance playerInstance);
        
        protected async Task StartEffectTask(int duration, PlayerInstance playerInstance)
        {
            await StartNewEffect(duration, playerInstance);
            LoggerManager.Info($"The effect {SkillDataModel.SkillName} has been started");
            await PlayerEffect(playerInstance);
        }

        private async Task PlayerEffect(PlayerInstance playerInstance)
        {
            playerInstance.PlayerEffect().AddEffect(this, Duration, PeriodStartTime);
            var mi = EffectIcon.UpdateEffectIcons(playerInstance.PlayerEffect().GetEffects());
            await playerInstance.SendPacketAsync(mi);
            await playerInstance.SendUserInfoAsync();
        }
        
        private async Task StartNewEffect(int duration, PlayerInstance playerInstance)
        {
            await StopEffectTask(playerInstance);
            PeriodStartTime = DateTimeHelper.CurrentUnixTimeMillis();
            Duration = duration;
            _cts = new CancellationTokenSource();
            _currentTask = TaskManagerScheduler.ScheduleAtFixed(async () => { await StopEffectTask(playerInstance); }, duration,
                _cts.Token);
        }

        private async Task StopEffectTask(PlayerInstance playerInstance)
        {
            if (_currentTask is null)
            {
                return;
            }
            if (!_currentTask.IsCanceled)
            {
                _cts.Cancel();
            }
            _currentTask = null;
            playerInstance.PlayerEffect().RemoveEffect(this);
            var mi = EffectIcon.UpdateEffectIcons(playerInstance.PlayerEffect().GetEffects());
            await playerInstance.SendPacketAsync(mi);
            LoggerManager.Info($"The effect {SkillDataModel.SkillName} has been disappeared");
            await playerInstance.SendUserInfoAsync();
        }
    }
}