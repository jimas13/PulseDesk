using Microsoft.AspNetCore.Mvc;
using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Api.Controllers;
[ApiController]
[Route("api/incidents")]
[Produces("application/json")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _service;

    public IncidentsController(IIncidentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets all incidents, optionally filtered by status.
    /// </summary>
    /// <param name="status">Optional incident status filter.</param>
    /// <returns>The list of incidents.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<IncidentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<IncidentDto>>> GetAll(
        [FromQuery] IncidentStatus? status)
    {
        return Ok(await _service.GetAllAsync(status));
    }

    /// <summary>
    /// Gets a single incident by its identifier.
    /// </summary>
    /// <param name="id">Incident identifier.</param>
    /// <returns>The requested incident if found.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(IncidentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IncidentDto>> GetById(int id)
    {
        var incident = await _service.GetByIdAsync(id);
        if (incident is null)
            return NotFound();

        return Ok(incident);
    }

    /// <summary>
    /// Creates a new incident.
    /// </summary>
    /// <param name="command">Incident creation payload.</param>
    /// <returns>The created incident.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(IncidentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncidentDto>> Create(
        [FromBody] CreateIncidentCommand command)
    {
        var result = await _service.CreateAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets all comments associated with a specific incident.
    /// </summary>
    /// <param name="incidentId">Incident identifier.</param>
    /// <returns>The list of comments for the incident.</returns>
    [HttpGet("{incidentId:int}/comments")]
    [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CommentDto>>> GetComments(int incidentId)
    {
        var comments = await _service.GetCommentsAsync(incidentId);
        if (comments is null)
            return NotFound();

        return Ok(comments);
    }

    /// <summary>
    /// Adds a new comment to a specific incident.
    /// </summary>
    /// <param name="incidentId">Incident identifier.</param>
    /// <param name="command">Comment creation payload.</param>
    /// <returns>The created comment.</returns>
    [HttpPost("{incidentId:int}/comments")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> AddComment(
        int incidentId,
        [FromBody] CreateCommentCommand command)
    {
        var comment = await _service.AddCommentAsync(incidentId, command);
        if (comment is null)
            return NotFound();

        return CreatedAtAction(nameof(GetComments), new { incidentId }, comment);
    }
}
