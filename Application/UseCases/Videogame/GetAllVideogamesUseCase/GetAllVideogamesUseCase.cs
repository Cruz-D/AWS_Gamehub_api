
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase
{
    public class GetAllVideogamesUseCase : IGetAllVideogamesUseCase
    {
        private readonly IVideogameRepository _videogameRepository;

        public GetAllVideogamesUseCase(IVideogameRepository videogameRepository)
        {
            _videogameRepository = videogameRepository;
        }

        public Task<IEnumerable<Models.Videogame>> ExecuteAsync()
        { 
            Console.WriteLine("2 GetAllVideogamesUseCase: Executing use case to get all videogames.");
            return _videogameRepository.GetAllVideogamesAsync();
        }
    }
}
