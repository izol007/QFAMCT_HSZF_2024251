using QFAMCT_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class MainMenu : Menu
    {
        public MainMenu(Hosting hosting) : base(hosting)
        {
            optionsStartIndex = 9;
            base.Options = new string[]{
                "Load in JSON files",
            "Add to the database",
            "Alter in database",
            "New Measurement to existing client",
            "List clients",
            "View Statistics",
            "Exit"
            };
            Exit = false;
            RezsiText();
            Next(hosting);
        }

        void RezsiText()
        {
            System.Console.WriteLine("RRR                       III\t|\nR  R  EEEEE  ZZZZZ   SSSS  I\t|\nR R   E         Z   S      I\t|\nRR    EEEE    zZz    SSS   I\t|\nR R   E       Z         S  I\t|\nR  R  EEEEE  ZZZZZ  SSSS  III\t|\n");

            System.Console.BackgroundColor = ConsoleColor.White;
            System.Console.ForegroundColor = ConsoleColor.Black;
            System.Console.WriteLine("                                ");
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        protected override void Next(Hosting host)
        {
            switch (SelectedOption)
            {
                case 0:
                    new LoadInFromJson(host);
                    break;
                case 1:
                    new AddToDatabase(host);
                    break;
                case 2:
                    new AlterDatabase(host);
                    break;
                case 3:
                    new NewMeasurmenetToExistingClient(host);
                    break;
                case 4:
                    new ListClients(host);
                    break;
                case 5:
                    new Statistics(host);
                    break;
                case 6:
                    Exit = true;
                    break;
                default:
                    break;
            }
        }
    }
}
