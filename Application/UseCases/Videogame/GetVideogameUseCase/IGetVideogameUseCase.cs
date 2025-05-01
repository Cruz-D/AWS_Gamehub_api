using gamehub_API.Models;

namespace gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase
{
    public interface IGetVideogameUseCase
    {
        Task<Models.Videogame> ExecuteAsync(string id);
    }
}
