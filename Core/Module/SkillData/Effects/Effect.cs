using System.Threading;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Effects;
using Core.Module.Player;
using Core.Module.SkillData.Helper;
using Core.NetworkPacket.ServerPacket;
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
        public bool IsModPer { get; protected set; }

        public abstract Task Process(PlayerInstance playerInstance, Character targetInstance);
        
        protected async Task StartEffectTask(int duration, Character targetInstance)
        {
            await StartNewEffect(duration, targetInstance);
            LoggerManager.Info($"The effect {SkillDataModel.SkillName} has been started");
            await CharacterEffect(targetInstance);
            await SendEffectMessage(targetInstance, SystemMessageId.YouFeelS1Effect);
        }

        private async Task SendEffectMessage(Character targetInstance, SystemMessageId messageId)
        {
            if (targetInstance is PlayerInstance playerInstance)
            {
                var sm = new SystemMessage(messageId);
                sm.AddSkillName(SkillDataModel.SkillId, SkillDataModel.Level);
                await playerInstance.SendPacketAsync(sm);
            }
        }

        private async Task CharacterEffect(Character targetInstance)
        {
            targetInstance.CharacterEffect().AddEffect(this, Duration, PeriodStartTime);
            await UpdateEffectIcons(targetInstance);
        }

        private async Task UpdateEffectIcons(Character targetInstance)
        {
            if (targetInstance is PlayerInstance playerInstance)
            {
                var mi = EffectIcon.UpdateEffectIcons(targetInstance.CharacterEffect().GetEffects());
                await playerInstance.SendPacketAsync(mi);
                await playerInstance.SendUserInfoAsync();
            }
        }

        private async Task StartNewEffect(int duration, Character targetInstance)
        {
            await StopEffectTask(targetInstance);
            PeriodStartTime = DateTimeHelper.CurrentUnixTimeMillis();
            Duration = duration;
            _cts = new CancellationTokenSource();
            _currentTask = TaskManagerScheduler.ScheduleAtFixed(async () => { await StopEffectTask(targetInstance); }, duration,
                _cts.Token);
        }

        private async Task StopEffectTask(Character targetInstance)
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
            targetInstance.CharacterEffect().RemoveEffect(this);
            await UpdateEffectIcons(targetInstance);
            LoggerManager.Info($"The effect {SkillDataModel.SkillName} has been disappeared");
            await SendEffectMessage(targetInstance, SystemMessageId.S1Disappeared);
        }

        /// <summary>
        /// If Character Can Use Skill
        /// </summary>
        /// <param name="playerInstance"></param>
        /// <param name="targetInstance"></param>
        /// <returns></returns>
        protected EffectResult CanPlayerUseSkill(PlayerInstance playerInstance, Character targetInstance)
        {
            return CheckUseSkillHelper.CanPlayerUseSkill(SkillDataModel, playerInstance, targetInstance);
        }
        
        protected async Task SendStatusUpdate(Character targetInstance)
        {
            if (targetInstance is PlayerInstance playerInstance)
            {
                await playerInstance.SendPacketAsync(new StatusUpdate(playerInstance));
            }
        }
    }
}