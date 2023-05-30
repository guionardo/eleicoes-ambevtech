namespace SharedResources.Domain.Models
{
    public class DetalheApuracao
    {
        public int IdVotacao { get; set; }
        public Dictionary<string, int> Contagem { get; } = new Dictionary<string, int>();
        public bool Encerrada { get; set; }
    }
}
