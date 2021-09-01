using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<int, SkillDataModel> _skills;

        public PlayerSkill(PlayerInstance playerInstance)
        {
            _skills = new ConcurrentDictionary<int, SkillDataModel>();
            _playerInstance = playerInstance;
            _userSkillRepository = playerInstance.GetUnitOfWork().UserSkill;
            _skillPchInit = playerInstance.ServiceProvider.GetRequiredService<SkillPchInit>();
            _skillDataInit = playerInstance.ServiceProvider.GetRequiredService<SkillDataInit>();
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

        public async Task<ConcurrentDictionary<int, SkillDataModel>> GetPlayerSkills()
        {
            var characterId = _playerInstance.PlayerCharacterInfo().CharacterId;
            var userSkills = await _userSkillRepository.GetSkillsByCharId(characterId);
            userSkills.ForEach(us =>
            {
                _skills.TryAdd(us.SkillId, _skillDataInit.GetSkillBySkillIdAndLevel(us.SkillId, us.SkillLevel));
            });
            return _skills;
        }
        
        public int GetSkillLevel(int skillId)
        {
            if (_skills.ContainsKey(skillId))
            {
                SkillDataModel skill = _skills[skillId];
                return skill?.Level ?? 0;
            }
            return 0;
        }
    }
}