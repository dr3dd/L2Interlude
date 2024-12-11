using Config;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Data;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.08.2024 0:08:19

namespace DataBase
{
    public class LoginConnectionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private MySqlConnection _connection;

        public LoginConnectionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDbConnection GetDbConnection()
        {
            var config = _serviceProvider.GetService<LoginConfig>();
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
