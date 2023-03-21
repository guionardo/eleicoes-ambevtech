using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public record Votacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Candidato> Candidatos { get; set; }
        public List<Eleitor> Eleitores { get; set; }
    }
}
