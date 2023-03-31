using Azure.Messaging.ServiceBus;
using Dominio.Messages;
using Dominio.Models;
using SharedResources.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task Send(Votacao votacao) => await Send(votacao, "votacao");


        public async Task Send(Voto voto) => await Send(voto, "voto");


        public async Task Send(TerminoDaVotacao termino) => await Send(termino, "termino");


        private async Task Send<T>(T obj, string entity)    //TODO: Criar testes unitários com mock do client
        {
            var json = JsonSerializer.Serialize<T>(obj);
            var message = new ServiceBusMessage(json);
            message.ApplicationProperties.Add("entity", entity);
            await _sender.SendMessageAsync(message);
        }
    }
}
