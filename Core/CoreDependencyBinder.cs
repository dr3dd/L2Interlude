using Core.Module.CharacterData.Template;
using Core.Module.NpcData;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core
{
    public static class CoreDependencyBinder
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddSingleton<NetworkWriter>();
            services.AddTransient<ClientManager>();
            services.AddSingleton<TemplateInit>();
            services.AddSingleton<NpcDataInit>();
        }
    }
}