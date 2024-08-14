using DataBase.Interfaces;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.08.2024 0:13:51

namespace DataBase
{
    public class LoginUnitOfWork : IUnitOfWorkLogin
    {
        public IAccountRepository Accounts { get; }
        public LoginUnitOfWork(
            IAccountRepository accountRepository

            )
        {
            Accounts = accountRepository;

        }
    }
}
