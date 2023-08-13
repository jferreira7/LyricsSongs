namespace LyricsSongs.Console.Menus
{
    internal class MenuSair : Menu
    {
        public override Task Exibir()
        {
            Environment.Exit(0);
            return base.Exibir();
        }
    }
}
