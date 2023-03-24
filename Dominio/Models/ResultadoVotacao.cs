using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class ResultadoVotacao
    {
        public int IdVotacao {get; private set;}
        public int IdCandidato1 {get; private set;}
        public int QtdVotosCandidato1 {get; private set;}
        public int IdCandidato2 {get; private set;}
        public int QtdVotosCandidato2 {get; private set;}
        public int QtdVotosTotal {get; private set;}
        public int QtdVotosNulosBrancos { get; private set; }
    }
}
