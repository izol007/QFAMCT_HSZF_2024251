using NUnit.Framework;

namespace QFAMCT_HSZF_2024251.Test
{
    [TestFixture]
    public class ClientServiceTests
    {
        private List<Clients> mockClients;
        private ClientService clientService;

        [SetUp]
        public void SetUp()
        {
            mockClients = new List<Clients>();
            clientService = new ClientService(mockClients); // Mock lista átadása
        }

        [Test]
        public void AddClient_ShouldAddClient()
        {
            // Arrange
            var client = new Clients { ClientID = 1, ClientName = "John Doe", ClientAddress = "123 Main St" };

            // Act
            clientService.AddClient(client);

            // Assert
            Assert.That(mockClients.Count, Is.EqualTo(1));
            Assert.That(mockClients.First().ClientName, Is.EqualTo("John Doe"));
        }

        [Test]
        public void DeleteClient_ShouldRemoveClient()
        {
            // Arrange
            var client = new Clients { ClientID = 1, ClientName = "John Doe", ClientAddress = "123 Main St" };
            mockClients.Add(client);

            // Act
            clientService.DeleteClient(1);

            // Assert
            Assert.That(mockClients.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllClients_ShouldReturnAllClients()
        {
            // Arrange
            mockClients.Add(new Clients { ClientID = 1, ClientName = "John Doe", ClientAddress = "123 Main St" });
            mockClients.Add(new Clients { ClientID = 2, ClientName = "Jane Doe", ClientAddress = "456 Elm St" });

            // Act
            var clients = clientService.GetAllClients();

            // Assert
            Assert.That(clients.Count, Is.EqualTo(2));
        }

        [Test]
        public void UpdateClient_ShouldUpdateClientAddress()
        {
            // Arrange
            var client = new Clients { ClientID = 1, ClientName = "John Doe", ClientAddress = "123 Main St" };
            mockClients.Add(client);

            // Act
            clientService.UpdateClient(1, "789 Oak St");

            // Assert
            Assert.That(client.ClientAddress, Is.EqualTo("789 Oak St"));
        }
    }

    // Mockolt ClientService
    public class ClientService
    {
        private readonly List<Clients> clients;

        public ClientService(List<Clients> clients)
        {
            this.clients = clients;
        }

        public void AddClient(Clients client) => clients.Add(client);

        public void DeleteClient(int id)
        {
            var client = clients.FirstOrDefault(c => c.ClientID == id);
            if (client != null)
                clients.Remove(client);
        }

        public List<Clients> GetAllClients() => clients.ToList();

        public void UpdateClient(int id, string newAddress)
        {
            var client = clients.FirstOrDefault(c => c.ClientID == id);
            if (client != null)
                client.ClientAddress = newAddress;
        }
    }

    // Mockolt Clients osztály
    public class Clients
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }

        public ICollection<Measurements> Measurements { get; set; } = new HashSet<Measurements>();
    }

    [TestFixture]
    public class MeasurementServiceTests
    {
        private List<Clients> mockClients;
        private MeasurementService measurementService;

        [SetUp]
        public void SetUp()
        {
            mockClients = new List<Clients>();
            measurementService = new MeasurementService(mockClients); // Mock lista átadása
        }

        [Test]
        public void AddMeasurement_ShouldAddMeasurementToClient()
        {
            // Arrange
            var client = new Clients { ClientID = 1, ClientName = "Test Client", Measurements = new List<Measurements>() };
            mockClients.Add(client);
            var measurement = new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 100.5, MeterCount = 10, GovernmentAid = true };

            // Act
            measurementService.AddMeasurement(1, measurement);

            // Assert
            Assert.That(client.Measurements.Count, Is.EqualTo(1));
            Assert.That(client.Measurements.First().Consumption, Is.EqualTo(100.5));
        }

        [Test]
        public void AddMeasurement_ShouldThrowExceptionIfClientNotFound()
        {
            // Arrange
            var measurement = new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 100.5, MeterCount = 10, GovernmentAid = true };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => measurementService.AddMeasurement(99, measurement));
            Assert.That(ex.Message, Is.EqualTo("Nincs ilyen ügyfél ezzel az ID-vel: 99"));
        }

        [Test]
        public void UpdateMeasurement_ShouldUpdateExistingMeasurement()
        {
            // Arrange
            var client = new Clients { ClientID = 1, Measurements = new List<Measurements>() };
            var measurement = new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 100.5, MeterCount = 10, GovernmentAid = true };
            client.Measurements.Add(measurement);
            mockClients.Add(client);

            var updatedMeasurement = new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 200.75, MeterCount = 20, GovernmentAid = false };

            // Act
            measurementService.UpdateMeasurement(101, updatedMeasurement);

            // Assert
            Assert.That(measurement.Consumption, Is.EqualTo(200.75));
            Assert.That(measurement.MeterCount, Is.EqualTo(20));
        }

        [Test]
        public void DeleteMeasurement_ShouldRemoveMeasurementFromClient()
        {
            // Arrange
            var client = new Clients { ClientID = 1, Measurements = new List<Measurements>() };
            var measurement = new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 100.5, MeterCount = 10, GovernmentAid = true };
            client.Measurements.Add(measurement);
            mockClients.Add(client);

            // Act
            measurementService.DeleteMeasurement(101);

            // Assert
            Assert.That(client.Measurements.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetMeasurementsByClientId_ShouldReturnAllMeasurementsForClient()
        {
            // Arrange
            var client = new Clients
            {
                ClientID = 1,
                Measurements = new List<Measurements>
                {
                    new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 100.5, MeterCount = 10, GovernmentAid = true },
                    new Measurements { MeasurementID = 102, Date = DateTime.Now, Consumption = 200.75, MeterCount = 20, GovernmentAid = false }
                }
            };
            mockClients.Add(client);

            // Act
            var measurements = measurementService.GetMeasurementsByClientId(1);

            // Assert
            Assert.That(measurements.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetTotalConsumptionByClientId_ShouldReturnCorrectSum()
        {
            // Arrange
            var client = new Clients
            {
                ClientID = 1,
                Measurements = new List<Measurements>
                {
                    new Measurements { MeasurementID = 101, Date = DateTime.Now, Consumption = 100.5, MeterCount = 10, GovernmentAid = true },
                    new Measurements { MeasurementID = 102, Date = DateTime.Now, Consumption = 200.75, MeterCount = 20, GovernmentAid = false },
                    new Measurements { MeasurementID = 103, Date = DateTime.Now, Consumption = 50.25, MeterCount = 5, GovernmentAid = true }
                }
            };
            mockClients.Add(client);

            // Act
            var totalConsumption = measurementService.GetTotalConsumptionByClientId(1);

            // Assert
            Assert.That(totalConsumption, Is.EqualTo(351.5)); // 100.5 + 200.75 + 50.25
        }
    }

    // Mockolt MeasurementService
    public class MeasurementService
    {
        private readonly List<Clients> clients;

        public MeasurementService(List<Clients> clients)
        {
            this.clients = clients;
        }

        public void AddMeasurement(int clientId, Measurements measurement)
        {
            var client = clients.FirstOrDefault(c => c.ClientID == clientId);
            if (client == null) throw new Exception($"Nincs ilyen ügyfél ezzel az ID-vel: {clientId}");
            client.Measurements.Add(measurement);
        }

        public void UpdateMeasurement(int measurementId, Measurements measurement)
        {
            var existingMeasurement = clients
                .SelectMany(c => c.Measurements)
                .FirstOrDefault(m => m.MeasurementID == measurementId);

            if (existingMeasurement == null) throw new Exception($"Measurement with ID {measurementId} not found.");

            existingMeasurement.Consumption = measurement.Consumption;
            existingMeasurement.MeterCount = measurement.MeterCount;
            existingMeasurement.Date = measurement.Date;
            existingMeasurement.GovernmentAid = measurement.GovernmentAid;
        }

        public void DeleteMeasurement(int measurementId)
        {
            foreach (var client in clients)
            {
                var measurement = client.Measurements.FirstOrDefault(m => m.MeasurementID == measurementId);
                if (measurement != null)
                if (measurement != null)
                {
                    client.Measurements.Remove(measurement);
                    break;
                }
            }
        }

        public HashSet<Measurements> GetMeasurementsByClientId(int clientId)
        {
            var client = clients.FirstOrDefault(c => c.ClientID == clientId);
            return client?.Measurements.ToHashSet() ?? new HashSet<Measurements>();
        }

        public double GetTotalConsumptionByClientId(int clientId)
        {
            var client = clients.FirstOrDefault(c => c.ClientID == clientId);
            if (client == null) throw new Exception($"Nincs ilyen ügyfél ezzel az ID-vel: {clientId}");

            return client.Measurements.Sum(m => m.Consumption);
        }
    }

    // Mockolt Measurements osztály
    public class Measurements
    {
        public int ClientID { get; set; }
        public int MeasurementID { get; set; }
        public DateTime Date { get; set; }
        public double Consumption { get; set; }
        public int MeterCount { get; set; }
        public bool GovernmentAid { get; set; }
    }
}