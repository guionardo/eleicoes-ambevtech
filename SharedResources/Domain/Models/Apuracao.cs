namespace SharedResources.Domain.Models;

public record Apuracao
{
    public readonly int IdEleicao;

    /// <summary>
    /// Representa os votos de um candidato
    /// Votos nulos/brancos -> chave = 0
    /// </summary>
    public readonly Dictionary<int, int> Votos = new();

    public Apuracao(int idEleicao)
    {
        IdEleicao = idEleicao;
    }

}
