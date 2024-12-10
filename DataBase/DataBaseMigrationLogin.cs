using Config;
using DbUp;
using DbUp.ScriptProviders;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Text;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.08.2024 9:29:13

namespace DataBase
{
    public static class DataBaseMigrationLogin
    {
        public static void DbMigrationLogin(this IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<LoginConfig>();
            LoggerManager.Info("DbMigrationLogin: initialise...");
            try
            {
                var connectionString = $"Server={config.DataBaseConfig.DataBaseHost};" +
                                       $"Port={config.DataBaseConfig.DataBasePort};" +
                                       $"Database={config.DataBaseConfig.DataBaseName};" +
                                       $"Uid={config.DataBaseConfig.DataBaseUser};" +
                                       $"Pwd={config.DataBaseConfig.DataBasePassword}"
                    ;

                if (config.DataBaseConfig.DataBaseAutoCreate)
                {
                    EnsureDatabase.For.MySqlDatabase(connectionString);
                }

                var options = new FileSystemScriptOptions
                {
                    IncludeSubDirectories = true,
                    Extensions = new[] { "*.sql" },
                    Encoding = Encoding.UTF8
                };

                var upgrader =
                    DeployChanges.To
                        .MySqlDatabase(connectionString)
                        .WithScriptsFromFileSystem("sql", options)
                        .LogScriptOutput()
                        .Build();

                var result = upgrader.PerformUpgrade();
                if (!result.Successful)
                {
                    LoggerManager.Error(result.Error.Message);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }
    }
}
