namespace LyricsSongs.Console
{
    using Menus;
    using System;

    internal class Program
    {


        static void Main(string[] args)
        {
            Dictionary<int, Menu> opcoes = new();
            opcoes.Add(1, new MenuPrincipal());
            opcoes.Add(2, new MenuLetrasSalvas());
            opcoes.Add(3, new MenuConfiguracoes());
            opcoes.Add(0, new MenuSair());

            Menu menu = new MenuPrincipal();
            menu.Exibir();

            int opcaoSelecionada = Console.Read();

            if (opcoes.ContainsKey(opcaoSelecionada)) { } else { Console.WriteLine("Opção inválida!"); }
        }
    }
}