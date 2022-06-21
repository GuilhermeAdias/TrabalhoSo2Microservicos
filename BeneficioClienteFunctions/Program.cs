using Microsoft.Extensions.Hosting;

namespace BeneficioClienteFunctions
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}