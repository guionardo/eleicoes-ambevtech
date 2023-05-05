using CsvHelper.Configuration;
using Dominio.Models;
using SharedResources.Domain.Models;
using System.Globalization;

namespace Provider.ReaderCsv;

public class CandidatoCsvParser : GenericCsvParser<CandidatoMap, Candidato>
{
    public CandidatoCsvParser(Stream csvStream) : base(csvStream)
    {
    }
}

public sealed class CandidatoMap : ClassMap<Candidato>
{
    public CandidatoMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Id).NameIndex(0);
        Map(m => m.Nome).NameIndex(1);
    }
}

