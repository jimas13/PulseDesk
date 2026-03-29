namespace PulseDesk.Application.DTOs;

public record class IncidentDto(
    Guid Id,
    string Title,
    string Description,
    string Priority,
    string Status,
    DateTime CreatedAt
);


public enum IncidentDtoPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum IncidentDtoStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}
