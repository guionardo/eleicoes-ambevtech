using CsvHelper;
using CsvHelper.Configuration;
using SharedResources.Domain.Models;
using System.Collections;
using System.Globalization;

namespace Provider.ReaderCsv;

public class VotacaoCsvParser : IEnumerable<Votacao>
{
    public static CsvConfiguration CsvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
    {
        HasHeaderRecord = false,
        Delimiter = ",",
        MissingFieldFound = null,
    };
    private Stream _stream;
    private IEnumerable<Candidato> _candidatos;
    private IEnumerable<Eleitor> _eleitores;

    public VotacaoCsvParser(Stream csvStream, IEnumerable<Candidato> candidatos, IEnumerable<Eleitor> eleitores)
    {
        _stream = csvStream;
        _candidatos = candidatos;
        _eleitores = eleitores;
    }

    public IEnumerator<Votacao> GetEnumerator()
    {
        _stream.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(_stream);
        using var csvReader = new CsvReader(reader, CsvConfig);
        var isFirstRow = true;
        while (csvReader.Read())
        {
            if (isFirstRow)
            {
                isFirstRow = false;
                continue;
            }
            yield return new Votacao(
                csvReader.GetField<int>(0),
                csvReader.GetField<string>(1),
                _candidatos,
                _eleitores);

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

