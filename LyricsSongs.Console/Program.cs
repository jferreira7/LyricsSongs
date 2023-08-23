namespace LyricsSongs.Console
{
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
            //Environment.SetEnvironmentVariable("lyricssongs_token_api_vagalume", "", EnvironmentVariableTarget.User);
            //setx lyricssongs_token_api_vagalume ""

            Console.OutputEncoding = Encoding.UTF8;

            ChecarSeOTokenExiste();

            ServiceProviderFactory.Initializer();

            (new MenuPrincipal()).Exibir().Wait();
        }

        private static void ChecarSeOTokenExiste()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("lyricssongs_token_api_vagalume", EnvironmentVariableTarget.User))) return;

            if (!string.IsNullOrEmpty(configuration["Settings:Token_Vagalume"]))
                if (SetApiTokenEnvVariable(configuration["Settings:Token_Vagalume"])) return;

            string? token = "";
            do
            {
                Console.Write("Digite um token válido (https://api.vagalume.com.br/): ");
                token = Console.ReadLine();

                if (token != null && token.Trim() != "")
                {
                    SetApiTokenEnvVariable(token);
                    Console.WriteLine("Token cadastrado!");
                    Thread.Sleep(1000);
                    break;
                }
                else
                {
                    Console.WriteLine("Token inválido");
                }
            } while (true);
        }

        private static bool SetApiTokenEnvVariable(string? token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            Environment.SetEnvironmentVariable("lyricssongs_token_api_vagalume", token, EnvironmentVariableTarget.User);
            return true;
        }
    }
}