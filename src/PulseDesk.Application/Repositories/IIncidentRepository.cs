using PulseDesk.Domain.Entities;
using PulseDesk.Domain.ValueObjects;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(int id);
    Task<List<Incident>> GetAllAsync(IncidentStatus? status);
    Task AddAsync(Incident incident);
    Task SaveChangesAsync();
}
