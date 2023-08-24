namespace LyricsSongs.Console.Views.Telas
{
    using LyricsSongs.Console.Services;
    using System;

    internal class AdicionarToken : View
    {
        public override async Task Exibir()
        {
            await base.Exibir();
            ExibirTituloMenu("ADICIONAR/ALTERAR TOKEN");

            Console.Write("Digite o novo token: ");
            string? novoToken = Console.ReadLine()?.Trim();

            if (novoToken != null && novoToken.Length > 0)
            {
                ITokenService tokenService = ServiceProviderFactory.GetService<ITokenService>();
                tokenService.DeleteStoredToken();
                tokenService.StorageToken(novoToken);

                await base.VoltarMenuPrincipal();
            }
        }
    }
}