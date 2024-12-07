using QFAMCT_HSZF_2024251.Console.Menus;

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
                menu = new MainMenu(hosting);
            } while (!menu.Exit);
        }
    }
}
