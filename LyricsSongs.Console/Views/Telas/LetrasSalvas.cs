using LyricsSongs.Console.Services;

namespace LyricsSongs.Console.Views
{
    using LyricsSongs.Console.Models;
    using System;

    internal class LetrasSalvas : View
    {
        private IJsonFileService _jsonFileService;
        private List<Musica> _musicasSalvas;

        public LetrasSalvas(IJsonFileService jsonFileService)
        {
            this._jsonFileService = jsonFileService;
            this._musicasSalvas = jsonFileService.musicasFavoritasSalvas;
        }

        public override async Task Exibir()
        {
            await base.Exibir();
            ExibirTituloMenu("LETRAS SALVAS");

            for (int i = 0; i < this._musicasSalvas.Count; i++)
                Console.WriteLine($"{i + 1} - {this._musicasSalvas[i].Nome} - {this._musicasSalvas[i].Banda}");

            await getOpcaoSubMenu();
        }

        public async Task getOpcaoSubMenu()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("\nSelecione uma das músicas acima (ou 0 p/ voltar): ");

                bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int opcaoSelecionada);

                if (conseguiuConverter && Enumerable.Range(0, this._musicasSalvas.Count + 1).Contains(opcaoSelecionada))
                {
                    if (opcaoSelecionada == 0)
                        await VoltarMenuPrincipal(this._jsonFileService);
                    else
                        await (new MostrarLetra(this._jsonFileService)).mostrarLetraOriginalMusicaSelecionada(this._musicasSalvas[opcaoSelecionada - 1]);
                }
                else
                {
                    ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        public void ExibirSubMenu()
        {
            Console.WriteLine("\n----------------------------");
            Console.WriteLine("1 - Traduzir música");
            Console.WriteLine("2 - Salvar música");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.WriteLine("----------------------------");
        }
    }
}