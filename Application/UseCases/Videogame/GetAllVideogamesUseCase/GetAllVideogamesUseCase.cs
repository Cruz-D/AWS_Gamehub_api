
using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase
{
    public class GetAllVideogamesUseCase : IGetAllVideogamesUseCase
    {
        private readonly IVideogameInterface _videogameRepository;

        public GetAllVideogamesUseCase(IVideogameInterface videogameRepository)
        {
            _videogameRepository = videogameRepository;
        }

        public async Task<IEnumerable<Infrastructure.Models.Videogame>> ExecuteAsync()
        {
            Console.WriteLine("2 GetAllVideogamesUseCase: Executing use case to get all videogames.");

            // Call the repository to get all videogames

            var videogames = await _videogameRepository.GetAllVideogamesAsync();

            // TODO: MAPEAR A DTO

            // Return the list of videogames
            return videogames;

        }

    }
}
