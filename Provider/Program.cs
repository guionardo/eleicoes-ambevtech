using Provider.BrokerSender;
using Provider.Fixtures;
using Provider.ReaderCsv;
using SharedResources.Configuracao;
using SharedResources.Domain.Models;
using System.Collections.Immutable;

public class Program
{
    const string CandidatoCsv = "candidatos.csv";
    const string EleitoresCsv = "eleitores.csv";

    public static void Main(string[] args)
    {
        Console.WriteLine("Provider!");

        if (args.Length > 1 && args[1] == "generate")
        {

            var qtdCandidatos = 15;
            if (args.Length > 2 && int.TryParse(args[2], out var qtd) && qtd > 0)
                qtdCandidatos = qtd;

            var qtdEleitores = 100;
            if (args.Length > 3 && int.TryParse(args[3], out qtd) && qtd > 0)
                qtdEleitores = qtd;

            GenerateCsvs(qtdCandidatos, qtdEleitores);
            return;
        }

        // A função deste programa é simular a coleta de votos e enviar para a fila de processamento

        // 1. Início da votação
        // 2. Coleta de votos
        // 3. Fim da votação

        ProcessElection().Wait();
    }

    public static void GenerateCsvs(int qtdCandidados, int qtdEleitores)
    {
        Console.WriteLine("GERANDO CSVs");
        DataCreator.CreateCsvIdNameFile(CandidatoCsv, qtdCandidados);
        Console.WriteLine($"  + {qtdCandidados} candidatos -> {CandidatoCsv}");
        DataCreator.CreateCsvIdNameFile(EleitoresCsv, qtdEleitores);
        Console.WriteLine($"  + {qtdEleitores} eleitores -> {EleitoresCsv}");
    }

    /// <summary>
    /// Obtém dados da votação, com eleitores, candidatos e nome da eleição a partir dos CSV
    /// </summary>
    /// <returns></returns>
    private static Votacao GetVotacao()
    {
        // Ler os candidatos a partir do CSV
        using var candidatoCsv = File.OpenRead(CandidatoCsv);
        var candidatosParser = new CandidatoCsvParser(candidatoCsv);


        // Ler os eleitores a partir do CSV
        using var eleitoresCsv = File.OpenRead(EleitoresCsv);
        var eleitoresParser = new EleitorCsvParser(eleitoresCsv);


        var votacao = new Votacao(1, "Eleição para head da torre de vendas",
            candidatosParser.ToArray(),
            eleitoresParser.ToArray());
        Console.WriteLine(votacao.ToString());
        return votacao;
    }


    public static async Task ProcessElection()
    {
        Console.WriteLine("*** INÍCIO DA VOTAÇÃO ***");
        var votacao = GetVotacao();

        var configuracao = new Configuracao();
        var sender = new Sender(configuracao);
        Console.WriteLine($" Iniciando envio para fila {configuracao.BrokerQueue}");

        // Envio da primeira mensagem, com a abertura da votação
        Console.WriteLine("  + Envio da mensagem de início da votação");
        await sender.SendAsync(votacao);

        // Ids dos candidatos
        var idsCandidatos = votacao.Candidatos.Select(c => c.Id).ToImmutableArray();

        // Id máximo dos eleitores
        var idMaximoEleitor = votacao.Eleitores.Max(c => c.Id);

        var numeroDeVotos = (int)(votacao.Eleitores.Count * 1.1);
        Console.WriteLine($"  + Produção de {numeroDeVotos} votos");

        var tasks = new List<Task>();
        // Envio dos votos
        for (var i = 0; i < numeroDeVotos; i++)
        {
            int idEleitor;
            if (Random.Shared.Next(100) <= 10)
            {
                idEleitor = Random.Shared.Next(idMaximoEleitor + 1, idMaximoEleitor + 100);
                Console.WriteLine($"    + Voto para eleitor inválido: {idEleitor}");
            }
            else
            {
                idEleitor = votacao.Eleitores[i % votacao.Eleitores.Count].Id;
                Console.WriteLine($"    + Voto para eleitor válido: {idEleitor}");
            }
            var voto = new Voto(
                votacao.Id,
                idEleitor,
                idsCandidatos[Random.Shared.Next(idsCandidatos.Length)]);

            tasks.Add(sender.SendAsync(voto));
        }
        Task.WaitAll(tasks.ToArray());


        // Envio do encerramento da votação
        Console.WriteLine("  + Envio da mensagem de fim da votação");
        await sender.SendAsync(new TerminoDaVotacao(votacao.Id));

        Console.WriteLine("*** FIM DA VOTAÇÃO ***");


    }
}

