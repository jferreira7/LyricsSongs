using LyricsSongs.Console.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LyricsSongs.Console.Services
{
    public static class ServiceProviderFactory
    {
        private static ServiceProvider? _serviceProvider;

        public static void Initializer()
        {
            if (ServiceProviderFactory._serviceProvider != null) return;

            ServiceProviderFactory._serviceProvider = new ServiceCollection()
              .AddSingleton<IJsonFileService, JsonFileService>()
              .AddSingleton<ITokenService, TokenService>()
              .BuildServiceProvider();
        }

        public static T GetService<T>() where T : notnull => ServiceProviderFactory._serviceProvider!.GetRequiredService<T>();
    }
}