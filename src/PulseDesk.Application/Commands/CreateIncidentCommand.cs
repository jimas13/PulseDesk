using PulseDesk.Application.DTOs;
namespace PulseDesk.Application.Commands;
public record CreateIncidentCommand(
    string Title,
    string Description,
    IncidentDtoPriority Priority
);
