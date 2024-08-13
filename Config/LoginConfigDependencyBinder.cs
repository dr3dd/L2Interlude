using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 13.08.2024 23:40:04

namespace Config
{
    public class LoginConfigDependencyBinder
    {
        public static void Bind(IServiceCollection provider)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"config/server.json", false, true)
                .AddJsonFile(@"config/debug.json", false, true)
                .Build();

            var loginServiceConfig = config.GetSection(LoginServerConfig.LoginServiceConfig).Get<LoginServerConfig>();
            var dataBaseConfig = config.GetSection(DataBaseConfig.DataBaseSection).Get<DataBaseConfig>();
            var debugConfig = config.GetSection(DebugConfig.DebugSection).Get<DebugConfig>();
            var loginConfig = new LoginConfig
            {
                ServerConfig = loginServiceConfig,
                DataBaseConfig = dataBaseConfig,
                DebugConfig = debugConfig
            };

            provider.AddSingleton(loginConfig);
        }
    }
}
