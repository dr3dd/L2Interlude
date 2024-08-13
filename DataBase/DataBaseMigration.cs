﻿using System;
using System.Reflection;
using Config;
using DbUp;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase
{
    public static class DataBaseMigration
    {
        public static void DbMigration(this IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<GameConfig>();
            LoggerManager.Info("DbMigrationGame: initialise...");
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

                var upgrader =
                    DeployChanges.To
                        .MySqlDatabase(connectionString)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                        .LogToAutodetectedLog()
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