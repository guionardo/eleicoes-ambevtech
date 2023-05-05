using MongoDB.Driver;
using SharedResources.Domain.Models;
using SharedResources.Exceptions;

namespace SharedResources.Repositories;

public class ElectionRepository : IElectionRepository
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<Votacao> _colVotacao;
    private readonly IMongoCollection<Voto> _colVotos;
    private readonly IMongoCollection<Apuracao> _colApuracao;

    public ElectionRepository(Configuracao.Configuracao configuracao, CreateIndexOptions options)
    {
        _client = new MongoClient(configuracao.DatabaseConnectionString);
        _db = _client.GetDatabase(configuracao.DatabaseName);
        _colVotacao = _db.GetCollection<Votacao>("votacao");
        _colVotos = _db.GetCollection<Voto>("votos");
        _colApuracao = _db.GetCollection<Apuracao>("apuracao");

        // Índice para votação
        var indexModel = new CreateIndexModel<Votacao>("{Id:1}", new CreateIndexOptions { Unique = true });
        _colVotacao.Indexes.CreateOne(indexModel);


        // Índice para votos
        var voteIndexModel = new CreateIndexModel<Voto>("{IdEleicao:1,IdEleitor:1}", new CreateIndexOptions { Unique = true });
        _colVotos.Indexes.CreateOne(voteIndexModel);

        // Índice para apuração
        var apuracaoIndexModel = new CreateIndexModel<Apuracao>("{IdEleicao:1}", new CreateIndexOptions { Unique = true });
        _colApuracao.Indexes.CreateOne(apuracaoIndexModel);
    }

    public async Task<IEnumerable<Voto>> GetApuracaoAsync(int idEleicao)
    {
        var result = await _colVotos.FindAsync(v => v.IdEleicao == idEleicao);
        return result.ToEnumerable();
    }

    public async Task<Votacao> GetElectionAsync(int idEleicao)
    {
        return await _colVotacao.Find(v => v.Id == idEleicao).FirstOrDefaultAsync();
    }

    public async Task SaveCountAsync(Apuracao apuracao)
    {
        var result = await _colApuracao.ReplaceOneAsync(a => a.IdEleicao == apuracao.IdEleicao, apuracao, new ReplaceOptions { IsUpsert = true });
        if ((result?.MatchedCount > 0 && result?.ModifiedCount > 0) || result?.UpsertedId is not null)
        {
            return;
        }
        throw new PersistenceErrorException("apuracao", $"Não atualizou nem criou apuracao da eleição {apuracao.IdEleicao}");
    }

    public async Task SaveElectionAsync(Votacao votacao)
    {
        var result = await _colVotacao.ReplaceOneAsync(v => v.Id == votacao.Id, votacao, new ReplaceOptions { IsUpsert = true });
        if ((result?.MatchedCount > 0 && result?.ModifiedCount > 0) || result?.UpsertedId is not null)
        {
            return;
        }
        throw new PersistenceErrorException("votacao", "Não atualizou nem criou nova votação");
    }

    public async Task SaveVoteAsync(Voto voto)
    {
        try
        {
            await _colVotos.InsertOneAsync(voto);
        }
        catch (MongoDuplicateKeyException)
        {
            throw new PersistenceErrorException("voto", $"Já ocorreu um voto do eleitor {voto.IdEleitor} na votação {voto.IdEleicao}", false);
        }
    }
}