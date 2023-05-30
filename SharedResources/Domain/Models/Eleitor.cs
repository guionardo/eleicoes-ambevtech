using SharedResources.Validacoes;

namespace SharedResources.Domain.Models
{
    public class Eleitor
    {
        public int Id { get; }

        public string Nome { get; }

        public Eleitor(int id, string nome)
        {
            ModelValidations.ThrowForNotPositiveNumber(id, "Id do eleitor");
            ModelValidations.ThrowForEmptyString(nome, "Nome do eleitor");

            Id = id;
            Nome = nome;
        }
    }
}