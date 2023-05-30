using SharedResources.Domain.Models;

namespace SharedResources.Repositories;

public interface IElectionRepository
{
    Task<Votacao> GetElectionAsync(int idEleicao);
    Task SaveElectionAsync(Votacao votacao);
    Task SaveVoteAsync(Voto voto);

    Task<IEnumerable<Voto>> GetApuracaoAsync(int idEleicao);
    Task SaveCountAsync(Apuracao apuracao);
}
