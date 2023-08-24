namespace LyricsSongs.Console.Views.Menus
{
    using LyricsSongs.Console.Menus;
    using LyricsSongs.Console.Views.Telas;
    using System;

    public class MenuConfiguracoes : View
    {
        private int _quantidadeOpcoesMenu;

        public MenuConfiguracoes()
        {
            this._quantidadeOpcoesMenu = 2;
        }

        public override async Task Exibir()
        {
            await base.Exibir();
            ExibirTituloMenu("CONFIGURAÇÕES");

            Console.WriteLine("1 - Adicionar/Alterar Token");
            Console.WriteLine("0 - Voltar");

            await GetOpcaoSelecionada();
        }

        public async Task GetOpcaoSelecionada()
        {
            bool respostaInvalida = true;
            do
            {
                Console.Write("\nSelecione uma opção: ");
                string? input = Console.ReadLine();

                bool conseguiuConverter = int.TryParse(input, out int opcaoSelecionada);

                if (conseguiuConverter && Enumerable.Range(0, this._quantidadeOpcoesMenu + 1).Contains(opcaoSelecionada))
                {
                    await MostrarTelaSelecionada(opcaoSelecionada);
                    respostaInvalida = false;
                }
                else
                {
                    ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                }
            } while (respostaInvalida);
        }

        public async Task MostrarTelaSelecionada(int opcaoSelecionada)
        {
            View tela = new();

            switch (opcaoSelecionada)
            {
                case 1:
                    tela = new AdicionarToken();
                    break;

                case 0:
                    tela = new MenuPrincipal();
                    break;

                default:
                    ExibirMensagemErro("Opção inválida! Por favor, digite uma opção válida.");
                    break;
            }

            if (tela != null)
                await tela.Exibir();
        }
    }
}