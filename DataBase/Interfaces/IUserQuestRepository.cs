using DataBase.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IUserQuestRepository : IGenericRepository<UserQuestEntity>
    {
        Task<int> DeleteAsync(UserQuestEntity entity);
        Task<List<UserQuestEntity>> GetAllAsync(int charId);
    }
}