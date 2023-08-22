namespace LyricsSongs.Console
{
    using LyricsSongs.Console.Menus;
    using LyricsSongs.Console.Services;
    using LyricsSongs.Console.Views;
    using System;
    using System.Text;

    internal class Program : View
    {
        private static void Main()
        {
            //Environment.SetEnvironmentVariable("token_api_vagalume", "", EnvironmentVariableTarget.User);
            Console.OutputEncoding = Encoding.UTF8;

            checarSeOTokenExiste();

            ServiceProviderFactory.Initializer();

            (new MenuPrincipal()).Exibir().Wait();
        }

        private static void checarSeOTokenExiste()
        {
            string? token = Environment.GetEnvironmentVariable("token_api_vagalume");

            if (token != null) return;

            do
            {
                Console.Write("Digite um token válido (https://api.vagalume.com.br/): ");
                token = Console.ReadLine();

                if (token != null && token.Trim() != "")
                {
                    Environment.SetEnvironmentVariable("token_api_vagalume", token, EnvironmentVariableTarget.User);
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
    }
}