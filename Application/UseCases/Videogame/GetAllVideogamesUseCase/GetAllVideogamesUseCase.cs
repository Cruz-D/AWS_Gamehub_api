
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

        public async Task<IEnumerable<Models.Videogame>> ExecuteAsync()
        {
            Console.WriteLine("2 GetAllVideogamesUseCase: Executing use case to get all videogames.");

            // Call the repository to get all videogames

            string sqlCosmosQuery = "SELECT * FROM c";
            return _videogameRepository.GetAllVideogamesAsync(sqlCosmosQuery).Result;
        }

    }
}
