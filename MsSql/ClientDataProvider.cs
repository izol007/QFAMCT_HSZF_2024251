using QFAMCT_HSZF_2024251.Model;
using QFAMCT_HSZF_2024251.MsSql;

namespace QFAMCT_HSZF_2024251.Persistence.MsSql
{
    public interface IClientDataProvider
    {
        public int Count { get; }
        bool Exists(int id);
        void Add(Client client);
        void Update(int clientId, Client client);
        void DeleteById(int id);
        Client GetById(int id);
        IEnumerable<Client> GetAll();

        delegate void NewPeakHandler(Client client, int consumption); //!!!
    }
    public class ClientDataProvider : IClientDataProvider
    {
        private readonly AppDbContext context;
        public ClientDataProvider(AppDbContext context)
        {
            this.context = context;
        }

        public int Count { get { return context.Clients.Count(); } }

        public void Add(Client client)
        {
            if (context.Clients.Where(x=>x.ClientName == client.ClientName && x.ClientAddress == client.ClientAddress).Count()==0)
            {
                context.Clients.Where(x => x.ClientName == client.ClientName && x.ClientAddress == client.ClientAddress).First().Measurements.Concat(client.Measurements);
            }
            else context.Clients.Add(client);
        }

        public void DeleteById(int id)
        {
            context.Remove(context.Clients.First(x=>x.ClientID==id));
        }

        public bool Exists(int id)
        {
            return context.Clients.Any(x=>x.ClientID==id);
        }

        public IEnumerable<Client> GetAll()
        {
            return context.Clients;
        }

        public Client? GetById(int id)
        {
            return context.Clients.FirstOrDefault(x=>x.ClientID==id);
        }

        public void Update(int clientId, Client client)
        {
            context.Clients.Find(clientId).ClientName = client.ClientName;
            context.Clients.Find(clientId).ClientAddress = client.ClientAddress;
        }
    }
}
