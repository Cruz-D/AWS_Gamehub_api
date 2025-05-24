using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.Interfaces
{
    public interface ICommentsInterface
    {
        Task<Comments> AddCommentAsync(Comments comment);
        Task<IEnumerable<Comments>> GetCommentsByGameAsync(string gameId, int pageNumber, int pageSize);
        Task<Comments> EditCommentAsync(string userId, string commentId, string content, string score);
        Task DeleteCommentAsync(string userId, string commentId);

        Task<Double> GetAverageScoreByGameAsync(string gameId);


    }
}
