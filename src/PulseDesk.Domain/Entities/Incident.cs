using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Domain.Entities;

public class Incident
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public IncidentPriority Priority { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    private Incident() { } // EF only

    public Incident(string title, string description, IncidentPriority priority)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = IncidentStatus.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(IncidentStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddComment(string author, string message)
    {
        _comments.Add(new Comment(author, message));
        UpdatedAt = DateTime.UtcNow;
    }
}
