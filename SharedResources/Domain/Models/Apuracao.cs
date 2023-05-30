using MongoDB.Bson.Serialization.Attributes;

namespace SharedResources.Domain.Models;

[BsonIgnoreExtraElements]
public record Apuracao
{
    public int IdEleicao { get; set; }

    /// <summary>
    /// Representa os votos de um candidato
    /// Votos nulos/brancos -> chave = 0
    /// </summary>
    public Dictionary<string, int> Votos { get; set; } = new();

    public Apuracao(int idEleicao)
    {
        IdEleicao = idEleicao;
    }

}
