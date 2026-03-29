using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;

namespace PulseDesk.Application.Services.Abstract;
public interface IIncidentService
{
    Task<IncidentDto> CreateAsync(CreateIncidentCommand command);
    Task<IncidentDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<IncidentDto>> GetAllAsync();
    Task<IReadOnlyCollection<CommentDto>?> GetCommentsAsync(Guid incidentId);
}
