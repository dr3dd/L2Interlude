using DataBase.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 23:02:19

namespace DataBase.Interfaces
{
    public interface IMacrosRepository : IGenericRepository<UserMacrosEntity>
    {
        Task<List<UserMacrosEntity>> GetMarcosesByOwnerIdAsync(int ownerId);
        Task<int> DeleteMarcosAsync(UserMacrosEntity macrosEntity);
    }
}
