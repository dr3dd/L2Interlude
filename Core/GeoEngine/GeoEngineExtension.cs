using Microsoft.Extensions.DependencyInjection;

namespace Core.GeoEngine
{
    public static class GeoEngineExtension
    {
        public static IServiceCollection UseGeoEngine(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<GeoEngineInit>();
        }
    }
}