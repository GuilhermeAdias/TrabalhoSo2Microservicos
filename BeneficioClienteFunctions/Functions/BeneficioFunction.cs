using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeneficioClienteFunctions.Domain;
using BeneficioClienteFunctions.Repository.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.InteropExtensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeneficioClienteFunctions.Functions
{
    public class BeneficioFunction
    {
        [Function("BeneficioFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("BeneficioFunction");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            string connectionString = "Endpoint=sb://filaeventocliente.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SfaHmtq2ndX3BJ04b34Yscagz+LIDG6wpe9+m7SfurU=";

            await EnviarMensagemAsync(connectionString, "fila_evento_beneficiocliente", ETipoEntidade.Fila, requestBody);


            return new OkObjectResult(requestBody);
        }

        public async Task EnviarMensagemAsync(string conexao, string nomeEntidade, ETipoEntidade tipoEntidade, string mensagem, Dictionary<string, object> propriedades = null)
        {
            var message = new Message(Encoding.UTF8.GetBytes(mensagem));
            if (propriedades != null)
            {
                foreach (KeyValuePair<string, object> propriedade in propriedades)
                {
                    message.UserProperties.Add(propriedade.Key, propriedade.Value);
                }
            }
            switch (tipoEntidade)
            {
                case ETipoEntidade.Fila:
                    {
                        var queueClient = new QueueClient(conexao, nomeEntidade);
                        await queueClient.SendAsync(message);
                        await queueClient.CloseAsync();
                    }
                    break;
                case ETipoEntidade.Topico:
                    {
                        var topicClient = new TopicClient(conexao, nomeEntidade);
                        await topicClient.SendAsync(message);
                        await topicClient.CloseAsync();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
