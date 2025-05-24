namespace gamehub_API.Application.UseCases.Comments.EditCommentUseCase
{
    public interface IEditCommentUseCase
    {
        Task<Infrastructure.Models.Comments> ExecuteAsync(string userId, string commentId, string content, string score);
    }
}
