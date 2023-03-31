using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Configuracao
{
    /// <summary>
    /// Classe de configuração do sistema
    /// Ler a partir de variáveis de ambiente
    /// </summary>
    public class Configuracao
    {
        /// <summary>
        /// BROKER_CONNECTION_STRING
        /// </summary>
        public string BrokerConnectionString { get; private set; }

        /// <summary>
        /// BROKER_QUEUE
        /// </summary>
        public string BrokerQueue { get; private set; }

        /// <summary>
        ///  DATABASE_CONNECTION_STRING
        /// </summary>
        public string DatabaseConnectionString { get; private set; }

        /// <summary>
        /// DATABASE_NAME
        /// </summary>
        public string DatabaseName { get; private set; }

        public Configuracao()
        {
            BrokerConnectionString = ReadEnv("BROKER_CONNECTION_STRING");
            BrokerQueue = ReadEnv("BROKER_QUEUE");
            DatabaseConnectionString = ReadEnv("DATABASE_CONNECTION_STRING");
            DatabaseName = ReadEnv("DATABASE_NAME");
        }

        private static string ReadEnv(string env)
        {
            var value = Environment.GetEnvironmentVariable(env);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"Variável de ambiente {env} não foi definida ou está vazia");
            return value;
        }

    }
}
