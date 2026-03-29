using PulseDesk.Application.DTOs;

namespace PulseDesk.Application.Repositories.Abstract;
public interface IIncidentRepository
{
    Task<IncidentDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<IncidentDto>> GetAllAsync();
    Task<IEnumerable<CommentDto>> GetCommentsByIncidentIdAsync(Guid incidentId);
    Task AddAsync(IncidentDto incident);
    Task SaveChangesAsync();
}
