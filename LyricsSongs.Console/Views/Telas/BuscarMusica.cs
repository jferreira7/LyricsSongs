using LyricsSongs.Console.API;
using LyricsSongs.Console.Models;
using LyricsSongs.Console.Services;

namespace LyricsSongs.Console.Views
{
    using System;

    internal class BuscarMusica : View
    {
        public Dictionary<int, Musica> musicasEncontradas = new();
        private IJsonFileService _jsonFileService;

        public BuscarMusica(IJsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        public override async Task Exibir()
        {
            await base.Exibir();
            ExibirTituloMenu("BUSCAR MÚSICA");

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
                await VoltarMenuPrincipal(_jsonFileService);
                return;
            }

            await buscarMusicasParaSelecionar(buscaTexto);
        }

        public async Task buscarMusicasParaSelecionar(string buscaTexto)
        {
            musicasEncontradas = await ApiVagalume.SearchMusicas(buscaTexto);

            if (musicasEncontradas.Count == 0)
            {
                Console.WriteLine("\nNenhuma música encontrada!");
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

        private void checarSeMusicaJaEstaNosFavoritos()
        {
            //TODO: checar pelo ID se a música já não existe entre as favoritas
            throw new NotImplementedException();
        }

        public async Task getOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("\nSelecione a música correta (0 p/ voltar): ");
                string? reposta = Console.ReadLine();

                bool conseguiuConverterResposta = int.TryParse(reposta, out int musicaEscolhida);

                if (conseguiuConverterResposta && musicaEscolhida >= 0 && musicaEscolhida <= musicasEncontradas.Count)
                {
                    if (musicaEscolhida == 0)
                    {
                        await VoltarMenuPrincipal(_jsonFileService);
                        return;
                    }

                    await (new MostrarLetra(this._jsonFileService)).mostrarLetraOriginalMusicaSelecionada(musicaEscolhida);

                    respostaInvalida = false;
                }
                else
                {
                    ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }
    }
}