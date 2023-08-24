using LyricsSongs.Console.Models;
using LyricsSongs.Console.Services;

namespace LyricsSongs.Console.Views
{
    using System;

    internal class BuscarMusica : View
    {
        public Dictionary<int, Musica> musicasEncontradas = new();
        private IApiVagalumeService _apiVagalumeService;
        private IJsonFileService _jsonFileService;
        private bool isNenhumaMusicaEncontrada = false;

        public BuscarMusica()
        {
            this._apiVagalumeService = ServiceProviderFactory.GetService<IApiVagalumeService>();
            this._jsonFileService = ServiceProviderFactory.GetService<IJsonFileService>();
        }

        public override async Task Exibir()
        {

            if (!this.isNenhumaMusicaEncontrada)
            {
                await base.Exibir();
                ExibirTituloMenu("BUSCAR MÚSICA");
            }


            string? buscaTexto;
            do
            {
                Console.Write("Digite o nome da música (0 p/ voltar): ");
                buscaTexto = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(buscaTexto))
                    ExibirMensagemErro("Digite um texto válido ou 0 (zero) para voltar.");
                else
                    break;
            } while (true);

            if (buscaTexto == "0")
            {
                await VoltarMenuPrincipal();
                return;
            }

            await buscarMusicasParaSelecionar(buscaTexto);
        }

        public async Task buscarMusicasParaSelecionar(string buscaTexto)
        {
            musicasEncontradas = await this._apiVagalumeService.SearchMusicas(buscaTexto);

            if (musicasEncontradas.Count == 0)
            {
                Console.WriteLine("\nNenhuma música encontrada!\n");
                this.isNenhumaMusicaEncontrada = true;
                await this.Exibir();
                return;
            }

            await mostrarMusicasBuscadas();
        }

        public async Task mostrarMusicasBuscadas()
        {
            Console.WriteLine("\nMúsicas encontradas: ");
            foreach (var item in musicasEncontradas)
                Console.WriteLine($"{item.Key} - {item.Value.Nome} - {item.Value.Banda}");

            await getOpcaoSelecionada();
        }

        public async Task getOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("\nSelecione a música correta (0 p/ voltar): ");
                string? reposta = Console.ReadLine();

                bool conseguiuConverterResposta = int.TryParse(reposta, out int opcaoSelecionada);

                if (conseguiuConverterResposta && opcaoSelecionada >= 0 && opcaoSelecionada <= musicasEncontradas.Count)
                {
                    if (opcaoSelecionada == 0)
                    {
                        await VoltarMenuPrincipal();
                        return;
                    }

                    MostrarLetra mostrarLetra = new();
                    Musica? musicaSelecionada = this.getMusicaSeJaEstaNosFavoritos(opcaoSelecionada);
                    if (musicaSelecionada != null)
                        await mostrarLetra.mostrarLetraOriginalMusicaSelecionada(musicaSelecionada);
                    else
                        await mostrarLetra.mostrarLetraOriginalMusicaSelecionada(opcaoSelecionada);

                    respostaInvalida = false;
                }
                else
                {
                    ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        private Musica? getMusicaSeJaEstaNosFavoritos(int opcaoSelecionada)
        {
            /*
             * É necessário usar o Substring(1) para remover o primeiro caractere o Id das músicas buscadas, pois só assim o Id ficara igual ao Id quando é buscado a música por completo. Existe essa diferença entre os Ids.
             */
            Musica? musicaSelecionada = this._jsonFileService.musicasFavoritasSalvas.Where(musica => musica.Id == this._apiVagalumeService.MusicasBuscadas[opcaoSelecionada].Id!.Substring(1)).FirstOrDefault();

            return musicaSelecionada;
        }
    }
}