using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.Services.Abstract;
public interface IIncidentService
{
    Task<IncidentDto> CreateAsync(CreateIncidentCommand command);
    Task<IncidentDto?> GetByIdAsync(int id);
    Task<List<IncidentDto>> GetAllAsync(IncidentStatus? status);
    Task<List<CommentDto>?> GetCommentsAsync(int incidentId);
    Task<CommentDto?> AddCommentAsync(int incidentId, CreateCommentCommand command);
}
