using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    /// <summary>
    /// Dados da Votação
    /// </summary>
    /// <exception cref="ArgumentException"/>
    public record Votacao
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<Candidato> Candidatos { get; private set; }
        public List<Eleitor> Eleitores { get; private set; }

        public Votacao(int idVotacao, string nome, IEnumerable<Candidato> candidatos, IEnumerable<Eleitor> eleitores)
        {            
            if (idVotacao <= 0)            
                throw new ArgumentException("Id da votação deve ser um inteiro positivo", nameof(idVotacao));
            
            Id = idVotacao;

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da votação não deve ser vazio", nameof(nome));

            Nome = nome;

            if (candidatos is null || candidatos.Count() < 2)
                throw new ArgumentException("Número de candidatos deve ser maior do que 1", nameof(candidatos));

            Candidatos = new List<Candidato>();
            Candidatos.AddRange(candidatos);

            if (eleitores is null || eleitores.Count() < 1)
                throw new ArgumentException("Número de eleitores deve ser maior do que 0", nameof(eleitores));

            Eleitores = new List<Eleitor>();
            Eleitores.AddRange(eleitores);
        }
    }
}
