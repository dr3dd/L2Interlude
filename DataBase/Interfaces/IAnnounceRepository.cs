using DataBase.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 13.01.2025 17:58:32

namespace DataBase.Interfaces
{
    public interface IAnnounceRepository : IGenericRepository<AnnounceEntity>
    {
        Task<IReadOnlyList<AnnounceEntity>> GetLoginAnnounces();
        Task<IReadOnlyList<AnnounceEntity>> GetIntervalAnnounces();
    }
}