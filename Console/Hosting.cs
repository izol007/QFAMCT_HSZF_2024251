using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QFAMCT_HSZF_2024251.Application;
using QFAMCT_HSZF_2024251.Persistence.MsSql;


namespace QFAMCT_HSZF_2024251.Console
{
    public class Hosting
    {
        internal IClientService clientService;
        public Hosting()
        {
            IHost host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<AppDbContext>();
                services.AddSingleton<IClientDataProvider, ClientDataProvider>();
                services.AddSingleton<IClientService, ClientService>();
            }
           ).Build();
            host.Start();
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            clientService = serviceProvider.GetRequiredService<IClientService>();
        }
    }
}
