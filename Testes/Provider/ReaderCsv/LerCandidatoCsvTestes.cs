using Provider.ReaderCsv;

namespace Testes.Provider.ReaderCsv;
public class LerCandidatoCsvTestes
{
    [Fact]
    public void Deve_Ler_Os_Candidatos_Do_Csv()
    {
        var downloader = new CachedDataDownloader();
        using var reader = downloader.GetReaderFromUrl(CandidatoPopulacaoDeDados.URL);
        Assert.True(reader.CanRead);
        var vehicles = CandidatoPopulacaoCsvParser.ReadFromCsv(reader);
        Assert.NotEmpty(vehicles);
    }
}
