namespace Provider.ReaderCsv;

/// <summary>
/// Dados de população de candidatos
/// </summary>
public record CandidatoPopulacaoDeDados
{
    public const string URL = @"";

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
