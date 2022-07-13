using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;

namespace Core.Module.Player.Validators
{
    public class GeneralPlayerSkillMagicValidator : IPlayerSkillMagicValidator
    {
        public async Task<bool> IsValid(PlayerInstance playerInstance, SkillDataModel skill)
        {
            if (IsCastingNow(playerInstance)) return await Task.FromResult(IsCastingNow(playerInstance));
            if (IsSkillDisabled(playerInstance, skill)) return await Task.FromResult(IsSkillDisabled(playerInstance, skill));
            return await Task.FromResult(true);
        }

        private bool IsCastingNow(PlayerInstance playerInstance)
        {
            return playerInstance.CharacterDesire().DesireCast().IsCastingNow();
        }
        
        private bool IsSkillDisabled(PlayerInstance playerInstance, SkillDataModel skill)
        {
            if (!playerInstance.CharacterDesire().DesireCast().IsSkillDisabled(skill)) return false;
            SystemMessage sm = new SystemMessage(SystemMessageId.S1PreparedForReuse);
            sm.AddSkillName(skill.SkillId, skill.Level);
            playerInstance.SendPacketAsync(sm);
            return true;
        }
    }
}