using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.Module.SkillData.Effects;
using Core.NetworkPacket.ServerPacket;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerDesireCast
    {
        private readonly PlayerInstance _playerInstance;
        private readonly EffectInit _effectInit;
        
        public PlayerDesireCast(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _effectInit = _playerInstance.ServiceProvider.GetRequiredService<EffectInit>();
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
            await HandleMagicSkill(skill);
            await SendToKnownListAsync(skill, target, hitTime, reuseDelay);
            // Send a system message to the Player
            await _playerInstance.PlayerMessage().SendMessageToPlayerAsync(skill, skillId);
            await _playerInstance.SendUserInfoAsync();
        }

        private async Task HandleMagicSkill(SkillDataModel skill)
        {
            try
            {
                var effects = skill.Effects;
                foreach (var (key, value) in effects)
                {
                    var effect = (Effect)Activator.CreateInstance(_effectInit.GetEffectHandler(key));
                    await effect.Process(value, skill, _playerInstance);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }

        private async Task SendToKnownListAsync(SkillDataModel skill, PlayerInstance target, float hitTime, float reuseDelay)
        {
            await _playerInstance.SendPacketAsync(new MagicSkillUse(_playerInstance, target, skill.SkillId, skill.Level, hitTime, reuseDelay));
        }
    }
}