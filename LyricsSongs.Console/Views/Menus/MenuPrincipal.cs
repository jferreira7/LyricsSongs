namespace LyricsSongs.Console.Menus
{
    using LyricsSongs.Console.Services;
    using LyricsSongs.Console.Views;
    using System;

    internal class MenuPrincipal : View
    {
        private string logo = @"
██╗░░░░░██╗░░░██╗██████╗░██╗░█████╗░░██████╗   ░██████╗░█████╗░███╗░░██╗░██████╗░░██████╗
██║░░░░░╚██╗░██╔╝██╔══██╗██║██╔══██╗██╔════╝   ██╔════╝██╔══██╗████╗░██║██╔════╝░██╔════╝
██║░░░░░░╚████╔╝░██████╔╝██║██║░░╚═╝╚█████╗░   ╚█████╗░██║░░██║██╔██╗██║██║░░██╗░╚█████╗░
██║░░░░░░░╚██╔╝░░██╔══██╗██║██║░░██╗░╚═══██╗   ░╚═══██╗██║░░██║██║╚████║██║░░╚██╗░╚═══██╗
███████╗░░░██║░░░██║░░██║██║╚█████╔╝██████╔╝   ██████╔╝╚█████╔╝██║░╚███║╚██████╔╝██████╔╝
╚══════╝░░░╚═╝░░░╚═╝░░╚═╝╚═╝░╚════╝░╚═════╝░   ╚═════╝░░╚════╝░╚═╝░░╚══╝░╚═════╝░╚═════╝░";

        private IJsonFileService _jsonFileService;
        private int _quantidadeOpcoesMenu;

        public MenuPrincipal()
        {
            this._jsonFileService = ServiceProviderFactory.GetService<IJsonFileService>();
            this._quantidadeOpcoesMenu = 3;
        }

        public override async Task Exibir()
        {
            await this.getMusicasSalvas();

            Console.Clear();
            Console.WriteLine(logo);

            Console.WriteLine("");
            Console.WriteLine("1 - Buscar música");
            Console.WriteLine("2 - Letras salvas");
            Console.WriteLine("3 - Configurações");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("");

            await this.GetOpcaoSelecionada();
        }

        public async Task getMusicasSalvas()
        {
            if (this._jsonFileService.musicasFavoritasSalvas.Count != 0) return;

            Console.WriteLine("Carregando músicas salvas...");
            await this._jsonFileService.GetMusicasDoArquivoJson();
        }

        public async Task GetOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("Selecione uma opção: ");
                string? input = Console.ReadLine();

                bool conseguiuConverter = int.TryParse(input, out int opcaoSelecionada);

                if (conseguiuConverter && Enumerable.Range(0, this._quantidadeOpcoesMenu + 1).Contains(opcaoSelecionada))
                {
                    await this.MostrarMenuSelecionado(opcaoSelecionada);
                    respostaInvalida = false;
                }
                else
                {
                    base.ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        public async Task MostrarMenuSelecionado(int opcaoSelecionada)
        {
            View tela = new();

            switch (opcaoSelecionada)
            {
                case 1:
                    tela = new BuscarMusica();
                    break;

                case 2:
                    tela = new LetrasSalvas();
                    break;

                case 0:
                    base.EncerrarPrograma();
                    break;

                default:
                    base.ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                    break;
            }

            if (tela != null)
                await tela.Exibir();
        }
    }
}