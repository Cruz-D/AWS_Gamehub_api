using gamehub_API.Application.DTO.Comments;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.Comments.CreateCommentUseCase
{
    public interface ICreateCommentUseCase
    {
        Task<ReadCommentDTO> ExecuteAsync(string userId, string gameId, string content, string score);
    }
}
