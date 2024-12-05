using QFAMCT_HSZF_2024251.Model;
using QFAMCT_HSZF_2024251.Persistence.MsSql;

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
    }
    public class ClientService : IClientService
    {
        private readonly IClientDataProvider clientDataProvider;

        public ClientService(IClientDataProvider clientDataProvider)
        {
            this.clientDataProvider = clientDataProvider;
        }

        public int Count { get { return clientDataProvider.Count; } }

        public void AddClient(Client client)
        {
            clientDataProvider.Add(client);
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
            throw new NotImplementedException();
        }

        public void ModifyClient(int ClientId, Client Client)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(int id)
        {
            clientDataProvider.DeleteById(id);
        }


    }
}
