using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.Player;
using Core.TaskManager;
using L2Logger;

namespace Core.Module.SkillData.Effects
{
    public abstract class Effect
    {
        private CancellationTokenSource _cts;
        private Task _currentTask;
        public SkillDataModel SkillDataModel { get; set; }

        protected Effect()
        {
            
        }

        public abstract Task Process(string[] param, SkillDataModel skill, PlayerInstance playerInstance);
        
        public Task StartEffectTask(int duration, PlayerInstance playerInstance)
        {
            _cts = new CancellationTokenSource();
            _currentTask = TaskManagerScheduler.ScheduleAtFixed(() =>
            {
                StopEffectTask(playerInstance);
            }, duration, _cts.Token);
            Console.WriteLine($"The effect {SkillDataModel.SkillName} has been started");
            //LoggerManager.Warn($"The effect {SkillDataModel.SkillName} has been started");
            playerInstance.PlayerEffect().AddEffect(this);
            return _currentTask;
        }

        public Task StopEffectTask(PlayerInstance playerInstance)
        {
            if (_currentTask != null)
            {
                if (!_currentTask.IsCanceled)
                {
                    _cts.Cancel();
                }
                _currentTask = null;
                playerInstance.PlayerEffect().RemoveEffect(this);
                //LoggerManager.Warn($"The effect {SkillDataModel.SkillName} has been disappeared");
                Console.WriteLine($"The effect {SkillDataModel.SkillName} has been disappeared");
            }
            return _currentTask;
        }
    }
}