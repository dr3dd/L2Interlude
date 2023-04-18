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
            ClassLoggerConfigurator.ConfigureClassLogger($"{Assembly.GetExecutingAssembly().Location}.log");
            
            LoggerManager.Info("Starting GameService...");
            
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            serviceProvider.DbMigration();

            await Task.Factory.StartNew(serviceProvider.GetRequiredService<LoginServiceController>().StartAsync);
            await Task.Factory.StartNew(serviceProvider.GetRequiredService<GameService>().StartAsync);
            Process.GetCurrentProcess().WaitForExit();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            ConfigDependencyBinder.Bind(services);
            DataBaseDependencyBinder.Bind(services);
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
