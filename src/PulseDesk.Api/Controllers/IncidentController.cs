using Microsoft.AspNetCore.Mvc;
using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.ValueObjects;

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

    [HttpPost]
    public async Task<ActionResult<IncidentDto>> Create(
        CreateIncidentCommand command)
    {
        var result = await _service.CreateAsync(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
}
