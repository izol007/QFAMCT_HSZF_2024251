using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class AlterDatabase : Menu
    {
        public AlterDatabase(Hosting host) : base(host)
        {
            optionsStartIndex = 3;
            System.Console.WriteLine();
            Options = new string[] { };
            Next(host);
        }

        protected override void Next(Hosting host)
        {
            throw new NotImplementedException();
        }
    }
}
