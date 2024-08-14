using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Config;
using DataBase;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace LoginService
{
    internal static class Program
    {
        static async Task Main()
        {
            ClassLoggerConfigurator.ConfigureClassLogger($"./log/{Assembly.GetExecutingAssembly().ManifestModule.Name}.log");
            
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            
            LoggerManager.Info("Starting Login Service...");

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            serviceProvider.DbMigrationLogin();

            await Task.Factory.StartNew(serviceProvider.GetRequiredService<LoginService>().StartAsync);
            Process.GetCurrentProcess().WaitForExit();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            LoginConfigDependencyBinder.Bind(services);
            LoginDataBaseDependencyBinder.Bind(services);
            services.AddSingleton<LoginPacketHandler>();
            services.AddSingleton<LoginController>();
            services.AddSingleton<GameServerPacketHandler>();
            services.AddSingleton<GameServerClient>();
            services.AddSingleton<GameServerListener>();
            services.AddSingleton<LoginService>();
        }
    }
}
