﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QFAMCT_HSZF_2024251.Model
{
    public class Client
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientAddress { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
        public Client()
        {
            Measurements = new HashSet<Measurement>();
        }
        public override string ToString()
        {
            string clientData = $"{ClientAddress}\t\t{ClientName}";
            string measurementsData = "\n";
            foreach (var item in Measurements)
            {
                measurementsData += item.ToString() + "\n";
            }
            return clientData + "\n" + measurementsData;
        }
    }
    public class Measurement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasurementID { get; set; }

        [JsonIgnore, ForeignKey("ClientID")]
        public int ClientID { get; set; }
        public double Consumption { get; set; }
        public int MeterCount { get; set; }
        public DateTime Date { get; set; }
        public bool GovernmentAid { get; set; }
        public override string ToString()
        {
            return $"Date of measurement: {Date.ToString()}\tConsumption: {Consumption}\tMeter count: {MeterCount}\tGevernemnt Aid: {GovernmentAid}";
        }
    }
    public class ClientList
    {
        public List<Client> Clients { get; set; }
    }
}