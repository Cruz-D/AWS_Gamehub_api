
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase
{
    public interface IGetAllVideogamesUseCase
    {
        Task<IEnumerable<Infrastructure.Models.Videogame>> ExecuteAsync();

    }
}
