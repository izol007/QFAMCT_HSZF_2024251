using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    public abstract class Menu
    {
        protected Menu()
        {
            System.Console.Clear();
        }
        public string[] Options { get; set; }
        public int SelectedOption { get; set; }
        public Menu NextMenu { get; set; }
        protected abstract void SelectOption();
        public static int LoadOptions(string[] options, int startRow = 0)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.White;
            int ActiveOption = 0;
            ConsoleKeyInfo menuNav;
            bool currentOption(int i) => ActiveOption % options.Length == i;
            do
            {
                System.Console.SetCursorPosition(0, startRow);
                for (int i = 0; i < options.Length; i++)
                {
                    System.Console.ForegroundColor = (currentOption(i)) ? ConsoleColor.Black : ConsoleColor.White;
                    System.Console.BackgroundColor = (currentOption(i)) ? ConsoleColor.White : ConsoleColor.Black;
                    System.Console.WriteLine(options[i]);
                }
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = ConsoleColor.White;
                menuNav = System.Console.ReadKey();
                switch (menuNav.Key)
                {
                    case ConsoleKey.UpArrow:
                        --ActiveOption;
                        if (ActiveOption < 0) ActiveOption += options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        ++ActiveOption;
                        break;
                }
            } while (!menuNav.Key.Equals(ConsoleKey.Enter));
            return ActiveOption % options.Length;
        }
    }
}
