using PulseDesk.Domain.Users;

namespace PulseDesk.Application.Commands;

public record CreateCommentCommand(
    Guid AuthorId,
    Guid IncidentId,
    string Message
);
