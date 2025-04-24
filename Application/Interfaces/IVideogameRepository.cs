using gamehub_API.Models;

namespace gamehub_API.Application.Interfaces
{
    public interface IVideogameRepository
    {
        Task<IEnumerable<Videogame>> GetAllVideogamesAsync();
        Task<Videogame> GetVideogameByIdAsync(int id);
        Task AddVideogameAsync(Videogame videogame);
        Task UpdateVideogameAsync(Videogame videogame);
        Task DeleteVideogameAsync(int id);
    }
}
