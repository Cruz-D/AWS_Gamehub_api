using gamehub_API.Models; 

namespace gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase
{
    public interface IGetAllVideogamesUseCase
    {
        Task<IEnumerable<Models.Videogame>> ExecuteAsync();

    }
}
