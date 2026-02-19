using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.Services.Abstract;
public interface IIncidentService
{
    Task<IncidentDto> CreateAsync(CreateIncidentCommand command);
    Task<List<IncidentDto>> GetAllAsync(IncidentStatus? status);
}
