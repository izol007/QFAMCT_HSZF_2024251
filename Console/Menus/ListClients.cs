using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Hosting;
using QFAMCT_HSZF_2024251.Model;
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
            List<string> ListedItems = new List<string>();
            switch (SelectedOption)
            {
                case 0:
                    System.Console.Clear();
                    foreach (var item in host.clientService.GetAllClients())
                    {
                        ListedItems.Add(item.ToString());
                    }
                    Listed(ListedItems);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

            void Listed(List<string> ListedItems)
            {
                optionsStartIndex = 0;
                Options = new string[] {"Exit\n"}.Concat(ListedItems).ToArray();
                System.Console.Clear();
                int select = SelectedOption;
                if (select>0)
                {
                    select--;
                    int selectedClientId = int.Parse(ListedItems[select].Split('\n')[0].Split('\t')[0]);
                    System.Console.Clear();
                    Options = ListedItems[select].Split('\n')[0].Split('\t').Skip(1).Concat(new List<string> { "Measurements" }).ToArray();
                    Client client = new Client();
                    switch (SelectedOption)
                    {
                        case 0:
                            client.ClientAddress = System.Console.ReadLine();
                            client.ClientName = Options[1];
                            break;
                        case 1:
                            client.ClientName = System.Console.ReadLine();
                            client.ClientAddress = Options[0];
                            break;
                        case 2:
                            ListMeasurements(host.clientService.GetAllMeasurementsForClient(selectedClientId));
                            break;
                    }
                    host.clientService.ModifyClient(selectedClientId,client);
                }
            }
            void ListMeasurements(IEnumerable<Measurement> measurements)
            {
                Options = new string[] { "New Measurement" };
                System.Console.Clear();
                List<string> l = new List<string>();
                foreach (var item in measurements)
                {
                    l.Concat(item.ToString().Split('\t'));
                }
                Options.Concat(l.ToArray());
                int select = SelectedOption;
            }
        }
    }
}
