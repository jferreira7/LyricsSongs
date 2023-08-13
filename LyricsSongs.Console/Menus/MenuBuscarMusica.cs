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

        public override async Task Exibir()
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
            Console.WriteLine("\nMúsicas encontradas: ");
            foreach (var item in this.musicasEncontradas)
                Console.WriteLine($"{item.Key} - {item.Value.Nome} - {item.Value.Banda}");


            bool respostaInvalida = true;
            do
            {
                Console.Write("\nSelecione a música correta: ");
                string? input = Console.ReadLine();

                bool conseguiuConverter = int.TryParse(input, out int musicaEscolhida);

                if (conseguiuConverter && musicaEscolhida >= 0 && musicaEscolhida <= this.musicasEncontradas.Count)
                {
                    await mostrarMusicaSelecionada(musicaEscolhida);
                    respostaInvalida = false;
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número inteiro válido.");
                }
            } while (respostaInvalida);
        }

        public async Task mostrarMusicaSelecionada(int musicaEscolhida)
        {
            Musica musicaSelecionada = await this.api.getMusicaSelecionada(musicaEscolhida);

            Console.Clear();

            Console.WriteLine($"Musica: {musicaSelecionada.Nome}");
            Console.WriteLine($"Banda: {musicaSelecionada.Banda}\n");

            Console.WriteLine(musicaSelecionada.LetraOriginal);
        }
    }
}
