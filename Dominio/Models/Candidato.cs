namespace Dominio.Models
{
    public record Candidato
    {

        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Candidato(int idCandidato, string nome)
        {
            if (idCandidato <= 0)
                throw new ArgumentException("Id do candidato votação deve ser um inteiro positivo", nameof(idCandidato));

            Id = idCandidato;

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do candidato não deve ser vazio", nameof(nome));

            Nome = nome;
        }
    }

}
