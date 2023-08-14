namespace LyricsSongs.Console
{
    using LyricsSongs.Console.Menus;
    using System;
    using System.Text;

    internal class Program
    {

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            MenuPrincipal menuPrincipal = new MenuPrincipal();
            menuPrincipal.Exibir().Wait();
        }
    }
}