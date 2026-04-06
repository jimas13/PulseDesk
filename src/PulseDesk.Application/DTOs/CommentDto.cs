using PulseDesk.Domain.Entities.ValueObjects;

namespace PulseDesk.Application.DTOs;

public class CommentDto
{
    public Guid Id {get; init;}
    public Guid IncidentId {get; init;}
    public Guid AuthorId {get; init;}
    public string Message {get; init;}
    public DateTime CreatedAt {get; init;}
}