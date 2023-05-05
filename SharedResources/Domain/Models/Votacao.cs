using SharedResources.Validacoes;

namespace SharedResources.Domain.Models;

/// <summary>
/// Dados da Votação
/// </summary>
/// <exception cref="ArgumentException"/>
public record Votacao
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public List<Candidato> Candidatos { get; private set; }
    public List<Eleitor> Eleitores { get; private set; }

    public Votacao(int idVotacao, string nome, IEnumerable<Candidato> candidatos, IEnumerable<Eleitor> eleitores)
    {
        ModelValidations.ThrowForNotPositiveNumber(idVotacao, "Id da votação");
        ModelValidations.ThrowForEmptyString(nome, "Nome da votação");
        ModelValidations.ThrowForNullOrEmptyEnumerable(candidatos, "Lista de candidatos");
        ModelValidations.ThrowForNullOrEmptyEnumerable(eleitores, "Lista de eleitores");

        Id = idVotacao;
        Nome = nome;
        Candidatos = new List<Candidato>();
        Candidatos.AddRange(candidatos);
        Eleitores = new List<Eleitor>();
        Eleitores.AddRange(eleitores);
    }

    public override string ToString() => $"Votação: #{Id} [{Nome}] - {Candidatos.Count} candidatos - {Eleitores.Count} eleitores";

}
