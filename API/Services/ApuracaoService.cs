using SharedResources.Domain.Models;
using SharedResources.Repositories;

namespace API.Services
{
    public class ApuracaoService : IApuracaoService
    {
        private readonly IElectionRepository _repository;

        public ApuracaoService(IElectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<DetalheApuracao> GetDetalheApuracao(int votacaoId)
        {
            var votacao = await _repository.GetElectionAsync(votacaoId);
            var votos = await _repository.GetApuracaoAsync(votacaoId);

            var apuracao = new DetalheApuracao
            {
                IdVotacao = votacaoId,
                Encerrada = votacao.Encerrada
            };
            apuracao.Contagem["brancos"] = 0;
            apuracao.Contagem["nulos"] = 0;
            foreach (var candidato in votacao.Candidatos)
            {
                apuracao.Contagem[candidato.ToString()] = 0;
            }

            var votantes = new HashSet<int>();

            foreach (var voto in votos)
            {
                // Voto em branco = idCandidato = 0
                if (voto.IdCandidato == 0)
                {
                    apuracao.Contagem["brancos"] += 1;
                    continue;
                }
                // Voto nulo = candidato não encontrado
                var candidato = votacao.Candidatos.FirstOrDefault(c => c.Id == voto.IdCandidato);
                if (candidato is null)
                {
                    apuracao.Contagem["nulos"] += 1;
                    continue;
                }

                // Se o eleitor já votou anteriormente, o voto deve ser nulo
                if (votantes.Contains(voto.IdEleitor))
                {
                    apuracao.Contagem["nulos"] += 1;
                    continue;
                }
                // Voto válido
                apuracao.Contagem[candidato.ToString()] += 1;

                votantes.Add(voto.IdEleitor);
            }
            return apuracao;

        }
    }
}
