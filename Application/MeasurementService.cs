using QFAMCT_HSZF_2024251.Model;
using QFAMCT_HSZF_2024251.Persistence.MsSql;
using System.Reflection;

namespace QFAMCT_HSZF_2024251.Application
{
    public interface IMeasurementService
    {
        public int Count { get; }
        bool Exists(int id);
        void AddMeasurement(Measurement measurement);
        Measurement FindMeasurementById(int id);
        IEnumerable<Measurement> GetAllMeasurements();
        void ModifyMeasurement(int MeasurementId, Measurement measurement);
        void RemoveMeasurement(int id);
        static public IEnumerable<PropertyInfo> MeasurementProperties { get; }

    }
    public class MeasurementService : IMeasurementService
    {
        private readonly MeasurementDataProvider measurementDataProvider;

        public MeasurementService(MeasurementDataProvider measurementDataProvider)
        {
            this.measurementDataProvider = measurementDataProvider;
        }

        public int Count => measurementDataProvider.Count;

        static public IEnumerable<PropertyInfo> MeasurementProperties => typeof(Measurement).GetProperties().Where(x=>!Attribute.IsDefined(x,typeof(HiddenPropertyAttribute)));

        public void AddMeasurement(Measurement measurement)
        {
            measurementDataProvider.Add(measurement);
        }

        public bool Exists(int id)
        {
            return measurementDataProvider.Exists(id);
        }

        public Measurement FindMeasurementById(int id)
        {
            return measurementDataProvider.GetById(id);
        }

        public IEnumerable<Measurement> GetAllMeasurements()
        {
            return measurementDataProvider.GetAll();
        }

        public void ModifyMeasurement(int MeasurementId, Measurement measurement)
        {
            measurementDataProvider.Update(MeasurementId,measurement);
        }

        public void RemoveMeasurement(int id)
        {
            measurementDataProvider.DeleteById(id);
        }
    }
}
