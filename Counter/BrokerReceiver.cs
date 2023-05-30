using Azure.Messaging.ServiceBus;
using SharedResources.Configuracao;
using SharedResources.Domain;
using SharedResources.Domain.Models;

namespace Counter
{
    public class BrokerReceiver : IBrokerReceiver
    {
        private readonly ILogger<BrokerReceiver> _logger;
        private readonly ServiceBusClient _brokerClient;
        private readonly ServiceBusReceiver _receiver;

        public BrokerReceiver(ILogger<BrokerReceiver> logger, Configuracao configuracao)
        {
            _logger = logger;
            _brokerClient = new ServiceBusClient(configuracao.BrokerConnectionString);
            // TODO: Desacoplar o ServiceBusClient, recebendo-o por composição
            _receiver = _brokerClient.CreateReceiver(configuracao.BrokerQueue);
        }

        public async Task CompleteMessageAsync(ServiceBusReceivedMessage? message)
        {
            await _receiver.CompleteMessageAsync(message);
        }

        public async Task DeadLetterMessageAsync(ServiceBusReceivedMessage? message)
        {
            await _receiver.DeadLetterMessageAsync(message);
        }

        public async Task<(string, TipoMensagemVotacao, ServiceBusReceivedMessage?)> ReceiveMessageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var message = await _receiver.ReceiveMessageAsync(null, cancellationToken);
                if (message is null)
                {
                    return (String.Empty, TipoMensagemVotacao.MensagemInvalida, null);
                }

                var tipo = TipoMensagemVotacao.MensagemInvalida;

                if (message.ApplicationProperties.TryGetValue("entity", out var entity))
                    switch (entity.ToString())
                    {
                        case Consts.TipoMensagemInicio:
                            tipo = TipoMensagemVotacao.Inicio;
                            break;
                        case Consts.TipoMensagemVoto:
                            tipo = TipoMensagemVotacao.Voto;
                            break;
                        case Consts.TipoMensagemFim:
                            tipo = TipoMensagemVotacao.Fim;
                            break;
                    }


                var corpo = message.Body.ToString();

                if (tipo == TipoMensagemVotacao.MensagemInvalida)
                {
                    // Mensagem veio sem o header de entity
                    // Deve ser descartada
                    await _receiver.DeadLetterMessageAsync(message, cancellationToken: cancellationToken);
                }

                return (corpo, tipo, message);

            }
            catch (ServiceBusException exc)
            {
                _logger.LogError("ERRO NO SERVICE BUS", exc);
            }
            catch (Exception exc)
            {
                _logger.LogError("ERRO INESPERADO", exc);
            }
            return ("", TipoMensagemVotacao.MensagemInvalida, null);
        }

        public async Task AbandonMessageAsync(ServiceBusReceivedMessage? message)
        {
            await _receiver.AbandonMessageAsync(message);
        }
    }
}
