namespace Dominio.Models
{
    public record Eleitor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Eleitor(int idEleitor, string nome)
        {
            if (idEleitor <= 0)
                throw new ArgumentException("Id do eleitor deve ser um inteiro positivo", nameof(idEleitor));

            Id = idEleitor;

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do eleitor não deve ser vazio", nameof(nome));

            Nome = nome;
        }
    }

}
