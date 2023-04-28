using Dominio.Messages;
using Dominio.Models;
using SharedResources.Repositories;
using System.Text.Json;

namespace Counter
{
    internal class CounterService : ICounterService
    {
        private readonly ILogger<CounterService> _logger;
        private readonly IBrokerReceiver _brokerReceiver;
        private readonly IElectionRepository _electionRepository;

        public CounterService(ILogger<CounterService> logger,
            IBrokerReceiver brokerReceiver,
            IElectionRepository electionRepository)
        {
            _logger = logger;
            _brokerReceiver = brokerReceiver;
            _electionRepository = electionRepository;
        }
        public async Task StartListeningAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var (corpo, tipo, message) = await _brokerReceiver.ReceiveMessageAsync(stoppingToken);
                try
                {
                    switch (tipo)
                    {
                        case TipoMensagemVotacao.Inicio:
                            await ProcessStartingVote(corpo);
                            break;
                        case TipoMensagemVotacao.Voto:
                            await ProcessVote(corpo);
                            break;
                        case TipoMensagemVotacao.Fim:
                            await ProcessEndingVote(corpo);
                            break;
                        default:
                            continue;
                    }
                    await _brokerReceiver.CompleteMessageAsync(message);

                }catch(Exception exc)
                {
                    await _brokerReceiver.DeadLetterMessageAsync(message);
                    // TODO: Implementar tratamento ta exceção do processamento quando houver problema com banco e a mensagem estiver OK
                }
            }
        }

        private async Task ProcessEndingVote(string corpo)
        {
            
            var termino = JsonSerializer.Deserialize<TerminoDaVotacao>(corpo);
            if (termino is null || termino.IdVotacao == 0)
                throw new JsonException("Término de votação inválido");

            // TODO: Implementar a validação da votão terminada
            // TODO: Implementar a persistência do término da votação            
        }

        private async Task ProcessVote(string corpo)
        {
            var voto= JsonSerializer.Deserialize<Voto>(corpo);
            if (voto is null || voto.IdEleicao == 0) 
                throw new JsonException("Voto inválido");

            // TODO: Implementar a validação do voto
            // TODO: Implementar a persistência do voto
        }

        private async Task ProcessStartingVote(string corpo)
        {
            var votacao = JsonSerializer.Deserialize<Votacao>(corpo);
            if (votacao is null || votacao.Id == 0) throw new JsonException("Votação inválida");

            // TODO: Implementar a validação da votação
            // TODO: Implementar a persistência da votação
        }
    }
}
