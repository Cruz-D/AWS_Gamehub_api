
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.Comments.DeleteCommentUseCase
{
    public class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        private readonly ICommentsInterface _repo;

        public DeleteCommentUseCase(ICommentsInterface repo)
        {
            _repo = repo;
        }

        public async Task<Infrastructure.Models.Comments> ExecuteAsync(string userId, string commentId)
        {
            try
            {
                await _repo.DeleteCommentAsync(userId, commentId);
                return null; // Assuming the method should return null after deletion.
            }
            catch (Exception ex)
            {

                throw new Exception("excepcion " + ex);
            }
        }
    }
}
