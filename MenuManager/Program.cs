using System;
using System.Collections.Generic;
using OrderLibrary;

namespace MenuManager
{
    class Program
    {
        static void Main(string[] args)
        {

            var menuManager = new MenuManager<MenuManager.MenuItem>();

            menuManager.AddItem(1, "Nasi Goreng", 15000);
            menuManager.AddItem(2, "Mie Goreng", 12000);
            menuManager.DisplayMenu();


            Console.Read();

        }
    }
}
