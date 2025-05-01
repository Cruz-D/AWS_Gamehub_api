namespace gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase
{
    public interface IGetVideogameUseCase
    {
        Task<Infrastructure.Models.Videogame> ExecuteAsync(string id);
    }
}
