using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Provider.ReaderCsv;
public static class CandidatoPopulacaoCsvParser
{
    public static IEnumerable<CandidatoPopulacaoDeDados> ReadFromCsv(FileStream stream)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ",",
        };

        var reader = new StreamReader(stream);
        using var csvReader = new CsvReader(reader, csvConfig);
        csvReader.Context.RegisterClassMap<Candidato>();
        var isFirstRow = true;
        while (csvReader.Read())
        {
            if (isFirstRow)
            {
                isFirstRow = false;
                continue;
            }
            var candidato = csvReader.GetRecord<CandidatoPopulacaoDeDados>();
            yield return candidato;
        }
    }
}

public sealed class Candidato : ClassMap<CandidatoPopulacaoDeDados>
{
    public Candidato()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Id).NameIndex(0);
        Map(m => m.Nome).NameIndex(1);
    }
}

