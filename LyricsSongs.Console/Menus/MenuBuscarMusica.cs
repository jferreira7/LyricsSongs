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
            await base.Exibir();
            base.ExibirTituloMenu("BUSCAR MÚSICA");

            string? buscaTexto;
            do
            {
                Console.Write("Digite o nome da música (0 p/ voltar): ");
                buscaTexto = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(buscaTexto))
                    base.ExibirMensagemErro("Digite um texto válido ou 0 (zero) para voltar.");
                else
                    break;
            } while (true);

            if (buscaTexto == "0")
            {
                await base.VoltarMenuPrincipal();
                return;
            }

            await this.buscarMusicasParaSelecionar(buscaTexto);
        }

        public async Task buscarMusicasParaSelecionar(string buscaTexto)
        {
            this.musicasEncontradas = await this.api.SearchMusicas(buscaTexto);

            if (this.musicasEncontradas.Count == 0)
            {
                Console.WriteLine("\nNenhuma música encontrada!");
                return;
            }

            await this.mostrarMusicasBuscadas();
        }

        public async Task mostrarMusicasBuscadas()
        {
            Console.WriteLine("\nMúsicas encontradas: ");
            foreach (var item in this.musicasEncontradas)
                Console.WriteLine($"{item.Key} - {item.Value.Nome} - {item.Value.Banda}");

            await this.getOpcaoSelecionada();
        }

        public async Task getOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("\nSelecione a música correta (0 p/ voltar): ");
                string? input = Console.ReadLine();

                bool conseguiuConverter = int.TryParse(input, out int musicaEscolhida);

                if (conseguiuConverter && musicaEscolhida >= 0 && musicaEscolhida <= this.musicasEncontradas.Count)
                {
                    if (musicaEscolhida == 0)
                    {
                        await base.VoltarMenuPrincipal();
                        return;
                    }

                    await mostrarLetraOriginalMusicaSelecionada(musicaEscolhida);
                    respostaInvalida = false;
                }
                else
                {
                    base.ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        public async Task mostrarLetraOriginalMusicaSelecionada(int musicaEscolhida)
        {
            Musica musicaSelecionada = await this.api.getMusicaSelecionada(musicaEscolhida);

            Console.Clear();

            Console.WriteLine($"Musica: {musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {musicaSelecionada.Banda}\n");

            Console.WriteLine(musicaSelecionada.LetraOriginal);
        }
    }
}
