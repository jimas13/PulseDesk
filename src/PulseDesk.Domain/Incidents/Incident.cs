using PulseDesk.Domain.Comments;
using PulseDesk.Domain.Entities.Abstract;
using PulseDesk.Domain.Users;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Domain.Incidents;

public class Incident : Entity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public IncidentPriority Priority { get; private set; }
    public IncidentStatus Status { get; private set; }

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

    private void ChangeStatus(IncidentStatus newStatus)
    {
        Status = newStatus;
        ModifiedAt = DateTime.UtcNow;
    }

    public void AddComment(User author, string message)
    {
        _comments.Add(new Comment(author, message));
        ModifiedAt = DateTime.UtcNow;
    }

    public void Resolve()
    {
        ChangeStatus(IncidentStatus.Resolved);
    }
    public void Close()
    {
        ChangeStatus(IncidentStatus.Closed);
    }

    public void Open()
    {
        ChangeStatus(IncidentStatus.Open);
    }
    public void SetInProgress()
    {
        ChangeStatus(IncidentStatus.InProgress);
    }
    
}
