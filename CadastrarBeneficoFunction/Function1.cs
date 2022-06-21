using System;
using BeneficioClienteFunctions.Domain;
using CadastroClienteAPI.Repository.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CadastrarBeneficoFunction
{
    public class Function1
    {
        private IBeneficioRepository _beneficioRepository;

        public Function1(IBeneficioRepository beneficioRepository)
        {
            _beneficioRepository = beneficioRepository;
        }

        [Function("Function1")]
        public void Run([ServiceBusTrigger("fila_evento_beneficiocliente", Connection = "Endpoint=sb://filaeventocliente.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SfaHmtq2ndX3BJ04b34Yscagz+LIDG6wpe9+m7SfurU=")] string myQueueItem, FunctionContext context)
        {
            var logger = context.GetLogger("SalvarBeneficioClienteFunction");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var beneficio = JsonConvert.DeserializeObject<Beneficio>(myQueueItem);

            _beneficioRepository.CreateBeneficio(beneficio);
        }
    }
}
