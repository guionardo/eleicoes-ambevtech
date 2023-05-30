using SharedResources.Domain.Models;
using System.Text.Json;

namespace Testes.Dominio.Models
{
    public class TestVotacao
    {
        [Fact]
        public void InstanciarVotacao_ComDadosValidos_DevePassar()
        {
            var id = 1;
            var nome = "Eleição para CTO da AmbevTech";
            var candidatos = new List<Candidato>
            {
                new Candidato(1,"José"),
                new Candidato(2,"Maria")
            };
            var eleitores = new List<Eleitor>
            {
                new Eleitor(1,"Joana"),
                new Eleitor(2,"Juca")
            };

            var votacao = new Votacao(id, nome, candidatos, eleitores);

        }

        [InlineData(0, "", null, null)]
        [InlineData(1, "", null, null)]
        [InlineData(2, "Eleição", null, null)]
        [Theory]
        public void InstanciarVotacao_ComDadosInValidos_DeveFalhar(int id, string nome, IEnumerable<Candidato> candidatos, IEnumerable<Eleitor> eleitores)
        {

            Assert.Throws<ArgumentException>(() =>
            {
                var votacao = new Votacao(id, nome, candidatos, eleitores);
            });


        }

        [Fact]
        public void InstanciarVotacao_ComDadosInValidosEleitores_DeveFalhar()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var votacao = new Votacao(1, "Teste", new List<Candidato> { new Candidato(1, "abc") }, null);
            });
        }

        [Fact]
        public void TesteDeSerializacaoDeserializacao()
        {
            var id = 1;
            var nome = "Eleição para CTO da AmbevTech";
            var candidatos = new List<Candidato>
            {
                new Candidato(1,"José"),
                new Candidato(2,"Maria")
            };
            var eleitores = new List<Eleitor>
            {
                new Eleitor(1,"Joana"),
                new Eleitor(2,"Juca")
            };
            var vot1 = new Votacao(id, nome, candidatos, eleitores);

            var json = JsonSerializer.Serialize(vot1);

            var vot2 = JsonSerializer.Deserialize<Votacao>(json);

            Assert.Equal(vot1.Nome, vot2.Nome);
        }

    }
}
