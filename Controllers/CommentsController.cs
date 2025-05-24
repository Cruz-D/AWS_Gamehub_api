using gamehub_API.Application.DTO.Comments;
using gamehub_API.Application.UseCases.Comments.CreateCommentUseCase;
using gamehub_API.Application.UseCases.Comments.DeleteCommentUseCase;
using gamehub_API.Application.UseCases.Comments.EditCommentUseCase;
using gamehub_API.Application.UseCases.Comments.ReadCommentsUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICreateCommentUseCase _createCommentUseCase;
    private readonly IReadCommentUseCase _readCommentUseCase;
    private readonly IEditCommentUseCase _editCommentUseCase;
    private readonly IDeleteCommentUseCase _deleteCommentUseCase;

    public CommentsController(
        ICreateCommentUseCase createCommentUseCase,
        IReadCommentUseCase readCommentUseCase,
        IEditCommentUseCase editCommentUseCase,
        IDeleteCommentUseCase deleteCommentUseCase)
    {
        _createCommentUseCase = createCommentUseCase;
        _readCommentUseCase = readCommentUseCase;
        _editCommentUseCase = editCommentUseCase;
        _deleteCommentUseCase = deleteCommentUseCase;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDTO dto)
    {
        try
        {
            var comment = await _createCommentUseCase.ExecuteAsync(dto.userId, dto.gameId, dto.content, dto.score);

            return CreatedAtAction(nameof(GetByGame), new { gameId = dto.gameId }, comment);
        }
        catch (Exception ex)
        {

            throw new Exception("Error on controller, METHOD -------> creating comment : " + ex.Message);
        }
    }
    [Authorize]
    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetByGame(string gameId, int page = 1, int size = 10)
    {
        try
        {
            var listComments = await _readCommentUseCase.ExecuteAsync(gameId, page, size);

            if (listComments == null || !listComments.Any())
            {
                return NotFound("No comments found for this game.");
            }

            return Ok(listComments);
        }
        catch (Exception ex)
        {

            throw new Exception("Error on controller, METHOD -------> get game´s comments : " + ex.Message);
        }
    }

    [Authorize]
    [HttpPut("{userId}/{commentId}")]
    public async Task<IActionResult> Edit([FromRoute] string userId, [FromBody] UpdateCommentDTO dto)
    {
        try
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(dto.commentId))
            {
                return BadRequest("User ID and Comment ID are required.");
            }
            var updatedComment = await _editCommentUseCase.ExecuteAsync(userId, dto.commentId, dto.content, dto.score);
            if (updatedComment == null)
            {
                return NotFound("Comment not found or you do not have permission to edit it.");
            }
            return Ok(updatedComment);
        }
        catch (Exception ex)
        {
            throw new Exception("Error on controller, METHOD -------> editing comment : " + ex.Message);
        }
    }

    [Authorize]
    [HttpDelete("{userId}/{commentId}")]
    public async Task<IActionResult> Delete(string userId, string commentId)
    {
        try
        {
            var deleteComment = await _deleteCommentUseCase.ExecuteAsync(userId, commentId);

            if (deleteComment == null)
            {
                return NotFound("Comment not found or you do not have permission to delete it.");
            }
            return NoContent();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
