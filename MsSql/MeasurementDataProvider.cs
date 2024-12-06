using QFAMCT_HSZF_2024251.Model;
namespace QFAMCT_HSZF_2024251.MsSql
{
    public interface IMeasurementDataProvider
    {
        public int Count { get; }
        bool Exists(int id);
        void Add(Measurement measurement);
        void Update(int id, Measurement measurement);
        void DeleteById(int id);
        Measurement GetById(int id);
        IEnumerable<Measurement> GetAll();
        IEnumerable<Measurement> GetAllForClient(int ClientID);
    }
    public class MeasurementDataProvider : IMeasurementDataProvider
    {
        public int Count { get { return context.Measurements.Count(); } }
        private readonly AppDbContext context;

        public MeasurementDataProvider(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Measurement measurement)
        {
            context.Measurements.Add(measurement);
        }

        public void DeleteById(int id)
        {
            context.Measurements.Remove(context.Measurements.Find(id));
        }

        public bool Exists(int id)
        {
            return context.Measurements.Find(id) == null;
        }

        public IEnumerable<Measurement> GetAll()
        {
            return context.Measurements;
        }

        public Measurement GetById(int id)
        {
            return context.Measurements.Find(id);
        }

        public void Update(int id, Measurement measurement)
        {
            context.Measurements.Remove(context.Measurements.Find(id));
            Measurement newMeasurement = measurement;
            newMeasurement.MeasurementID = id;
            context.Measurements.Add(newMeasurement);
        }

        public IEnumerable<Measurement> GetAllForClient(int ClientID)
        {
            Client? ThisClient = context.Clients.Find(ClientID);
            if (ThisClient == null)
                throw new Exception("Client not found");
            else
                return ThisClient.Measurements;
        }
    }
}
