using System;
using System.Data;
using Config;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace DataBase
{
    public class GameConnectionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private MySqlConnection _connection;

        public GameConnectionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IDbConnection GetDbConnection()
        {
            var config = _serviceProvider.GetService<GameConfig>();
            try
            {
                var connectionString = $"Server={config.DataBaseConfig.DataBaseHost};" +
                                       $"Port={config.DataBaseConfig.DataBasePort};" +
                                       $"Database={config.DataBaseConfig.DataBaseName};" +
                                       $"Uid={config.DataBaseConfig.DataBaseUser};" +
                                       $"Pwd={config.DataBaseConfig.DataBasePassword}"
                    ;
                _connection = new MySqlConnection(connectionString);
                return _connection;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                _connection?.Dispose();
                throw;
            }
        }
    }
}