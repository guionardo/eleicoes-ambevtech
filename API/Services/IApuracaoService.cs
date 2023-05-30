using SharedResources.Domain.Models;

namespace API.Services
{
    public interface IApuracaoService
    {
        Task<DetalheApuracao> GetDetalheApuracao(int votacaoId);
    }
}
