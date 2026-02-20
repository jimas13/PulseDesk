using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Domain.Entities;

public class Comment
{
    public int Id { get; private set; }
    public string Author { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Comment() { }

    public Comment(string author, string message)
    {
        Author = author;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? author, string? message)
    {
        if (!string.IsNullOrWhiteSpace(author))
            Author = author;

        if (!string.IsNullOrWhiteSpace(message))
            Message = message;
    }
}
