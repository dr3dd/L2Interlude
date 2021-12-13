using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Config;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace NpcService
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            ClassLoggerConfigurator.ConfigureClassLogger($"{Assembly.GetExecutingAssembly().Location}.log");
            LoggerManager.Info("Starting NpcService...");
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            await Task.Factory.StartNew(serviceProvider.GetRequiredService<NpcService>().StartAsync);
            await Process.GetCurrentProcess().WaitForExitAsync();
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            ConfigDependencyBinder.Bind(services);
            services.AddSingleton<GameServiceHandler>();
            services.AddSingleton<NpcService>();
        }
    }
}