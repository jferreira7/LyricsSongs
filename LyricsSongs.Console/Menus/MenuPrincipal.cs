namespace LyricsSongs.Console.Menus
{
    using System;

    internal class MenuPrincipal : Menu
    {
        string logo = @"
██╗░░░░░██╗░░░██╗██████╗░██╗░█████╗░░██████╗   ░██████╗░█████╗░███╗░░██╗░██████╗░░██████╗
██║░░░░░╚██╗░██╔╝██╔══██╗██║██╔══██╗██╔════╝   ██╔════╝██╔══██╗████╗░██║██╔════╝░██╔════╝
██║░░░░░░╚████╔╝░██████╔╝██║██║░░╚═╝╚█████╗░   ╚█████╗░██║░░██║██╔██╗██║██║░░██╗░╚█████╗░
██║░░░░░░░╚██╔╝░░██╔══██╗██║██║░░██╗░╚═══██╗   ░╚═══██╗██║░░██║██║╚████║██║░░╚██╗░╚═══██╗
███████╗░░░██║░░░██║░░██║██║╚█████╔╝██████╔╝   ██████╔╝╚█████╔╝██║░╚███║╚██████╔╝██████╔╝
╚══════╝░░░╚═╝░░░╚═╝░░╚═╝╚═╝░╚════╝░╚═════╝░   ╚═════╝░░╚════╝░╚═╝░░╚══╝░╚═════╝░╚═════╝░";

        Dictionary<int, Menu> opcoes;

        public MenuPrincipal()
        {
            this.opcoes = new()
            {
                { 1, new MenuBuscarMusica() },
                { 2, new MenuLetrasSalvas() },
                { 3, new MenuConfiguracoes() },
                { 0, new MenuSair() }
            };
        }

        public async override Task Exibir()
        {
            await base.Exibir();
            Console.WriteLine(logo);

            Console.WriteLine("");
            Console.WriteLine("1 - Buscar música");
            Console.WriteLine("2 - Letras salvas");
            Console.WriteLine("3 - Configurações");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("");

            await this.getOpcaoSelecionada();
        }

        public async Task getOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("Selecione uma opção: ");
                string? input = Console.ReadLine();

                bool conseguiuConverter = int.TryParse(input, out int opcaoSelecionada);

                if (conseguiuConverter && opcoes.ContainsKey(opcaoSelecionada))
                {
                    await this.mostrarMenuSelecionado(opcaoSelecionada);
                    respostaInvalida = false;
                }
                else
                {
                    base.ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        public async Task mostrarMenuSelecionado(int opcaoSelecionada)
        {
            Menu menu = opcoes[opcaoSelecionada];
            await menu.Exibir();

            Console.Write("\nPressione qualquer tecla para voltar ao menu principal...");
            Console.ReadLine();

            await base.VoltarMenuPrincipal();
        }
    }
}
