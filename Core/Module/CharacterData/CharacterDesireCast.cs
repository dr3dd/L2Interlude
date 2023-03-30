using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Controller;
using Core.GeoEngine;
using Core.Module.Player;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.CharacterData
{
    public class CharacterDesireCast
    {
        private readonly Character _character;
        private CancellationTokenSource _cts;
        private readonly GeoEngineInit _geoEngine;
        private int _castEndTime;
        private int _castInterruptTime;
        private readonly GameTimeController _timeController;
        private readonly IList<SkillDataModel> _disabledSkills;
        
        public CharacterDesireCast(Character character)
        {
            _character = character;
            _geoEngine = character.ServiceProvider.GetRequiredService<GeoEngineInit>();
            _timeController = character.ServiceProvider.GetRequiredService<GameTimeController>();
            _disabledSkills = new List<SkillDataModel>();
        }

        public async Task DoCastAsync(SkillDataModel skill)
        {
            var target = GetTarget(skill.TargetType);
            if (target == null)
            {
                await _character.SendPacketAsync(new SystemMessage(SystemMessageId.CantSeeTarget));
                return;
            }
            var castRange = skill.CastRange;
            if (!CalculateRange.CheckIfInRange(castRange, _character.GetX(), _character.GetY(),
                    _character.GetZ(), _character.CharacterCombat().GetCollisionRadius(), target.GetX(), target.GetY(),
                    target.GetZ(), target.CharacterCombat().GetCollisionRadius(), true)
            )
            {
                await _character.SendPacketAsync(new SystemMessage(SystemMessageId.TargetTooFar));
                return;
            }
            
            if (!_geoEngine.CanSee(_character.GetX(), _character.GetY(), _character.GetZ(),
                    _character.CharacterCombat().GetCollisionHeight(),
                    target.GetX(), target.GetY(), target.GetZ(), target.CharacterCombat().GetCollisionHeight()
                )
               ) {
                await _character.SendPacketAsync(new SystemMessage(SystemMessageId.CantSeeTarget));
                return;
            }

            // Get the Identifier of the skill
            int skillId = skill.SkillId;
            //todo need add calculator
            short coolTime = (short) skill.SkillCoolTime;
            short hitTime = (short)(skill.SkillHitTime * 1000);
            short reuseDelay = (short)(skill.ReuseDelay * 1000);
            
            DisableSkill(skill, reuseDelay);
            SetCastTime(coolTime, hitTime);
            
            await HandleMagicSkill(skill, target, hitTime);
            
            await SendToKnownListAsync(skill, target, hitTime, reuseDelay);
            await _character.SendPacketAsync(new SetupGauge(SetupGauge.Blue, hitTime));
            // Send a system message to the Player
            if (_character is PlayerInstance playerInstance)
            {
                await CharacterMessage.SendMessageToPlayerAsync(_character, skill, skillId);
                await playerInstance.SendUserInfoAsync();                
            }
        }

        private void SetCastTime(short coolTime, short hitTime)
        {
            _castEndTime = 10 + _timeController.GameTicks +
                                 (coolTime + hitTime) / _timeController.MillisInTick;
            _castInterruptTime = -2 + _timeController.GameTicks +
                                       (hitTime / _timeController.MillisInTick);
        }
        
        private void DisableSkill(SkillDataModel skill, int reuseDelay)
        {
            if (reuseDelay <= 10) return;
            if (!_disabledSkills.Contains(skill))
            {
                _disabledSkills.Add(skill);
            }
            TaskManagerScheduler.Schedule(() =>
            {
                EnableSkill(skill);
            }, reuseDelay);
        }

        private void EnableSkill(SkillDataModel skill)
        {
            _disabledSkills.Remove(skill);
        }
        
        public bool IsSkillDisabled(SkillDataModel skill)
        {
            return _disabledSkills.Contains(skill);
        }
        
        public bool IsCastingNow()  
        {
            return _castEndTime > _timeController.GameTicks;
        }

        /// <summary>
        /// Get Selected Target
        /// </summary>
        /// <returns></returns>
        private Character GetTarget(TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Self:
                    return _character;
                case TargetType.Target:
                    return (Character) _character.CharacterTargetAction().GetTarget();
                case TargetType.None:
                    break;
                case TargetType.EnemyOnly:
                    return (Character) _character.CharacterTargetAction().GetTarget();
                case TargetType.Enemy:
                    return (Character) _character.CharacterTargetAction().GetTarget();
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
            return _character;
        }

        private async Task HandleMagicSkill(SkillDataModel skill, Character target, float hitTime)
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
                            await value.Process(_character, target);
                        }, (int)hitTime, _cts.Token);
                    }
                }
                catch (Exception ex)
                {
                    LoggerManager.Error(skill.SkillName + " " + ex.Message);
                }
            });
        }

        private async Task SendToKnownListAsync(SkillDataModel skill, Character target, float hitTime, float reuseDelay)
        {
            var skillUse = new MagicSkillUse(_character, target, skill.SkillId, skill.Level, hitTime, reuseDelay);
            await _character.SendPacketAsync(skillUse);
            await _character.SendToKnownPlayers(skillUse);
        }
    }
}