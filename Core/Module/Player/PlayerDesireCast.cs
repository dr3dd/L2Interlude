using System.Threading.Tasks;
using Core.Module.SkillData;

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
            // Get the Identifier of the skill
            int skillId = skill.SkillId;
            // Send a system message to the Player
            await _playerInstance.PlayerMessage().SendMessageToPlayerAsync(skill, skillId);
        }
    }
}