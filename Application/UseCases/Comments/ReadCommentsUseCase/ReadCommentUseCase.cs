
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.Comments.ReadCommentsUseCase
{
    public class ReadCommentUseCase : IReadCommentUseCase
    {
        private readonly ICommentsInterface _repo;

        public ReadCommentUseCase(ICommentsInterface repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Infrastructure.Models.Comments>> ExecuteAsync( string gameId, int pageNumber, int pageSize)
        {
            return await _repo.GetCommentsByGameAsync(gameId, pageNumber, pageSize);
        }
    }
}
