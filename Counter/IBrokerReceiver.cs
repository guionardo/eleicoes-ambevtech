using Azure.Messaging.ServiceBus;
using SharedResources.Domain.Models;

namespace Counter
{
    internal interface IBrokerReceiver
    {
        /// <summary>
        /// Retorna uma tupla com o JSON da mensagem e o nome da entidade e a mensagem para posterior complete ou deadletter
        /// </summary>
        /// <returns></returns>
        Task<(string, TipoMensagemVotacao, ServiceBusReceivedMessage?)> ReceiveMessageAsync(CancellationToken cancellationToken);

        Task CompleteMessageAsync(ServiceBusReceivedMessage message);
        Task DeadLetterMessageAsync(ServiceBusReceivedMessage message);
        Task AbandonMessageAsync(ServiceBusReceivedMessage? message);
    }
}
