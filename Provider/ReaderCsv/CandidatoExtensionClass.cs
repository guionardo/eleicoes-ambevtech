namespace Provider.ReaderCsv;

public static class CandidatoExtensionClass
{
    public static string MakerAndModel(this CandidatoPopulacaoDeDados candidato)
    {
        return $"{candidato.Id}/{candidato.Nome}";
    }
}
