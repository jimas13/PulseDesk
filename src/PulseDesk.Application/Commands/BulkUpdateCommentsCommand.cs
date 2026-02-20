namespace PulseDesk.Application.Commands;

public record BulkUpdateCommentsCommand(
    List<int> CommentIds,
    string? Author,
    string? Message
);
