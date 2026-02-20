using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.Entities;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.Services;
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

    public async Task<IncidentDto?> GetByIdAsync(int id)
    {
        var incident = await _repository.GetByIdAsync(id);
        return incident is null ? null : Map(incident);
    }

    public async Task<List<CommentDto>?> GetCommentsAsync(int incidentId)
    {
        var incident = await _repository.GetByIdAsync(incidentId);
        if (incident is null)
            return null;

        var comments = await _repository.GetCommentsByIncidentIdAsync(incidentId);
        return comments.Select(MapComment).ToList();
    }

    public async Task<CommentDto?> AddCommentAsync(int incidentId, CreateCommentCommand command)
    {
        var incident = await _repository.GetByIdAsync(incidentId);
        if (incident is null)
            return null;

        incident.AddComment(command.Author, command.Message);
        await _repository.SaveChangesAsync();

        var comment = incident.Comments
            .OrderByDescending(c => c.CreatedAt)
            .First();

        return MapComment(comment);
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

    private static CommentDto MapComment(Comment c) =>
        new(
            c.Id,
            c.Author,
            c.Message,
            c.CreatedAt
        );
}
