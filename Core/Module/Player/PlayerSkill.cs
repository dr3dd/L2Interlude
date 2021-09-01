using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.Player
{
    public class PlayerSkill
    {
        private readonly PlayerInstance _playerInstance;
        private readonly IUserSkillRepository _userSkillRepository;
        private readonly SkillPchInit _skillPchInit;
        private readonly SkillDataInit _skillDataInit;

        public PlayerSkill(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _userSkillRepository = playerInstance.GetUnitOfWork().UserSkill;
            _skillPchInit = playerInstance.ServiceProvider.GetRequiredService<SkillPchInit>();
            _skillDataInit= playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
        }

        public async Task SendSkillListAsync()
        {
            var playerSkill = await GetPlayerSkills();
            SkillList sl = new SkillList();
            foreach (var item in playerSkill)
            {
                sl.AddSkill(item.Value);
            }
            await _playerInstance.SendPacketAsync(sl);
        }

        public async Task<Dictionary<int, SkillDataModel>> GetPlayerSkills()
        {
            var characterId = _playerInstance.PlayerCharacterInfo().CharacterId;
            var userSkills = await _userSkillRepository.GetSkillsByCharId(characterId);
            Dictionary<int, SkillDataModel> skills = new Dictionary<int, SkillDataModel>();
            userSkills.ForEach(us =>
            {
                var skillName = _skillPchInit.GetSkillNameById(us.SkillId);
                skills.Add(us.SkillId, _skillDataInit.GetSkillByName(skillName));
            });
            return skills;
        }
    }
}