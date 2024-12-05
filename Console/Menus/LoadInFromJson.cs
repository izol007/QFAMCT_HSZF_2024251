using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class LoadInFromJson : Menu
    {
        public string[] Options { get; set; }
        public Menu NextMenu { get; set; }

        public LoadInFromJson()
        {
            System.Console.WriteLine("Greetings, fellow REZSI enjoyer!\nName your business!\n");
            Options = new string[] { "Add name of JSON file", "Back" };
            SelectOption();
        }
        protected override void SelectOption()
        {
            SelectedOption = LoadOptions(Options,3);
            switch (SelectedOption)
            {
                case 0:
                    break;
                case 1:
                    new MainMenu();
                    break;
            }
        }
    }
}
