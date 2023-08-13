namespace LyricsSongs.Console.Menus
{
    using LyricsSongs.Console.API;
    using LyricsSongs.Console.Models;
    using System;

    internal class MenuBuscarMusica : Menu
    {
        public Dictionary<int, Musica> musicasEncontradas = new();
        ApiVagalume api;

        public MenuBuscarMusica()
        {
            api = new();
        }

        public async override void Exibir()
        {
            Console.WriteLine("Buscar musica\n");

            Console.Write("Digite o nome da musica: ");
            string buscaTexto = Console.ReadLine()!;

            await this.buscarMusicasParaSelecionar(buscaTexto);
        }

        public async Task buscarMusicasParaSelecionar(string buscaTexto)
        {
            this.musicasEncontradas = await this.api.SearchMusicas(buscaTexto);

            await this.mostrarMusicasBuscadas();
        }

        public async Task mostrarMusicasBuscadas()
        {
            Console.WriteLine("Músicas encontradas: ");
            foreach (var item in this.musicasEncontradas)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Nome} - {item.Value.Banda}");
            }

            //Console.Write("Selecione a música correta: ");
            //string input = Console.ReadLine()!;
            //if (int.TryParse(input, out int musicaEscolhida))
            //{
            await this.mostrarMusicaSelecionada(int.Parse(Console.ReadLine()!));
            //}
            //else
            //{
            //    Console.WriteLine("Entrada inválida. Por favor, digite um número inteiro válido.");
            //}
            //Console.ReadKey();
        }

        public async Task mostrarMusicaSelecionada(int musicaEscolhida)
        {
            Musica musicaSelecionada = await this.api.getMusicaSelecionada(musicaEscolhida);

            Console.WriteLine($"Musica: {musicaSelecionada.Nome}");
            Console.WriteLine($"Banda: {musicaSelecionada.Banda}\n");

            Console.WriteLine(musicaSelecionada.LetraOriginal);
        }
    }
}
