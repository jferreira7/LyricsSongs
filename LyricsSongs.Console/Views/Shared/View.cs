﻿using LyricsSongs.Console.Menus;

namespace LyricsSongs.Console.Views
{
    using System;

    public class View
    {
        public virtual Task Exibir()
        {
            LimparConsole();
            return Task.CompletedTask;
        }

        public void ExibirTituloMenu(string titulo)
        {
            char simbolo = '*';
            int qntCaracteres = titulo.Length;
            string bordasBottonTop = string.Empty.PadLeft(qntCaracteres + 6, simbolo);
            string bordasLeftRight = string.Empty.PadLeft(2, simbolo);
            Console.WriteLine(bordasBottonTop);
            Console.WriteLine($"{bordasLeftRight} {titulo} {bordasLeftRight}");
            Console.WriteLine(bordasBottonTop + "\n");
        }

        public async Task VoltarMenuPrincipal()
        {
            MenuPrincipal menu = new MenuPrincipal();
            await menu.Exibir();
        }

        public void ExibirMensagemErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{mensagem}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void EncerrarPrograma()
        {
            Environment.Exit(0);
        }

        public void LimparConsole()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
        }
    }
}