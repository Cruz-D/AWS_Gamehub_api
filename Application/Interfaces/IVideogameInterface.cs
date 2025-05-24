using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.Interfaces
{
    public interface IVideogameInterface
    {
        // GET
        Task<List<Videogame>> GetAllVideogamesAsync();

        // GET/ID
        Task<Videogame> GetVideogameByIdAsync(string id);

    }
}
