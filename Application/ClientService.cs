using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using QFAMCT_HSZF_2024251.Model;
using QFAMCT_HSZF_2024251.Persistence.MsSql;
using System.Reflection;
using System.Text.Json.Serialization;

namespace QFAMCT_HSZF_2024251.Application
{
    public interface IClientService
    {
        public int Count { get; }
        bool Exists(int id);
        void AddClient(Client Client);
        Client FindClientById(int id);
        IEnumerable<Client> GetAllClients();
        void ModifyClient(int ClientId, Client Client);
        void RemoveClient(int id);
        void ImportClients(string path);
        List<string> Statistics();
        HashSet<Measurement> GetAllMeasurementsForClient(int ClientID);
        static public IEnumerable<PropertyInfo> AllProperties { get; }
        static public IEnumerable<PropertyInfo> ClientProperties { get; }
    }
    public class ClientService : IClientService
    {
        private readonly IClientDataProvider clientDataProvider;

        public ClientService(IClientDataProvider clientDataProvider)
        {
            this.clientDataProvider = clientDataProvider;
        }

        public int Count { get { return clientDataProvider.Count; } }

        static public IEnumerable<PropertyInfo> AllProperties { 
            get 
            {
                IEnumerable<PropertyInfo> props1 = ClientProperties;
                return props1.Union(MeasurementService.MeasurementProperties);
            } 
        }

        static public IEnumerable<PropertyInfo> ClientProperties
        {
            get
            {
                return typeof(Client).GetProperties().Where(x => !Attribute.IsDefined(x, typeof(HiddenPropertyAttribute)));
            }
        }

        public void AddClient(Client client)
        {
            clientDataProvider.AddOrUpdate(client);
        }

        public bool Exists(int id)
        {
            return clientDataProvider.Exists(id);
        }

        public Client FindClientById(int id)
        {
            return clientDataProvider.GetById(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return clientDataProvider.GetAll();
        }

        public void ImportClients(string path)
        {
            AddAll(JsonConvert.DeserializeObject<ClientList>(File.ReadAllText(path+".json")).Clients);
        }

        public void ModifyClient(int ClientId, Client Client)
        {
            clientDataProvider.Update(ClientId,Client);
        }

        public void RemoveClient(int id)
        {
            clientDataProvider.DeleteById(id);
        }

        void AddAll(List<Client> clientList)
        {
            foreach (Client client in clientList)
            {
                clientDataProvider.AddOrUpdate(client);
            }
        }

        public HashSet<Measurement> GetAllMeasurementsForClient(int ClientID)
        {
            return clientDataProvider.GetById(ClientID).Measurements.ToHashSet();
        }

        public List<string> Statistics()
        {
            List<string> strings = new List<string>();
            foreach (var item in clientDataProvider.GetAll())
            {
                string toAdd = $"{item.ClientAddress}\t{item.ClientName}\nNumber of Measurements:\t{clientDataProvider.NumberOfMeasurementsForClient(item.ClientID)}\nAverage consumption:\t{clientDataProvider.AverageConsumption(item.ClientID)}\nNot supported consumptions:\n\n";
                foreach (var notSupported in clientDataProvider.MoreConsumedThanSupported(item.ClientID))
                {
                    toAdd +=notSupported.ToString()+"\n";
                }
                toAdd += "\n\n\n";
                strings.Add(toAdd);
            }
            return strings;
        }
    }
}
