using SharedResources.Validacoes;

namespace SharedResources.Domain.Models
{
    /// <summary>
    /// Dados da Votação
    /// </summary>
    /// <exception cref="ArgumentException"/>
    public class Votacao
    {
        public int Id { get; set; }

        public string Nome { get; set; } = "";

        public List<Candidato> Candidatos { get; set; } = new List<Candidato>();

        public List<Eleitor> Eleitores { get; set; } = new List<Eleitor>();

        public bool Encerrada { get; set; }

        public Votacao(int idVotacao, string nome, IEnumerable<Candidato> candidatos, IEnumerable<Eleitor> eleitores, bool encerrada = false)
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
            Encerrada = encerrada;
        }

        public Votacao()
        {
        }

        public override string ToString()
        {
            return $"Votação: #{Id} [{Nome}] - {Candidatos.Count} candidatos - {Eleitores.Count} eleitores";
        }
    }
}