using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.SkillData;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
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
                //_skills.TryAdd(us.SkillId, _skillDataInit.GetSkillBySkillIdAndLevel(us.SkillId, us.SkillLevel));
                _skills.AddOrUpdate(us.SkillId,
                    i => _skillDataInit.GetSkillBySkillIdAndLevel(us.SkillId, us.SkillLevel),
                    (i, update) => _skillDataInit.GetSkillBySkillIdAndLevel(us.SkillId, us.SkillLevel));
            });
            return _skills;
        }
        
        private List<SkillDataModel> GetAllSkills()
        {
            return _skills.Values.ToList();
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

        public async Task RestorePassiveSkills()
        {
            var playerSkill = await GetPlayerSkills();
            foreach (var skill in playerSkill.Where(s => s.Value.OperateType == OperateType.P))
            {
                if (skill.Value.Effects is null)
                    continue;
                var effect = skill.Value.Effects.SingleOrDefault().Value;
                _playerInstance.PlayerEffect().AddEffect(effect, 0, 0);
            }
        }
        
        public void AddSkill(SkillDataModel newSkill, bool store = false)
        {
            // Add or update a PlayerInstance skill in the character_skills table of the database
            try
            {
                if (store)
                {
                    SaveSkill(newSkill);
                }
                _skills.TryAdd(newSkill.SkillId, newSkill);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex);
            }
        }
        
        private void SaveSkill(SkillDataModel newSkill)
        {
            var skill = GetAllSkills().FirstOrDefault(s => s.SkillId == newSkill.SkillId);
            var characterId = _playerInstance.PlayerCharacterInfo().CharacterId;
            var userSkillEntity = new UserSkillEntity
            {
                CharacterId = characterId,
                SkillId = newSkill.SkillId,
                SkillLevel = newSkill.Level,
                ToEndTime = 0
            };
            if (skill is null)
            {
                _userSkillRepository.AddAsync(userSkillEntity);
                return;
            }
            _userSkillRepository.UpdateAsync(userSkillEntity);
        }
    }
}