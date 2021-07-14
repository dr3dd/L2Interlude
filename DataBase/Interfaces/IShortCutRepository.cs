using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface IShortCutRepository : IGenericRepository<ShortCutEntity>
    {
        Task<List<ShortCutEntity>> GetShortCutsByOwnerIdAsync(int ownerId, int classIndex);
        Task<int> DeleteShortCutAsync(ShortCutEntity shortCutEntity);
    }
}