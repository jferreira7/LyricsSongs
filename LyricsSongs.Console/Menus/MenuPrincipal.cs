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


        public override void Exibir()
        {
            Console.WriteLine(logo);

            Console.WriteLine("");
            Console.WriteLine("1 - Buscar música");
            Console.WriteLine("2 - Letras salvas");
            Console.WriteLine("3 - Configurações");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("");
        }
    }
}
