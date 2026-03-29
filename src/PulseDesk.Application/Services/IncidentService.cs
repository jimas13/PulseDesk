using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.Incidents;

namespace PulseDesk.Application.Services;
public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUserRepository _userRepository;

    public IncidentService(IIncidentRepository incidentRepository, IUserRepository userRepository)
    {
        _incidentRepository = incidentRepository;
        _userRepository = userRepository;
    }

    public async Task<IncidentDto> CreateAsync(CreateIncidentCommand command)
    {
        var tempId = Guid.NewGuid();
        var incident = new IncidentDto(tempId, command.Title, command.Description, command.Priority.ToString(), "Open", DateTime.UtcNow);
        await _incidentRepository.AddAsync(incident);
        await _incidentRepository.SaveChangesAsync();

        return incident;
    }

    public async Task<IReadOnlyCollection<IncidentDto>> GetAllAsync()
    {
        var incidents = await _incidentRepository.GetAllAsync();
        return incidents.ToList().AsReadOnly();
    }

    public async Task<IncidentDto?> GetByIdAsync(Guid id)
    {
        
        var incident = await _incidentRepository.GetByIdAsync(id);
        return incident;

    }
    public async Task<IReadOnlyCollection<CommentDto>?> GetCommentsAsync(Guid incidentId)
    {
        var incident = await _incidentRepository.GetByIdAsync(incidentId);
        if (incident is null)
            return null;

        var comments = await _incidentRepository.GetCommentsByIncidentIdAsync(incidentId);
        return comments.ToList();
    }
    private static IncidentDto Map(Incident i) =>
        new(
            i.Id.Id,
            i.Title,
            i.Description,
            i.Priority.ToString(),
            i.Status.ToString(),
            i.CreatedAt
        );
}
