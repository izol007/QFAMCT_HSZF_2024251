using QFAMCT_HSZF_2024251.Model;
using QFAMCT_HSZF_2024251.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QFAMCT_HSZF_2024251.Application
{
    public interface IMeasurementService
    {
        public int Count { get; }
        bool Exists(int id);
        void AddMeasurement(Measurement measurement);
        Measurement FindMeasurementById(int id);
        IEnumerable<Measurement> GetAllMeasurements();
        IEnumerable<Measurement> GetAllMeasurementsForClient(int ClientID);
        void ModifyMeasurement(int MeasurementId, Measurement measurement);
        void RemoveMeasurement(int id);
        void ImportMeasurement(string path);
        static public PropertyInfo[] Properties { get; }

    }
    internal class MeasurementService : IMeasurementService
    {
        private readonly MeasurementDataProvider measurementDataProvider;

        public MeasurementService(MeasurementDataProvider measurementDataProvider)
        {
            this.measurementDataProvider = measurementDataProvider;
        }

        public int Count => measurementDataProvider.Count;

        static public PropertyInfo[] Properties => typeof(Measurement).GetProperties();

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

        public IEnumerable<Measurement> GetAllMeasurementsForClient(int ClientID)
        {
            throw new NotImplementedException();
        }

        public void ImportMeasurement(string path)
        {
            throw new NotImplementedException();
        }

        public void ModifyMeasurement(int MeasurementId, Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public void RemoveMeasurement(int id)
        {
            throw new NotImplementedException();
        }
    }
}
