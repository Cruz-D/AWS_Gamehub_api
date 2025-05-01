using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.Interfaces
{
    public interface IVideogameRepository
    {
        // GET
        Task<List<Videogame>> GetAllVideogamesAsync(string sqlCosmosQuery);

        // GET/ID
        Task<Videogame> GetVideogameByIdAsync(string id);

    }
}
