using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.Commands;

public record CreateIncidentCommand(
    string Title,
    string Description,
    IncidentPriority Priority
);
