namespace PulseDesk.Application.Commands;

public record CreateCommentCommand(
    string Author,
    string Message
);
