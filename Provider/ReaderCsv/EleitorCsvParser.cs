using CsvHelper.Configuration;
using SharedResources.Domain.Models;
using System.Globalization;

namespace Provider.ReaderCsv;

public class EleitorCsvParser : GenericCsvParser<EleitorMap, Eleitor>
{
    public EleitorCsvParser(Stream csvStream) : base(csvStream)
    {
    }
}

public sealed class EleitorMap : ClassMap<Eleitor>
{
    public EleitorMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Id).NameIndex(0);
        Map(m => m.Nome).NameIndex(1);
    }
}