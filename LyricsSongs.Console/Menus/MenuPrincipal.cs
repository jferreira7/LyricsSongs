namespace LyricsSongs.Console.Menus
{
    using LyricsSongs.Console.Services;
    using System;

    internal class MenuPrincipal : Menu
    {
        private string logo = @"
██╗░░░░░██╗░░░██╗██████╗░██╗░█████╗░░██████╗   ░██████╗░█████╗░███╗░░██╗░██████╗░░██████╗
██║░░░░░╚██╗░██╔╝██╔══██╗██║██╔══██╗██╔════╝   ██╔════╝██╔══██╗████╗░██║██╔════╝░██╔════╝
██║░░░░░░╚████╔╝░██████╔╝██║██║░░╚═╝╚█████╗░   ╚█████╗░██║░░██║██╔██╗██║██║░░██╗░╚█████╗░
██║░░░░░░░╚██╔╝░░██╔══██╗██║██║░░██╗░╚═══██╗   ░╚═══██╗██║░░██║██║╚████║██║░░╚██╗░╚═══██╗
███████╗░░░██║░░░██║░░██║██║╚█████╔╝██████╔╝   ██████╔╝╚█████╔╝██║░╚███║╚██████╔╝██████╔╝
╚══════╝░░░╚═╝░░░╚═╝░░╚═╝╚═╝░╚════╝░╚═════╝░   ╚═════╝░░╚════╝░╚═╝░░╚══╝░╚═════╝░╚═════╝░";

        private Dictionary<int, Type> opcoes;
        private IJsonFileService _jsonFileService;

        public MenuPrincipal(IJsonFileService jsonFileService)
        {
            this._jsonFileService = jsonFileService;
            this.opcoes = new()
            {
                { 1, typeof(MenuBuscarMusica) },
                { 2, typeof(MenuLetrasSalvas) },
                { 3, typeof(MenuConfiguracoes) },
                { 0, typeof(MenuSair) }
            };
        }

        public override async Task Exibir()
        {
            await base.Exibir();
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

                if (conseguiuConverter && opcoes.ContainsKey(opcaoSelecionada))
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
            Menu? menu = null;

            switch (opcaoSelecionada)
            {
                case 1:
                    menu = new MenuBuscarMusica(this._jsonFileService);
                    break;

                case 2:
                    menu = new MenuLetrasSalvas(this._jsonFileService);
                    break;

                case 0:
                    menu = new MenuSair();
                    break;

                default:
                    break;
            }

            if (menu != null)
                await menu.Exibir();
        }
    }
}