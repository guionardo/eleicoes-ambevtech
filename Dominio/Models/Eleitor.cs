using SharedResources.Validacoes;

namespace Dominio.Models
{
    public record Eleitor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Eleitor(int idEleitor, string nome)
        {
            ModelValidations.ThrowForNotPositiveNumber(idEleitor, "Id do eleitor");
            ModelValidations.ThrowForEmptyString(nome, "Nome do eleitor");

            Id = idEleitor;
            Nome = nome;
        }
    }

}
