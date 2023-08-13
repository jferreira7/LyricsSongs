namespace LyricsSongs.Console
{
    using LyricsSongs.Console.Menus;
    using System;

    internal class Program
    {

        static void Main()
        {
            MainAsync().Wait();
        }

        public static async Task MainAsync()
        {
            Dictionary<int, Menu> opcoes = new();
            opcoes.Add(1, new MenuBuscarMusica());
            opcoes.Add(2, new MenuLetrasSalvas());
            opcoes.Add(3, new MenuConfiguracoes());
            opcoes.Add(0, new MenuSair());

            do
            {

                Menu menu = new MenuPrincipal();
                await menu.Exibir();

                string? input = Console.ReadLine();
                int opcaoSelecionada = int.Parse(input!);

                if (opcoes.ContainsKey(opcaoSelecionada))
                {
                    Console.Clear();
                    menu = opcoes[opcaoSelecionada];
                    await menu.Exibir();
                }
                else
                {
                    Console.WriteLine("\nOpção inválida!");
                    Thread.Sleep(2000);
                    continue;
                }

                Console.WriteLine("\nClique em qualquer tecla para voltar ao menu principal.");
                Console.ReadLine();
                Console.Clear();

            } while (true);
        }
    }
}