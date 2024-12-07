using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class Statistics : Menu
    {
        public Statistics(Hosting host) : base(host)
        {
            optionsStartIndex = 3;
            System.Console.WriteLine();
            Options = new string[] { };
            Next(host);
        }

        protected override void Next(Hosting host)
        {
            List<string> writeContent = host.clientService.Statistics();
            System.Console.Write("Name the output file:\t");
            string fileName = System.Console.ReadLine();
            File.WriteAllLines(Path.Combine(@"..\..\..\..\", fileName+".txt"), writeContent);
        }
    }
}
