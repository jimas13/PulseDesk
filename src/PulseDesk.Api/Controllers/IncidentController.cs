using Microsoft.AspNetCore.Mvc;
using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Api.Controllers;
[ApiController]
[Route("api/incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _service;

    public IncidentsController(IIncidentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<IncidentDto>>> GetAll(
        [FromQuery] IncidentStatus? status)
    {
        return Ok(await _service.GetAllAsync(status));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IncidentDto>> GetById(int id)
    {
        var incident = await _service.GetByIdAsync(id);
        if (incident is null)
            return NotFound();

        return Ok(incident);
    }

    [HttpPost]
    public async Task<ActionResult<IncidentDto>> Create(
        [FromBody] CreateIncidentCommand command)
    {
        var result = await _service.CreateAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{incidentId:int}/comments")]
    public async Task<ActionResult<List<CommentDto>>> GetComments(int incidentId)
    {
        var comments = await _service.GetCommentsAsync(incidentId);
        if (comments is null)
            return NotFound();

        return Ok(comments);
    }

    [HttpPost("{incidentId:int}/comments")]
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
