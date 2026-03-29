using PulseDesk.Domain.Entities.ValueObjects;

namespace PulseDesk.Application.DTOs;

public record CommentDto(
    Guid Id,
    Guid IncidentId,
    Guid AuthorId,
    string Message,
    DateTime CreatedAt
);