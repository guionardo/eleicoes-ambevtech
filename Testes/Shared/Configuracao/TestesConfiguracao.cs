namespace Testes.Shared.Configuracao;

public class TestesConfiguracao
{

    [Fact]
    public void TesteConfiguracaoValida()
    {
        Environment.SetEnvironmentVariable("BROKER_CONNECTION_STRING", "sb://");
        Environment.SetEnvironmentVariable("BROKER_QUEUE", "eleicao");
        Environment.SetEnvironmentVariable("DATABASE_CONNECTION_STRING", "mongodb://localhost");
        Environment.SetEnvironmentVariable("DATABASE_NAME", "eleicao");
        var config = new SharedResources.Configuracao.Configuracao();

    }
}

