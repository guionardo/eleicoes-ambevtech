using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Provider
{
    public class TestMainProgram
    {
        [Fact]
        public void TestGenerateCsvs()
        {

            Program.Main(new string[] { "", "generate" });
            Assert.True(File.Exists("candidatos.csv"));
            Assert.True(File.Exists("eleitores.csv"));
        }
    }
}
