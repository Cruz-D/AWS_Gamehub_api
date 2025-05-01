using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase
{
    public class GetVideogameUseCase : IGetVideogameUseCase
    {
        private readonly IVideogameRepository _videogameRepository;

        public GetVideogameUseCase(IVideogameRepository videogameRepository)
        {
            _videogameRepository = videogameRepository;
        }
        public async Task<Infrastructure.Models.Videogame> ExecuteAsync(string id)
        {
            var videogame = await _videogameRepository.GetVideogameByIdAsync(id);
            return videogame;
        }
    }
  
}
