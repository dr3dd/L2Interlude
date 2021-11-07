using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using L2Logger;

namespace Core.Module.Player
{
    public class PlayerDesireCast
    {
        private readonly PlayerInstance _playerInstance;
        private CancellationTokenSource _cts;
        
        public PlayerDesireCast(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        public async Task DoCastAsync(SkillDataModel skill)
        {
            var target = _playerInstance; //self target
            // Get the Identifier of the skill
            int skillId = skill.SkillId;
            //todo need add calculator
            float coolTime = skill.SkillCoolTime;
            float hitTime = skill.SkillHitTime * 1000;
            float reuseDelay = skill.ReuseDelay * 1000;
            await HandleMagicSkill(skill, hitTime);
            await SendToKnownListAsync(skill, target, hitTime, reuseDelay);
            await _playerInstance.SendPacketAsync(new SetupGauge(SetupGauge.Blue, hitTime));
            // Send a system message to the Player
            await _playerInstance.PlayerMessage().SendMessageToPlayerAsync(skill, skillId);
            await _playerInstance.SendUserInfoAsync();
        }

        private async Task HandleMagicSkill(SkillDataModel skill, float hitTime)
        {
            await Task.Run(() =>
            {
                try
                {
                    _cts = new CancellationTokenSource();
                    var effects = skill.Effects;
                    foreach (var (key, value) in effects)
                    {
                        TaskManagerScheduler.ScheduleAtFixed(async () =>
                        {
                            await value.Process(_playerInstance);
                        }, (int)hitTime, _cts.Token);
                    }
                }
                catch (Exception ex)
                {
                    LoggerManager.Error(ex.Message);
                }
            });
        }

        private async Task SendToKnownListAsync(SkillDataModel skill, PlayerInstance target, float hitTime, float reuseDelay)
        {
            var skillUse = new MagicSkillUse(_playerInstance, target, skill.SkillId, skill.Level, hitTime, reuseDelay);
            await _playerInstance.SendPacketAsync(skillUse);
            await _playerInstance.SendToKnownPlayers(skillUse);
        }
    }
}