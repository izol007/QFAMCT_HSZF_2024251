using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QFAMCT_HSZF_2024251.Application;
using QFAMCT_HSZF_2024251.MsSql;
using QFAMCT_HSZF_2024251.Persistence.MsSql;


namespace QFAMCT_HSZF_2024251.Console
{
    internal class Hosting
    {
        internal IClientService clientService;
        public Hosting()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<AppDbContext>();
                services.AddSingleton<IClientDataProvider, ClientDataProvider>();
                services.AddSingleton<IClientService, ClientService>();
                services.AddSingleton<IMeasurementDataProvider, MeasurementDataProvider>();
                services.AddSingleton<IMeasurementService, MeasurementService>();
            }
           ).Build();
            host.Start();
            using IServiceScope serviceScope = host.Services.CreateScope();
            clientService = host.Services.GetRequiredService<IClientService>();
        }
    }
}
