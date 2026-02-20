using Microsoft.AspNetCore.Mvc;
using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Services.Abstract;

namespace PulseDesk.Api.Controllers;

[ApiController]
[Route("api/comments")]
[Produces("application/json")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentsController(ICommentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets all comments across incidents.
    /// </summary>
    /// <returns>A list of comments.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CommentDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>
    /// Gets a comment by its identifier.
    /// </summary>
    /// <param name="id">Comment identifier.</param>
    /// <returns>The requested comment if found.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> GetById(int id)
    {
        var comment = await _service.GetByIdAsync(id);
        if (comment is null)
            return NotFound();

        return Ok(comment);
    }

    /// <summary>
    /// Updates a single comment without requiring incident context.
    /// </summary>
    /// <param name="id">Comment identifier.</param>
    /// <param name="command">Comment update payload.</param>
    /// <returns>The updated comment if found.</returns>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> Update(
        int id,
        [FromBody] UpdateCommentCommand command)
    {
        var comment = await _service.UpdateAsync(id, command);
        if (comment is null)
            return NotFound();

        return Ok(comment);
    }

    /// <summary>
    /// Applies the same update to multiple comments.
    /// </summary>
    /// <param name="command">Bulk comment update payload.</param>
    /// <returns>The list of comments that were updated.</returns>
    [HttpPut("bulk")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<CommentDto>>> BulkUpdate(
        [FromBody] BulkUpdateCommentsCommand command)
    {
        var comments = await _service.BulkUpdateAsync(command);
        return Ok(comments);
    }
}
