namespace PulseDesk.Application.DTOs;

public class IncidentDto
{
    public Guid Id {get; init;}
    public string Title {get; init;}
    public string Description {get; init;}
    public string Priority {get; init;}
    public string Status {get; init;}
    public DateTime CreatedAt {get; init;}
}


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
