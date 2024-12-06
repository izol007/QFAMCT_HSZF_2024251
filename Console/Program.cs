using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QFAMCT_HSZF_2024251.Application;
using QFAMCT_HSZF_2024251.Console.Menus;
using QFAMCT_HSZF_2024251.MsSql;
using QFAMCT_HSZF_2024251.Persistence.MsSql;

namespace QFAMCT_HSZF_2024251.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hosting hosting = new Hosting();
            Menu menu;
            do
            {
                menu = new MainMenu();
            } while (!menu.Exit);
        }
    }
}
