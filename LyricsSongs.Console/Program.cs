namespace LyricsSongs.Console
{
    using LyricsSongs.Console.Menus;
    using System;

    internal class Program
    {

        static void Main()
        {
            Dictionary<int, Menu> opcoes = new();
            opcoes.Add(1, new MenuBuscarMusica());
            opcoes.Add(2, new MenuLetrasSalvas());
            opcoes.Add(3, new MenuConfiguracoes());
            opcoes.Add(0, new MenuSair());

            Menu menu = new MenuPrincipal();
            menu.Exibir();

            string? input = Console.ReadLine();
            int opcaoSelecionada = int.Parse(input!);

            if (opcoes.ContainsKey(opcaoSelecionada))
            {
                Console.Clear();
                menu = opcoes[opcaoSelecionada];
                menu.Exibir();
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }

            Console.ReadLine();
        }
    }
}