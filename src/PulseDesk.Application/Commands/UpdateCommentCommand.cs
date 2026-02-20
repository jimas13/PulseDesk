namespace PulseDesk.Application.Commands;

public record UpdateCommentCommand(
    string? Author,
    string? Message
);
