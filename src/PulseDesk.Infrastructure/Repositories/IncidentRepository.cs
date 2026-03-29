using Microsoft.EntityFrameworkCore;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Domain.Entities;
using PulseDesk.Domain.Incidents;
using PulseDesk.Domain.ValueObjects;
using PulseDesk.Infrastructure;
using PulseDesk.Infrastructure.Persistence;

namespace PulseDesk.Infrastructure.Repositories;
public class IncidentRepository : IIncidentRepository
{
    private readonly PulseDeskDbContext _context;

    public IncidentRepository(PulseDeskDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(IncidentDto incident)
    {
        var incidentEntity = new IncidentRecord {
            Title = incident.Title,
            Description = incident.Description,
            Priority = Enum.Parse<IncidentRecordPriority>(incident.Priority),
            Status = Enum.Parse<IncidentRecordStatus>(incident.Status),
            CreatedAt = incident.CreatedAt
        };
        await _context.Incidents.AddAsync(incidentEntity);
        await SaveChangesAsync();
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();

    public async Task<IncidentDto?> GetByIdAsync(Guid id)
    {
        var incident = await _context.Incidents
            .Include(i => i.Comments)
            .FirstOrDefaultAsync(i => i.Id == id);
        var incidentDto = incident != null ? new IncidentDto(incident.Id, incident.Title, incident.Description, incident.Priority.ToString(), incident.Status.ToString(), incident.CreatedAt) : null;
        return incidentDto;
    }

    public async Task<IEnumerable<IncidentDto>> GetAllAsync()
    {
        var incidents = await _context.Incidents
            .Include(i => i.Comments)
            .ToListAsync();
        return incidents.Select(i => new IncidentDto(i.Id, i.Title, i.Description, i.Priority.ToString(), i.Status.ToString(), i.CreatedAt));
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByIncidentIdAsync(Guid incidentId)
    {
        var comments = await _context.Incidents
            .Where(i => i.Id == incidentId)
            .SelectMany(i => i.Comments)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
        return comments.Select(c => new CommentDto(c.Id, c.IncidentId, c.Author.Id.Value, c.Message, c.CreatedAt));
    }
}
