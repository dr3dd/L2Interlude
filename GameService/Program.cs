using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Config;
using Core;
using Core.Controller;
using Core.Controller.Handlers;
using DataBase;
using L2Logger;

namespace GameService
{
    static class Program
    {
        private static async Task Main()
        {
            ClassLoggerConfigurator.ConfigureClassLogger($"./log/{Assembly.GetExecutingAssembly().ManifestModule.Name}.log");
            
            LoggerManager.Info("Starting GameService...");
            
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            serviceProvider.DbMigrationGame();

            await Task.Factory.StartNew(serviceProvider.GetRequiredService<LoginServiceController>().StartAsync);
            await Task.Factory.StartNew(serviceProvider.GetRequiredService<GameService>().StartAsync);
            Process.GetCurrentProcess().WaitForExit();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            GameConfigDependencyBinder.Bind(services);
            GameDataBaseDependencyBinder.Bind(services);
            CoreDependencyBinder.Bind(services);
            
            services.AddSingleton<Initializer>();
            services.AddSingleton<LoginServicePacketHandler>();
            services.AddSingleton<LoginServiceController>();
            services.AddSingleton<GameServicePacketHandler>();
            services.AddSingleton<GameServiceController>();
            services.AddSingleton<ClientManager>();
            services.AddSingleton<GameService>();
        }
    }
}
