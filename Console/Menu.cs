using QFAMCT_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console
{
    internal abstract class Menu
    {
        protected int optionsStartIndex = 0;
        internal bool Exit { get; set; }
        protected Menu()
        {
            System.Console.Clear();
        }
        protected string[] Options { get; set; }
        protected int SelectedOption
        {
            get { return LoadOptions(Options, optionsStartIndex); }
            set { value = 0; }
        }
        protected abstract void Next();
        protected static int LoadOptions(string[] options, int startRow = 0)
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
                    System.Console.ForegroundColor = currentOption(i) ? ConsoleColor.Black : ConsoleColor.White;
                    System.Console.BackgroundColor = currentOption(i) ? ConsoleColor.White : ConsoleColor.Black;
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
