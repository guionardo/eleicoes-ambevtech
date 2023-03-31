// See https://aka.ms/new-console-template for more information
using Dominio.Models;
using Provider.Fakes;

Console.WriteLine("Provider!");


// A função deste programa é simular a coleta de votos e enviar para a fila de processamento

// 1. Início da votação
// 2. Coleta de votos
// 3. Fim da votação

var candidatos = FakerEntities.Candidatos(5);
var eleitores = FakerEntities.Eleitores(1000);

var votacao = new Votacao(1, "Eleição para head da torre de vendas", candidatos, eleitores);

