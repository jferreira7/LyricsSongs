namespace LyricsSongs.Console
{
    using LyricsSongs.Console.API;
    using System;

    internal class Program
    {

        static async Task Main()
        {
            //Dictionary<int, Menu> opcoes = new();
            //opcoes.Add(1, new MenuBuscarMusica());
            //opcoes.Add(2, new MenuLetrasSalvas());
            //opcoes.Add(3, new MenuConfiguracoes());
            //opcoes.Add(0, new MenuSair());

            //Menu menu = new MenuPrincipal();
            //menu.Exibir();

            //int opcaoSelecionada = Console.Read();

            //if (opcoes.ContainsKey(opcaoSelecionada)) { } else { Console.WriteLine("Opção inválida!"); }


            ApiVagalume api = new();
            await api.SearchMusicas("vamos fugir");

            Console.ReadLine();
        }
    }
}