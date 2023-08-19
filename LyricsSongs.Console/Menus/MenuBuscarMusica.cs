namespace LyricsSongs.Console.Menus
{
    using LyricsSongs.Console.API;
    using LyricsSongs.Console.Models;
    using LyricsSongs.Console.Services;
    using System;
    using System.Runtime.InteropServices;

    internal class MenuBuscarMusica : Menu
    {
        public Dictionary<int, Musica> musicasEncontradas = new();
        private ApiVagalume _api;
        private Musica _musicaSelecionada;
        private IJsonFileService _jsonFileService;
        private bool _exibindoTraducao;

        public MenuBuscarMusica(IJsonFileService jsonFileService)
        {
            this._jsonFileService = jsonFileService;
            this._api = new();
            this._musicaSelecionada = new();
            this._exibindoTraducao = false;
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
                await base.VoltarMenuPrincipal(this._jsonFileService);
                return;
            }

            await this.buscarMusicasParaSelecionar(buscaTexto);
        }

        public async Task buscarMusicasParaSelecionar(string buscaTexto)
        {
            this.musicasEncontradas = await this._api.SearchMusicas(buscaTexto);

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
                string? reposta = Console.ReadLine();

                bool conseguiuConverterResposta = int.TryParse(reposta, out int musicaEscolhida);

                if (conseguiuConverterResposta && musicaEscolhida >= 0 && musicaEscolhida <= this.musicasEncontradas.Count)
                {
                    if (musicaEscolhida == 0)
                    {
                        await base.VoltarMenuPrincipal(this._jsonFileService);
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

        public async Task mostrarLetraOriginalMusicaSelecionada([Optional] int musicaEscolhida)
        {
            if (this._musicaSelecionada.Id == null && musicaEscolhida != 0)
                this._musicaSelecionada = await this._api.getMusicaSelecionada(musicaEscolhida);

            Console.Clear();

            Console.WriteLine($"Musica: {_musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {_musicaSelecionada.Banda}\n");

            Console.WriteLine(_musicaSelecionada.LetraOriginal);

            this._exibindoTraducao = false;

            await this.getOpcaoSubMenu();
        }

        public async Task mostrarTraducaoMusicaSelecionada()
        {
            Console.Clear();

            Console.WriteLine($"Musica: {_musicaSelecionada.Nome}");
            Console.WriteLine($"Cantor(a)/Banda: {_musicaSelecionada.Banda}\n");

            Console.WriteLine(_musicaSelecionada.LetraTraduzida);

            this._exibindoTraducao = true;

            await this.getOpcaoSubMenu();
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
                        await this.mostrarTraducaoMusicaSelecionada();
                    else if (opcaoSelecionada == 2)
                        await this.salvarMusica();
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

        public async Task salvarMusica()
        {
            this._jsonFileService.Adicionar(this._musicaSelecionada);

            if (this._exibindoTraducao == true)
                await this.mostrarTraducaoMusicaSelecionada();
            else
                await this.mostrarLetraOriginalMusicaSelecionada();
        }
    }
}