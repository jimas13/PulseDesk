namespace PulseDesk.Application.DTOs;

public record CommentDto(
    int Id,
    string Author,
    string Message,
    DateTime CreatedAt
);
