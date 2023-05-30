using SharedResources.Domain.Models;

namespace Testes.Dominio.Models;
public class TestesCandidato
{
    [Fact]
    public void Instanciar_Candidato_Com_Dados_Validos_Deve_Passar()
    {
        var id = 1;
        var nome = "Ana";
 
        var candidato = new Candidato(id, nome);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "")]
    [InlineData(0, "Maria")]
    public void Instanciar_Candidato_Com_Dados_InValidos_Deve_Falhar(int id, string nome)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var candidato = new Candidato(id, nome);
        });
    }
}
