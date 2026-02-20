using PulseDesk.Domain.Entities;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.Repositories.Abstract;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(int id);
    Task<List<Incident>> GetAllAsync(IncidentStatus? status);
    Task<List<Comment>> GetCommentsByIncidentIdAsync(int incidentId);
    Task AddAsync(Incident incident);
    Task SaveChangesAsync();
}
