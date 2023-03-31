using Provider.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Provider.Fakes
{
    public class TesteFakes
    {
        [Fact]
        public void TesteGeradorDeEleitores()
        {
            var eleitores = FakerEntities.Nomes(10);
            Assert.Equal(10, eleitores.Count);
        }
    }
}
