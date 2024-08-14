using Dapper.FluentMap;
using DataBase.Entities.Map;
using DataBase.Interfaces;
using DataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.08.2024 0:10:56

namespace DataBase
{
    public class LoginDataBaseDependencyBinder
    {
        public static void Bind(IServiceCollection services)
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserAuthMap());
            });

            services.AddSingleton<LoginConnectionFactory>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddSingleton<IUnitOfWorkLogin, LoginUnitOfWork>();
        }
    }
}
