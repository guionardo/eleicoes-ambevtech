using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Fakes
{
    public static class FakerEntities
    {
        public static List<string> Nomes(int quantidade)
        {
            var resultado = new List<string>();
            for (var i = 0; i < quantidade; i++)
            {
                resultado.Add(Faker.Name.FullName());
            }
            return resultado;
        }


        public static List<Candidato> Candidatos(int quantidade)
        {
            var candidatos = new List<Candidato>();
            for (var id = 1; id <= quantidade; id++)
            {
                candidatos.Add(new Candidato(id, Faker.Name.FullName()));

            }
            return candidatos;
        }

        public static List<Eleitor> Eleitores(int quantidade)
        {
            var eleitores = new List<Eleitor>();
            for (var id = 1; id <= quantidade; id++)
            {
                eleitores.Add(new Eleitor(id, Faker.Name.FullName()));
            }
            return eleitores;

        }
    }
}
