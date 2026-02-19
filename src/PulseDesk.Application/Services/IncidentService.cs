using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.Entities;
using PulseDesk.Domain.ValueObjects;
using PulseDesk.Application.Repositories.Abstract;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _repository;

    public IncidentService(IIncidentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IncidentDto> CreateAsync(CreateIncidentCommand command)
    {
        var incident = new Incident(
            command.Title,
            command.Description,
            command.Priority
        );

        await _repository.AddAsync(incident);
        await _repository.SaveChangesAsync();

        return Map(incident);
    }

    public async Task<List<IncidentDto>> GetAllAsync(IncidentStatus? status)
    {
        var incidents = await _repository.GetAllAsync(status);
        return incidents.Select(Map).ToList();
    }

    private static IncidentDto Map(Incident i) =>
        new(
            i.Id,
            i.Title,
            i.Description,
            i.Priority,
            i.Status,
            i.CreatedAt
        );
}
