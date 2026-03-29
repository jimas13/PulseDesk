using PulseDesk.Domain.Entities.Abstract;
using PulseDesk.Domain.Users;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Domain.Comments;

public class Comment : Entity
{
    public User Author { get; private set; }
    public User Editor { get; private set; }
    public string Message { get; private set; }

    private Comment() { }

    public Comment(User author, string message)
    {
        Author = author;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(User? author, string? message)
    {
        if (!string.IsNullOrWhiteSpace(author.ToString()))
            Author = author;

        if (!string.IsNullOrWhiteSpace(message))
            Message = message;
    }

    public override string ToString()
    {
        return $"{Author.Name} {Author.Surname}";
    }
}
