using System.Diagnostics.CodeAnalysis;
using PulseDesk.Domain.Comments;
using PulseDesk.Domain.Users;

namespace PulseDesk.Infrastructure.Persistence;

public class IncidentRecord
{
    public IncidentRecordId Id {get; set;}
    public string Title {get; set;}
    public string Description {get; set;}
    public IncidentRecordPriority Priority {get; set;}
    public IncidentRecordStatus Status {get; set;}
    private List<CommentRecord> _comments {get; set;} = new List<CommentRecord>();
    public IReadOnlyCollection<CommentRecord> Comments => _comments.ToList().AsReadOnly();
    public DateTime CreatedAt {get; set;}
    public DateTime ModifiedAt {get; set;}
    public UserRecord Author {get; set;}
    public UserRecordId AuthorId {get; set;}
    public UserRecord Assignee {get; set;}
    public UserRecordId AssigneeId {get; set;}
    public UserRecord Editor {get; set;}
    public UserRecordId EditorId {get; set;}
}

public enum IncidentRecordStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}
public enum IncidentRecordPriority
{
    Low,
    Medium,
    High,
    Critical
}

public readonly struct IncidentRecordId : IEquatable<IncidentRecordId>
{
    public Guid Value {get;}
    public IncidentRecordId(Guid Value)
    {
        this.Value = Value;
    }
    public static IncidentRecordId New()
    {
        return new(Guid.NewGuid());
    }

    public bool Equals(IncidentRecordId other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is IncidentRecordId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(IncidentRecordId id) => id.Value;
    public static explicit operator IncidentRecordId(Guid value) => new(value);
}