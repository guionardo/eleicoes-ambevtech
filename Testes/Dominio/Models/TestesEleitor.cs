using SharedResources.Domain.Models;

namespace Testes.Dominio.Models;
public class TestesEleitor
{
    [Fact]
    public void Instanciar_Eleitor_Com_Dados_Validos_Deve_Passar()
    {
        var id = 1;
        var nome = "Maria da Silva";

        var eleitor = new Eleitor(id, nome);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "")]
    [InlineData(0, "Maria")]
    public void Instanciar_Eleitor_Com_Dados_InValidos_Deve_Falhar(int id, string nome)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var Eleitor = new Eleitor(id, nome);
        });
    }
}
