using Azure.Messaging.ServiceBus;
using SharedResources.Configuracao;
using SharedResources.Domain.Models;
using System.Text.Json;

namespace Provider.BrokerSender
{
    internal class Sender
    {
        private ServiceBusClient _brokerClient;
        private ServiceBusSender _sender;

        public Sender(Configuracao configuracao)    //TODO: Usar composição para o service bus client ao invés de encapsular
        {
            _brokerClient = new ServiceBusClient(configuracao.BrokerConnectionString);
            _sender = _brokerClient.CreateSender(configuracao.BrokerQueue);
        }

        public async Task SendAsync(Votacao votacao) => await SendAsync(votacao, "votacao");


        public async Task SendAsync(Voto voto) => await SendAsync(voto, "voto");


        public async Task SendAsync(TerminoDaVotacao termino) => await SendAsync(termino, "termino");


        private async Task SendAsync<T>(T obj, string entity)    //TODO: Criar testes unitários com mock do client
        {
            var json = JsonSerializer.Serialize<T>(obj);
            var message = new ServiceBusMessage(json);
            message.ApplicationProperties.Add("entity", entity);
            await _sender.SendMessageAsync(message);
        }
    }
}
