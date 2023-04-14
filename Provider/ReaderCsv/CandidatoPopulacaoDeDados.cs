namespace Provider.ReaderCsv;

/// <summary>
/// Dados de população de candidatos
/// Disponível em https://catalog.data.gov/dataset/electric-vehicle-population-data
/// </summary>
public record CandidatoPopulacaoDeDados
{
    public const string URL = @"https://data.wa.gov/api/views/f6w7-q2d2/rows.csv?accessType=DOWNLOAD";

    /// <summary>
    /// Id do candidato
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do candidato
    /// </summary>
    public string Nome { get; set; } = default!;

    public override string ToString()
    {
        return $"Candidato: {Id} - {Nome}";
    }
}
