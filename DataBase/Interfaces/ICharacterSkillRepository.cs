using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface ICharacterSkillRepository : IGenericRepository<CharacterSkillEntity>
    {
        Task<List<CharacterSkillEntity>> GetSkillsByCharObjectAndClassIndex(int ownerId, int classId);
    }
}