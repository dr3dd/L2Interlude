using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface IUserSkillRepository : IGenericRepository<UserSkillEntity>
    {
        Task<List<UserSkillEntity>> GetSkillsByCharId(int charId);
    }
}