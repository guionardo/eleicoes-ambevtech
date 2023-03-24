namespace Dominio.Models
{
    public record Candidato
    {

        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Candidato(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

}
