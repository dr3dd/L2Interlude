using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player.Validators;
using Core.Module.SkillData;

namespace Core.Module.Player
{
    public class PlayerSkillMagic
    {
        private readonly PlayerInstance _playerInstance;
        private readonly IPlayerSkillMagicValidator _validator;

        public PlayerSkillMagic(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _validator = new GeneralPlayerSkillMagicValidator();
        }

        public async Task UseMagicAsync(SkillDataModel skill, bool forceUse, bool dontMove)
        {
            if (!await _validator.IsValid(_playerInstance, skill))
            {
                await _playerInstance.SendActionFailedPacketAsync();
                return;
            }
            _playerInstance.PlayerDesire().AddDesire(Desire.CastDesire, skill);
        }
    }
}