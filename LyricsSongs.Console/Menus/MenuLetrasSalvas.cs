using LyricsSongs.Console.Services;

namespace LyricsSongs.Console.Menus
{
    using System;

    internal class MenuLetrasSalvas : Menu
    {
        private IJsonFileService _jsonFileService;

        public MenuLetrasSalvas(IJsonFileService jsonFileService)
        {
            this._jsonFileService = jsonFileService;
        }

        public override async Task Exibir()
        {
            await base.Exibir();
            base.ExibirTituloMenu("LETRAS SALVAS");

            await this.getMusicasSalvas();
            var musicas = this._jsonFileService.musicasFavoritasSalvas;
            for (int i = 0; i < musicas.Count; i++)
                Console.WriteLine($"{i + 1} - {musicas[i].Nome} - {musicas[i].Banda}");

            await this.getOpcaoSubMenu();
        }

        public async Task getMusicasSalvas()
        {
            if (this._jsonFileService.musicasFavoritasSalvas.Count == 0)
                await this._jsonFileService.GetMusicasDoArquivoJson();
        }

        public async Task getOpcaoSubMenu()
        {
            bool respostaInvalida = true;
            do
            {
                this.ExibirSubMenu();
                Console.Write("Selecione uma das opções acima: ");

                bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int opcaoSelecionada);

                if (conseguiuConverter)
                {
                    if (opcaoSelecionada == 1)
                        //await this.mostrarTraducaoMusicaSelecionada();
                        return;
                    else if (opcaoSelecionada == 2)
                        //await this.salvarMusica();
                        return;
                    else if (opcaoSelecionada == 0)
                        await base.VoltarMenuPrincipal(this._jsonFileService);
                    else
                        base.ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
                else
                {
                    base.ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
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