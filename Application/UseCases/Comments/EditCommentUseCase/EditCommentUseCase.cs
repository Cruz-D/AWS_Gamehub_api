using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.Comments.EditCommentUseCase
{
    public class EditCommentUseCase : IEditCommentUseCase
    {
        private readonly ICommentsInterface _repo;

        public EditCommentUseCase(ICommentsInterface repo)
        {
            _repo = repo;
        }

        public async Task<Infrastructure.Models.Comments> ExecuteAsync(string userId, string commentId, string content, string score)
        {
            return await _repo.EditCommentAsync(userId, commentId, content, score);
        }
    }
}
