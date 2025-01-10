using MongoDB.Driver;
using pokedex.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pokedex.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllPokemonsAsync();
        Task<Pokemon> GetPokemonByIdAsync(string id);
        Task CreatePokemonAsync(Pokemon pokemon);
        Task UpdatePokemonAsync(string id, Pokemon pokemon);
        Task DeletePokemonAsync(string id);
    }

    public class PokemonService : IPokemonService
    {
        private readonly IMongoDbContext _context;

        public PokemonService(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pokemon>> GetAllPokemonsAsync()
        {
            return await _context.Pokemons.Find(_ => true).ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByIdAsync(string id)
        {
            var filter = Builders<Pokemon>.Filter.Eq(pokemon => pokemon.Id, id);
            return await _context.Pokemons.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreatePokemonAsync(Pokemon pokemon)
        {
            await _context.Pokemons.InsertOneAsync(pokemon);
        }

        public async Task UpdatePokemonAsync(string id, Pokemon pokemon)
        {
            var filter = Builders<Pokemon>.Filter.Eq(pokemon => pokemon.Id, id);
            await _context.Pokemons.ReplaceOneAsync(filter, pokemon);
        }

        public async Task DeletePokemonAsync(string id)
        {
            var filter = Builders<Pokemon>.Filter.Eq(pokemon => pokemon.Id, id);
            await _context.Pokemons.DeleteOneAsync(filter);
        }
    }
}
