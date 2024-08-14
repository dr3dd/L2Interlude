//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.08.2024 0:14:43

namespace DataBase.Interfaces
{
    public interface IUnitOfWorkLogin
    {
        IAccountRepository Accounts { get; }
    }
}
