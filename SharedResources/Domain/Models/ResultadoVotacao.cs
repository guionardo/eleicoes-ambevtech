using SharedResources.Validacoes;

namespace SharedResources.Domain.Models;

public class ResultadoVotacao
{
    public int IdVotacao { get; private set; }
    public int IdCandidato1 { get; private set; }
    public int QtdVotosCandidato1 { get; private set; }
    public int IdCandidato2 { get; private set; }
    public int QtdVotosCandidato2 { get; private set; }
    public int QtdVotosTotal { get; private set; }
    public int QtdVotosNulosBrancos { get; private set; }

    public ResultadoVotacao(int id, int idCandidato1, int qtdVotosCandidato1, int idCandidato2, int qtdVotosCandidato2, int qtdVotosTotal, int qtdVotosNulosBrancos)
    {
        ModelValidations.ThrowForNotPositiveNumber(id, "Id da votação");
        ModelValidations.ThrowForNotPositiveNumber(IdCandidato1, "Id do candidato 1");
        ModelValidations.ThrowForNotPositiveNumber(IdCandidato2, "Id do candidato 2");
        ModelValidations.ThrowForNegativeNumber(qtdVotosCandidato1, "Quantidade de votos do candidato 1");
        ModelValidations.ThrowForNegativeNumber(qtdVotosCandidato2, "Quantidade de votos do candidato 2");
        ModelValidations.ThrowForNegativeNumber(qtdVotosTotal, "Quantidade total de votos");
        ModelValidations.ThrowForNegativeNumber(qtdVotosNulosBrancos, "Quantidade de votos nulos ou brancos");

        IdVotacao = id;
        IdCandidato1 = idCandidato1;
        IdCandidato2 = idCandidato2;
        QtdVotosCandidato1 = qtdVotosCandidato1;
        QtdVotosCandidato2 = qtdVotosCandidato2;
        QtdVotosTotal = qtdVotosTotal;
        QtdVotosNulosBrancos = qtdVotosNulosBrancos;
    }

}
