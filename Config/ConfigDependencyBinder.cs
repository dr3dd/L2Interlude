using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Config
{
    public static class ConfigDependencyBinder
    {
        public static void Bind(IServiceCollection provider)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            
            var config = builder.Build();
            var gameServiceConfig = config.GetSection(ServerConfig.GameServiceSection).Get<ServerConfig>();
            var dataBaseConfig = config.GetSection(DataBaseConfig.DataBaseSection).Get<DataBaseConfig>();
            var loginServiceConfig = config.GetSection(LoginServerConfig.LoginServiceConfig).Get<LoginServerConfig>();
            var gameConfig = new GameConfig
            {
                ServerConfig = gameServiceConfig,
                DataBaseConfig = dataBaseConfig,
                LoginServerConfig = loginServiceConfig
            };

            provider.AddSingleton(gameConfig);
        }
    }
}