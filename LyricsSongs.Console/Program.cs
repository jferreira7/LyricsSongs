namespace LyricsSongs.Console
{
    using LyricsSongs.Console.Menus;
    using LyricsSongs.Console.Services;
    using System;
    using System.Text;

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            ServiceProviderFactory.Initializer();

            (new MenuPrincipal()).Exibir().Wait();
        }
    }
}