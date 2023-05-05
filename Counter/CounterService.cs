using SharedResources.Domain.Models;
using SharedResources.Exceptions;
using SharedResources.Repositories;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Counter
{
    internal class CounterService : ICounterService
    {
        private readonly ILogger<CounterService> _logger;
        private readonly IBrokerReceiver _brokerReceiver;
        private readonly IElectionRepository _electionRepository;
        private readonly ConcurrentDictionary<int, int> _currentVoteCounting;

        public CounterService(ILogger<CounterService> logger,
            IBrokerReceiver brokerReceiver,
            IElectionRepository electionRepository)
        {
            _logger = logger;
            _brokerReceiver = brokerReceiver;
            _electionRepository = electionRepository;
            _currentVoteCounting = new ConcurrentDictionary<int, int>();
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

                }
                catch (PersistenceErrorException exc)
                {
                    _logger.LogError(exc, exc.Message);
                    if (exc.DeadLetterMessage)
                    {
                        await _brokerReceiver.DeadLetterMessageAsync(message);
                    }
                    else
                    {
                        await _brokerReceiver.AbandonMessageAsync(message);
                    }
                    // DONE: Implementar tratamento ta exceção do processamento quando houver problema com banco e a mensagem estiver OK
                }
                catch (Exception exc)
                {
                    _logger.LogError(exc, exc.Message);
                    await _brokerReceiver.DeadLetterMessageAsync(message);
                }
            }
        }

        private async Task ProcessEndingVote(string corpo)
        {

            var termino = JsonSerializer.Deserialize<TerminoDaVotacao>(corpo);
            if (termino is null || termino.IdVotacao == 0)
                throw new JsonException("Término de votação inválido");

            // DONE: Implementar a validação da votão terminada
            var votacao = await _electionRepository.GetElectionAsync(termino.IdVotacao);
            if (votacao is null)
                throw new DataNotFoundException($"Eleição não encontrada [{termino.IdVotacao}]");

            // DONE: Implementar a persistência do término da votação
            await ElectionCount(termino.IdVotacao);
           
        }

        private async Task ProcessVote(string corpo)
        {
            var voto = JsonSerializer.Deserialize<Voto>(corpo);
            if (voto is null || voto.IdEleicao <= 0 || voto.IdEleitor <= 0)
                throw new JsonException("Voto inválido");

            // DONE: Implementar a validação do voto
            var votacao = await _electionRepository.GetElectionAsync(voto.IdEleicao);
            if (votacao is null)
                throw new DataNotFoundException($"Eleição não encontrada [{voto.IdEleicao}]");

            // TODO: Implementar verificação se a votação já está encerrada

            if (!votacao.Eleitores.Any(e => e.Id == voto.IdEleitor))
                throw new DataNotFoundException($"Eleitor não encontrado [{voto.IdEleitor}]");

            // DONE: Implementar a persistência do voto
            await _electionRepository.SaveVoteAsync(voto);
            _currentVoteCounting[voto.IdEleicao]++;

            // Verificar se o contador atual é candidato a apuração parcial
            if (_currentVoteCounting[voto.IdEleicao] % 20 == 0)
            {
                await ElectionCount(voto.IdEleicao);
            }
        }


        private async Task ElectionCount(int idEleicao)
        {
            var votacao = await _electionRepository.GetElectionAsync(idEleicao);
            var votos = await _electionRepository.GetApuracaoAsync(idEleicao);
            var apuracao = new Apuracao(idEleicao);
            foreach (var voto in votos)
            {
                if (votacao.Eleitores.Any(e => e.Id == voto.IdEleitor) &&
                    votacao.Candidatos.Any(c => c.Id == voto.IdCandidato))
                {
                    apuracao.Votos[voto.IdCandidato]++;
                }
                else
                {
                    apuracao.Votos[0]++;
                }
            }
            await _electionRepository.SaveCountAsync(apuracao);

        }

        private async Task ProcessStartingVote(string corpo)
        {
            _currentVoteCounting.Clear();
            var votacao = JsonSerializer.Deserialize<Votacao>(corpo);
            if (votacao is null || votacao.Id == 0) throw new JsonException("Votação inválida");

            // DONE: Implementar a validação da votação

            await _electionRepository.SaveElectionAsync(votacao);
            // DONE: Implementar a persistência da votação
        }
    }
}
