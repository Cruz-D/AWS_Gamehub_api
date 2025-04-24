using gamehub_API.Application.Interfaces;
using gamehub_API.DbContext.NewFolder;
using gamehub_API.Models;
using Microsoft.EntityFrameworkCore;

namespace gamehub_API.Infrastructure.Repositories
{
    public class VideogameRepository : IVideogameRepository
    {
        private readonly LocalDbContext _dbContext;

        public VideogameRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddVideogameAsync(Videogame videogame)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVideogameAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Videogame>> GetAllVideogamesAsync()
        {
            Console.WriteLine("3 VideogameRepository: Getting all videogames from the database.");
            return await _dbContext.Videogames.ToListAsync();
        }

        public Task<Videogame> GetVideogameByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVideogameAsync(Videogame videogame)
        {
            throw new NotImplementedException();
        }
    }

}
