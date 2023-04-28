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
        const string SecretFile = "secrets.txt";
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
            {
                LoadSecretsFile();
                value = Environment.GetEnvironmentVariable(env);
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException($"Variável de ambiente {env} não foi definida ou está vazia");
            }
            return value;
        }

        /// <summary>
        /// Loads secrets.txt
        /// </summary>
        private static void LoadSecretsFile()
        {
            if (!Path.Exists(SecretFile))
                return;

            foreach (var line in File.ReadAllLines(SecretFile))
            {
                var words = line.Split('=', 2).Select(w => w.Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                if (words.Length == 2)
                {
                    Environment.SetEnvironmentVariable(words[0].Trim(), words[1].Trim());
                }
            }
        }

    }
}
