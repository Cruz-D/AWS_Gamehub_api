namespace gamehub_API.Application.UseCases.Comments.ReadCommentsUseCase
{
    public interface IReadCommentUseCase
    {
        Task<IEnumerable<Infrastructure.Models.Comments>> ExecuteAsync(string gameId, int pageNumber, int pageSize);
    }
}
