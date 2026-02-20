using Microsoft.EntityFrameworkCore;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Domain.Entities;
using PulseDesk.Domain.ValueObjects;
using PulseDesk.Infrastructure;

namespace PulseDesk.Infrastructure.Repositories;
public class IncidentRepository : IIncidentRepository
{
    private readonly PulseDeskDbContext _context;

    public IncidentRepository(PulseDeskDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Incident incident)
    {
        await _context.Incidents.AddAsync(incident);
    }

    public async Task<Incident?> GetByIdAsync(int id)
    {
        return await _context.Incidents
            .Include(i => i.Comments)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Incident>> GetAllAsync(IncidentStatus? status)
    {
        var query = _context.Incidents.AsQueryable();

        if (status.HasValue)
            query = query.Where(i => i.Status == status);

        return await query.ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsByIncidentIdAsync(int incidentId)
    {
        return await _context.Incidents
            .Where(i => i.Id == incidentId)
            .SelectMany(i => i.Comments)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
