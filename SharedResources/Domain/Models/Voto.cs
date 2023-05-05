namespace SharedResources.Domain.Models;

/// <summary>
/// Registra inteñção de voto pelo eleitor.
/// Deve ser persistido no banco durante o periodo de votação.
/// Após a apuração, deve ser removido do banco.
/// </summary>
public record Voto
{
    public int IdEleicao { get; private set; }
    public int IdEleitor { get; private set; }
    public int IdCandidato { get; private set; }

    public Voto(int idEleicao, int idEleitor, int idCandidato)
    {
        IdEleicao = idEleicao;
        IdEleitor = idEleitor;
        IdCandidato = idCandidato;
    }

}
