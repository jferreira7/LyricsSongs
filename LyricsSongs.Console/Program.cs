namespace LyricsSongs.Console
{
    using LyricsSongs.Console.API;
    using LyricsSongs.Console.Menus;
    using LyricsSongs.Console.Services;
    using LyricsSongs.Console.Views;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Text;

    internal class Program : View
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            ServiceProviderFactory.Initializer();

#if DEBUG
            if (!IsInDebbugingMode())
#endif
                getTokenService().CheckIfTokenExists();

            (new MenuPrincipal()).Exibir().Wait();
        }

        public static ITokenService getTokenService()
        {
            return ServiceProviderFactory.GetService<ITokenService>();
        }

        public static bool IsInDebbugingMode()
        {
            if (!System.Diagnostics.Debugger.IsAttached) return false;

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            if (!string.IsNullOrEmpty(configuration["Settings:Token_Vagalume"]))
            {
                ApiVagalume.SetApiKey(configuration["Settings:Token_Vagalume"]!);
            }
            else
            {
                Console.WriteLine("Digite um token válido no arquivo 'appsettings.Development.json'");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }

            return true;
        }
    }
}