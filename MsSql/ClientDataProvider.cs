using QFAMCT_HSZF_2024251.Model;

namespace QFAMCT_HSZF_2024251.Persistence.MsSql
{
    public interface IClientDataProvider
    {
        public int Count { get; }
        bool Exists(int id);
        void AddOrUpdate(Client client);
        void Update(int clientId, Client client);
        void DeleteById(int id);
        Client GetById(int id);
        IEnumerable<Client> GetAll();
        int NumberOfMeasurementsForClient(int ClientID);
        double AverageConsumption(int ClientID);
        HashSet<Measurement> MoreConsumedThanSupported(int ClientID);
        int GetID(string ClientAddress, string ClientName);

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

        public void AddOrUpdate(Client client)
        {
            var oldClient = Existing(client);
            if (oldClient != null)
                foreach (var item in client.Measurements)
                {
                    oldClient.Measurements.Add(item);
                }
            else
                context.Clients.Add(client);
            context.SaveChanges();
        }

        Client? Existing(Client client)
        {
            foreach (var item in context.Clients)
            {
                if (item.ClientAddress==client.ClientAddress&&item.ClientName==client.ClientName)
                {
                    return item;
                }
            }
            return null;
        }

        public void DeleteById(int id)
        {
            context.Remove(context.Clients.Find(id));
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


        public int NumberOfMeasurementsForClient(int ClientID)
        {
            return GetById(ClientID).Measurements.Count();
        }

        public double AverageConsumption(int ClientID)
        {
            Measurement minMeasurement = GetById(ClientID).Measurements.OrderByDescending(x => x.Consumption).Last();
            Measurement maxMeasurement = GetById(ClientID).Measurements.OrderByDescending(x => x.Consumption).First();
            return (minMeasurement.Consumption + maxMeasurement.Consumption) / 2;
        }

        public HashSet<Measurement> MoreConsumedThanSupported(int ClientID)
        {
            return GetById(ClientID).Measurements.Where(x => x.Consumption > 50).ToHashSet();
        }

        public int GetID(string ClientAddress, string ClientName)
        {
            return context.Clients.First(x=>x.ClientName==ClientName&&x.ClientAddress==ClientAddress).ClientID;
        }
    }
}
