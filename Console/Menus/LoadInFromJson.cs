using QFAMCT_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class LoadInFromJson : Menu
    {
        public LoadInFromJson(Hosting host):base(host)
        {
            optionsStartIndex = 3;
            System.Console.WriteLine("Greetings, fellow REZSI enjoyer!\nName your business!\n");
            base.Options = new string[] { "Add name of JSON file", "Back" };
            Next(host);
        }
        protected override void Next(Hosting host)
        {
            switch (SelectedOption)
            {
                case 0:
                    System.Console.WriteLine("\n\nHand it over. That thing, your dark json.\n");
                    string filename = System.Console.ReadLine();
                    host.clientService.ImportClients(Path.Combine(@"..\..\..\..\", filename));
                    break;
                case 1:
                    break;
            }
        }
    }
}
