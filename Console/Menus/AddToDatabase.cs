namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class AddToDatabase : Menu
    {
        public AddToDatabase(Hosting host) : base(host)
        {
            optionsStartIndex = 3;
            System.Console.WriteLine("Here you MUST PROVIDE the necessary informations!");
            
        }

        protected override void Next(Hosting host)
        {
        }
    }
}
