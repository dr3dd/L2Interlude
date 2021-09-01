using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.SkillData;

namespace Core.Module.Player
{
    public class PlayerSkillMagic
    {
        private readonly PlayerInstance _playerInstance;

        public PlayerSkillMagic(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }

        public async Task UseMagicAsync(SkillDataModel skill, bool forceUse, bool dontMove)
        {
            _playerInstance.PlayerDesire().AddDesire(Desire.CastDesire, skill);
        }
    }
}