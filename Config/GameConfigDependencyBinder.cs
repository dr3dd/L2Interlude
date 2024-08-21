using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Config
{
    public static class GameConfigDependencyBinder
    {
        public static void Bind(IServiceCollection provider)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"config/server.json", false, true)
                .AddJsonFile(@"config/debug.json", false, true)
                .AddJsonFile(@"config/access.json", false, true)
                .Build();

            var gameServiceConfig = config.GetSection(GameServerConfig.GameServiceSection).Get<GameServerConfig>();
            var dataBaseConfig = config.GetSection(DataBaseConfig.DataBaseSection).Get<DataBaseConfig>();
            var debugConfig = config.GetSection(DebugConfig.DebugSection).Get<DebugConfig>();
            var accessConfig = config.GetSection(AccessConfig.AccessSection).Get<AccessConfig>();
            var gameConfig = new GameConfig
            {
                ServerConfig = gameServiceConfig,
                DataBaseConfig = dataBaseConfig,
                DebugConfig = debugConfig,
                AccessConfig = accessConfig
            };

            provider.AddSingleton(gameConfig);
        }
    }
}