using BeneficioClienteFunctions;
using CadastroClienteAPI.Domain.Data;
using CadastroClienteAPI.Repository;
using CadastroClienteAPI.Repository.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BeneficioClienteFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<IConfiguration>(configuration);

            builder.Services.AddHttpClient();

            var sqlServerConnection = "Data Source=trabalhoso2database.cpwcuzfp8ylg.sa-east-1.rds.amazonaws.com;Initial Catalog=ClienteDB;User ID=admin;Password=09876Gui";

            builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(sqlServerConnection));

            builder.Services.AddScoped<IBeneficioRepository, BeneficioRepository>();
        }
    }
}
