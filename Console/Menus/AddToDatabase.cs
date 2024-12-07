using QFAMCT_HSZF_2024251.Application;
using QFAMCT_HSZF_2024251.Model;
using System.Reflection;

namespace QFAMCT_HSZF_2024251.Console.Menus
{
    internal class AddToDatabase : Menu
    {
        public AddToDatabase(Hosting host) : base(host)
        {
            optionsStartIndex = 3;
            System.Console.WriteLine("Here you MUST PROVIDE the necessary informations!");
            Client client = new Client();
            Measurement measurement = new Measurement();
            foreach (var item in ClientService.ClientProperties)
            {
                if (item.PropertyType!=typeof(string)&& typeof(IEnumerable<Measurement>).IsAssignableFrom(item.PropertyType))
                {
                    foreach (PropertyInfo measurementProp in MeasurementService.MeasurementProperties)
                    {
                        System.Console.Write(measurementProp.Name + ":\t");
                        Type type = measurementProp.GetType();
                        if (measurementProp.PropertyType==typeof(string))
                        {
                            measurementProp.SetValue(measurement, System.Console.ReadLine());
                        }
                        else
                        {
                            MethodInfo methodInfo = measurementProp.PropertyType.GetMethod("Parse", new[] {typeof(string)});
                            try
                            {
                                measurementProp.SetValue(measurement,methodInfo.Invoke(null, new object[] { System.Console.ReadLine()}));
                            }
                            catch (Exception)
                            {

                                throw new Exception("Non-parseable type");
                            }
                        }
                    }
                    client.Measurements.Add(measurement);
                }
                else
                {
                    System.Console.Write(item.Name + ":\t");
                    item.SetValue(client, System.Console.ReadLine());
                }
            }
            host.clientService.AddClient(client);
        }

        protected override void Next(Hosting host)
        {
        }
    }
}
