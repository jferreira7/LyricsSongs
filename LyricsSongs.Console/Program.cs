namespace LyricsSongs.Console
{
    using LyricsSongs.Console.Menus;
    using LyricsSongs.Console.Service;
    using LyricsSongs.Console.Services;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Text;

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            ServiceProvider serviceProvider = new ServiceCollection()
              .AddSingleton<IJsonFileService, JsonFileService>()
              .BuildServiceProvider();

            IJsonFileService jsonFileService = serviceProvider.GetService<IJsonFileService>()!;

            MenuPrincipal menuPrincipal = new MenuPrincipal(jsonFileService);
            menuPrincipal.Exibir().Wait();
        }
    }
}