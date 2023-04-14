using Dominio.Models;
using Provider.ReaderCsv;


namespace Testes.Provider.ReaderCsv;

public class CsvParsersTest
{
    private string _fileCandidatos;
    private string _fileEleitores;
    private string _fileVotacao;
    private static List<Candidato> candidatos;
    private static List<Eleitor> eleitores;

    public CsvParsersTest()
    {
        _fileCandidatos = Path.Join("Fixtures", "sample_candidatos.csv");
        _fileEleitores = Path.Join("Fixtures", "sample_eleitores.csv");
        _fileVotacao = Path.Join("Fixtures", "sample_votacao.csv");
    }

    [Fact]
    public void ReadAllCandidates_ShouldCountFourCandidates()
    {
        using var _candidatos = new FileStream(_fileCandidatos, FileMode.Open);
        var parser = new CandidatoCsvParser(_candidatos);
        candidatos = parser.ToList();
        Assert.Equal(4, candidatos.Count);
    }

    [Fact]
    public void ReadAllElectors_ShouldCountSixElectors()
    {
        using var _eleitores = new FileStream(_fileEleitores, FileMode.Open);
        var parser = new EleitorCsvParser(_eleitores);
        eleitores = parser.ToList();
        Assert.Equal(6, eleitores.Count);
    }

    [Fact]
    public void ReadAllVoting_ShouldCountOneVoting()
    {
        using var _votacoes = new FileStream(_fileVotacao, FileMode.Open);
        var parser = new VotacaoCsvParser(_votacoes, candidatos, eleitores);
        var votacoes = parser.ToList();
        Assert.Single(votacoes);
    }



}
