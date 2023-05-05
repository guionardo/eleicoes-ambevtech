using SharedResources.Validacoes;

namespace SharedResources.Domain.Models;

public record Candidato
{

    public int Id { get; private set; }
    public string Nome { get; private set; }

    public Candidato(int idCandidato, string nome)
    {
        ModelValidations.ThrowForNotPositiveNumber(idCandidato, "Id do candidato");
        ModelValidations.ThrowForEmptyString(nome, "Nome do candidato");

        Id = idCandidato;
        Nome = nome;
    }
}
