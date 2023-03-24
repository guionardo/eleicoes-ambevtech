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
        public string DatabaseConnectionString { get;private set; }
        
        /// <summary>
        /// DATABASE_NAME
        /// </summary>
        public string DatabaseName { get; private set; }

    }
}
