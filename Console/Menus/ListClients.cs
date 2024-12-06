using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class ListClients : Menu
    {
        public ListClients(Hosting host) : base(host)
        {
            optionsStartIndex = 3;
            System.Console.WriteLine("What do you need exactly?");
            Options = new string[] { "List them ALL!!!", "List only some of them (I will tell which)","SH!T, GO BACK!"};
            Next(host);
        }

        protected override void Next(Hosting host)
        {
            switch (SelectedOption)
            {
                case 0:
                    System.Console.Clear();
                    foreach (var item in host.clientService.GetAllClients())
                    {
                        System.Console.WriteLine(item);
                    }
                    System.Console.ReadLine();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
}
