using SharedResources.Validacoes;

namespace SharedResources.Domain.Models
{
    public class Candidato
    {
        public int Id { get; }

        public string Nome { get; }

        public Candidato(int id, string nome)
        {
            ModelValidations.ThrowForNotPositiveNumber(id, "Id do candidato");
            ModelValidations.ThrowForEmptyString(nome, "Nome do candidato");

            Id = id;
            Nome = nome;
        }

        public override string ToString()
        {
            return $"{Id} {Nome}";
        }
    }
}