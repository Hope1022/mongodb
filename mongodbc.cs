using MongoDB.Driver;

namespace pokedex.Models
{
    public interface IMongoDbContext
    {
        IMongoCollection<Pokemon> Pokemons { get; }
    }

    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("PokedexDb");
        }

        public IMongoCollection<Pokemon> Pokemons => _database.GetCollection<Pokemon>("Pokemons");
    }
}
