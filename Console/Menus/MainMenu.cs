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
        public string[] Options { get; set; }

        public MainMenu() : base()
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
            Next();
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

        protected override void Next()
        {
            switch (SelectedOption)
            {
                case 0:
                    new LoadInFromJson();
                    break;
                case 1:
                    new AddToDatabase();
                    break;
                case 2:
                    new AlterDatabase();
                    break;
                case 3:
                    new NewMeasurmenetToExistingClient();
                    break;
                case 4:
                    new ListClients();
                    break;
                case 5:
                    new Statistics();
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
