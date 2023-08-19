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

        public MenuPrincipal(IJsonFileService jsonFileService)
        {
            this._jsonFileService = jsonFileService;
        }

        public override async Task Exibir()
        {
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

        public async Task GetOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("Selecione uma opção: ");
                string? input = Console.ReadLine();

                bool conseguiuConverter = int.TryParse(input, out int opcaoSelecionada);

                if (conseguiuConverter && Enumerable.Range(0, 3).Contains(opcaoSelecionada))
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
                    tela = new BuscarMusica(this._jsonFileService);
                    break;

                case 2:
                    tela = new LetrasSalvas(this._jsonFileService);
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