using System.Threading.Tasks;
using DataBase.Entities;

namespace DataBase.Interfaces
{
    public interface IAccountRepository : IGenericRepository<UserAuthEntity>
    {
        Task<UserAuthEntity> GetAccountByLoginAsync(string login);
        Task<UserAuthEntity> CreateAccountAsync(string login, string password);
        Task<int> UpdateLastUseAsync(UserAuthEntity userAuthEntity);
    }
}