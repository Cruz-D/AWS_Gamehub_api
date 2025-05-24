namespace gamehub_API.Application.UseCases.Comments.DeleteCommentUseCase
{
    public interface IDeleteCommentUseCase
    {
        Task<Infrastructure.Models.Comments> ExecuteAsync(string userId, string commentId);
    }
}
