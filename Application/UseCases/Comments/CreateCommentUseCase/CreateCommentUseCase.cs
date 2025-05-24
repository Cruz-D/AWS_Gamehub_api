using gamehub_API.Application.Interfaces;
using gamehub_API.Application.UseCases.Comments.CreateCommentUseCase;
using gamehub_API.Infrastructure.Models;
using gamehub_API.Application.DTO.Comments;

public class CreateCommentUseCase : ICreateCommentUseCase
{
    private readonly ICommentsInterface _repo;
    private readonly IIdGenerator _idGenerator;

    public CreateCommentUseCase(ICommentsInterface repo, IIdGenerator idGenerator)
    {
        _repo = repo;
        _idGenerator = idGenerator;
    }

    public async Task<ReadCommentDTO> ExecuteAsync(string userId, string gameId, string content, string score)
    {
        var comment = new Comments
        {
            userId = userId,
            commentId = _idGenerator.GenerateId(),
            gameId = gameId,
            content = content,
            score = score,
            createdAt = DateTime.UtcNow.ToString("o"),
            updatedAt = null,
            isEdited = false,
            isDeleted = false
        };
        var newComment = await _repo.AddCommentAsync(comment);

        if (newComment == null)
        {
            throw new Exception("Error al crear el comentario");
        }

        // mapear resultado de vuelta  
        var readCommentDTO = new ReadCommentDTO
        {
            userId = newComment.userId,
            commentId = newComment.commentId,
            gameId = newComment.gameId,
            content = newComment.content,
            score = newComment.score,
            createdAt = newComment.createdAt,
        };

        return readCommentDTO;
    }
}
