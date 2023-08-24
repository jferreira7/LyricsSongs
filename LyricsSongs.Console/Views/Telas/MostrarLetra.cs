namespace LyricsSongs.Console.Views
{
    using LyricsSongs.Console.Models;
    using LyricsSongs.Console.Services;
    using System;

    internal class MostrarLetra : View
    {
        private bool _exibindoTraducao;
        private Musica _musicaSelecionada;
        private IJsonFileService _jsonFileService;

        public MostrarLetra()
        {
            this._jsonFileService = ServiceProviderFactory.GetService<IJsonFileService>();
            this._musicaSelecionada = new();
            this._exibindoTraducao = false;
        }

        public async Task mostrarLetraOriginalMusicaSelecionada()
        {
            LimparConsole();

            Console.WriteLine($"Musica: {this._musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {this._musicaSelecionada.Banda}\n");

            Console.WriteLine(this._musicaSelecionada.LetraOriginal);

            this._exibindoTraducao = false;

            await getOpcaoSubMenu();
        }

        public async Task mostrarLetraOriginalMusicaSelecionada(int musicaEscolhida)
        {
            this._musicaSelecionada = await (ServiceProviderFactory.GetService<IApiVagalumeService>()).getMusicaSelecionada(musicaEscolhida);

            LimparConsole();

            Console.WriteLine($"Musica: {this._musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {this._musicaSelecionada.Banda}\n");

            Console.WriteLine(this._musicaSelecionada.LetraOriginal);

            _exibindoTraducao = false;

            await getOpcaoSubMenu();
        }

        public async Task mostrarLetraOriginalMusicaSelecionada(Musica musica)
        {
            this._musicaSelecionada = musica;

            LimparConsole();

            Console.WriteLine($"Musica: {this._musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {this._musicaSelecionada.Banda}\n");

            Console.WriteLine(this._musicaSelecionada.LetraOriginal);

            this._exibindoTraducao = false;

            await getOpcaoSubMenu();
        }

        public async Task mostrarTraducaoMusicaSelecionada()
        {
            LimparConsole();

            Console.WriteLine($"Musica: {this._musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {this._musicaSelecionada.Banda}\n");

            if (this._musicaSelecionada.LetraTraduzida != null)
                Console.WriteLine(this._musicaSelecionada.LetraTraduzida);
            else
                Console.WriteLine("Tradução não disponível.");

            this._exibindoTraducao = true;

            await getOpcaoSubMenu();
        }

        public async Task getOpcaoSubMenu()
        {
            bool respostaInvalida = true;
            do
            {
                ExibirSubMenu();
                Console.Write("Selecione uma das opções acima: ");

                bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int opcaoSelecionada);

                if (conseguiuConverter)
                {
                    if (opcaoSelecionada == 1 && !this._exibindoTraducao)
                        await mostrarTraducaoMusicaSelecionada();
                    else if (opcaoSelecionada == 1 && this._exibindoTraducao)
                        await mostrarLetraOriginalMusicaSelecionada();
                    else if (opcaoSelecionada == 2 && !this._musicaSelecionada.IsMusicaFavorita)
                        await salvarMusicaFavoritos();
                    else if (opcaoSelecionada == 2 && this._musicaSelecionada.IsMusicaFavorita)
                        await removerMusicaFavoritos();
                    else if (opcaoSelecionada == 0)
                        await VoltarMenuPrincipal();
                    else
                        ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
                else
                {
                    ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        private async Task removerMusicaFavoritos()
        {
            await this._jsonFileService.Remover(this._musicaSelecionada);
            this._musicaSelecionada.IsMusicaFavorita = false;
            await this.mostrarLetraOriginalMusicaSelecionada();
        }

        public void ExibirSubMenu()
        {
            string textoVersaoLetra = (this._exibindoTraducao) ? "Mostrar original" : "Traduzir música";
            string textoSalvarOuDeletar = (this._musicaSelecionada.IsMusicaFavorita) ? "Remover música dos favoritos" : "Salvar música nos favoritos";

            Console.WriteLine("\n----------------------------");
            Console.WriteLine($"1 - {textoVersaoLetra}");
            Console.WriteLine($"2 - {textoSalvarOuDeletar}");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.WriteLine("----------------------------");
        }

        public async Task salvarMusicaFavoritos()
        {
            await this._jsonFileService.Adicionar(_musicaSelecionada);

            if (_exibindoTraducao == true)
                await mostrarTraducaoMusicaSelecionada();
            else
                await mostrarLetraOriginalMusicaSelecionada();
        }
    }
}