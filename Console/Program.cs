using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QFAMCT_HSZF_2024251.Application;
using QFAMCT_HSZF_2024251.Console.Menus;
using QFAMCT_HSZF_2024251.MsSql;
using QFAMCT_HSZF_2024251.Persistence.MsSql;

namespace QFAMCT_HSZF_2024251.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<AppDbContext>();
                services.AddSingleton<IClientDataProvider, ClientDataProvider>();
                services.AddSingleton<IClientService, ClientService>();
                services.AddSingleton<IMeasurementDataProvider, MeasurementDataProvider>();
            }
            ).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IClientService clientService = host.Services.GetRequiredService<IClientService>();

            System.Console.Clear();
            Menu menu = new MainMenu();
        }
    }
}
