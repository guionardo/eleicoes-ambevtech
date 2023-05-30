using API.Services;
using Moq;
using SharedResources.Domain.Models;
using SharedResources.Repositories;

namespace Testes.API
{
    public class ApuracaoServiceTest
    {
        [Fact]
        public async Task GetDetalheApuracaoShouldReturnCorrectValue()
        {
            List<Candidato> candidatos = new List<Candidato>()
            {
                new Candidato(1,"João"),
                new Candidato(2,"Maria"),
                new Candidato(3,"José")
            };
            List<Eleitor> eleitores = new List<Eleitor>()
            {
                new Eleitor(1,"Carlos"),
                new Eleitor(2,"Joana"),
                new Eleitor(3,"Maria"),
                new Eleitor(5,"Francisca")
            };
            Votacao votacao = new Votacao(1, "Eleição teste", candidatos, eleitores);
            List<Voto> votos = new List<Voto>()
            {
                new Voto(1,1,1),
                new Voto(1,2,1),
                new Voto(1,3,3),
                new Voto(1,4,4),
                new Voto(1,3,2),
                new Voto(1,5,0),
            };
            Mock<IElectionRepository> repositoryMock = new Mock<IElectionRepository>();
            _ = repositoryMock.Setup(r => r.GetElectionAsync(It.IsAny<int>())).ReturnsAsync(votacao);
            _ = repositoryMock.Setup(r => r.GetApuracaoAsync(It.IsAny<int>())).ReturnsAsync(votos);

            ApuracaoService service = new ApuracaoService(repositoryMock.Object);

            DetalheApuracao detalhes = await service.GetDetalheApuracao(1);

            Assert.Equal(2, detalhes.Contagem["1 João"]);
            Assert.Equal(0, detalhes.Contagem["2 Maria"]);
            Assert.Equal(1, detalhes.Contagem["3 José"]);
            Assert.Equal(1, detalhes.Contagem["brancos"]);
            Assert.Equal(2, detalhes.Contagem["nulos"]);
        }
    }
}
