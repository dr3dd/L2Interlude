using System;
using System.Threading;
using System.Threading.Tasks;
using Core.GeoEngine;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerDesireCast
    {
        private readonly PlayerInstance _playerInstance;
        private CancellationTokenSource _cts;
        private readonly GeoEngineInit _geoEngine;
        
        public PlayerDesireCast(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _geoEngine = _playerInstance.ServiceProvider.GetRequiredService<GeoEngineInit>();
        }

        public async Task DoCastAsync(SkillDataModel skill)
        {
            var target = GetTarget(skill.TargetType);
            if (target == null)
            {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.CantSeeTarget));
                return;
            }
            
            if (!_geoEngine.CanSee(_playerInstance.GetX(), _playerInstance.GetY(), _playerInstance.GetZ(), 33,
                    target.GetX(), target.GetY(), target.GetZ(), 20
                    )
            ) {
                await _playerInstance.SendPacketAsync(new SystemMessage(SystemMessageId.TargetIsIncorrect));
                return;
            }

            // Get the Identifier of the skill
            int skillId = skill.SkillId;
            //todo need add calculator
            float coolTime = skill.SkillCoolTime;
            float hitTime = skill.SkillHitTime * 1000;
            float reuseDelay = skill.ReuseDelay * 1000;
            await HandleMagicSkill(skill, target, hitTime);
            await SendToKnownListAsync(skill, target, hitTime, reuseDelay);
            await _playerInstance.SendPacketAsync(new SetupGauge(SetupGauge.Blue, hitTime));
            // Send a system message to the Player
            await _playerInstance.PlayerMessage().SendMessageToPlayerAsync(skill, skillId);
            await _playerInstance.SendUserInfoAsync();
        }

        /// <summary>
        /// Get Selected Target
        /// </summary>
        /// <returns></returns>
        private PlayerInstance GetTarget(TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Self:
                    return _playerInstance;
                case TargetType.Target:
                    return _playerInstance.PlayerTargetAction().GetTarget();
                case TargetType.None:
                    break;
                case TargetType.EnemyOnly:
                    return _playerInstance.PlayerTargetAction().GetTarget();
                case TargetType.Enemy:
                    return _playerInstance.PlayerTargetAction().GetTarget();
                case TargetType.HolyThing:
                    break;
                case TargetType.Summon:
                    break;
                case TargetType.NpcBody:
                    break;
                case TargetType.DoorTreasure:
                    break;
                case TargetType.PcBody:
                    break;
                case TargetType.Others:
                    break;
                case TargetType.Item:
                    break;
                case TargetType.WyvernTarget:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }
            return _playerInstance;
        }

        private async Task HandleMagicSkill(SkillDataModel skill, PlayerInstance target, float hitTime)
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
                            await value.Process(target);
                        }, (int)hitTime, _cts.Token);
                    }
                }
                catch (Exception ex)
                {
                    LoggerManager.Error(skill.SkillName + " " + ex.Message);
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