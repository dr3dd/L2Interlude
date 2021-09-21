using System.Linq;
using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.Module.SkillData.Effects;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Player
{
    public class PlayerDesireCast
    {
        private readonly PlayerInstance _playerInstance;
        
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
            await StartEffect(skill);
            await SendToKnownListAsync(skill, target, hitTime, reuseDelay);
            // Send a system message to the Player
            await _playerInstance.PlayerMessage().SendMessageToPlayerAsync(skill, skillId);
            await _playerInstance.SendUserInfoAsync();
        }

        private async Task StartEffect(SkillDataModel skill)
        {
            var effects = skill.Effects;
            foreach (var (key, value ) in effects)
            {
                key.Process(value);
            }
            /*
            await Task.Run(() =>
            {
                Effect effect = skill.Effects.First().Key;
                effect.SkillDataModel = skill;
                effect.StartEffectTask(1200 * 1000, _playerInstance);
            });
            */
        }

        private async Task SendToKnownListAsync(SkillDataModel skill, PlayerInstance target, float hitTime, float reuseDelay)
        {
            await _playerInstance.SendPacketAsync(new MagicSkillUse(_playerInstance, target, skill.SkillId, skill.Level, hitTime, reuseDelay));
        }
    }
}