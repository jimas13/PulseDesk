using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.DTOs;

public record IncidentDto(
    int Id,
    string Title,
    string Description,
    IncidentPriority Priority,
    IncidentStatus Status,
    DateTime CreatedAt
);
