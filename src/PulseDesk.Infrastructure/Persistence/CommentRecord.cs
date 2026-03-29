using PulseDesk.Domain.Incidents;

namespace PulseDesk.Infrastructure.Persistence;

public class CommentRecord
{
    public CommentRecordId Id { get; init; }
    public UserRecord Author { get; init; }
    public UserRecordId AuthorId {get; set;}
    public string Message { get; init; }
    public IncidentRecordId IncidentId { get; init; }
    public IncidentRecord Incident { get; init; }
    public DateTime CreatedAt {get; set;}
    public DateTime ModifiedAt {get; set;}
}

public readonly struct CommentRecordId : IEquatable<CommentRecordId>
{
    public Guid Value { get; }
    public CommentRecordId(Guid Value)
    {
        this.Value = Value;
    }
    public static CommentRecordId New()
    {
        return new(Guid.NewGuid());
    }

    public bool Equals(CommentRecordId other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is CommentRecordId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    public static implicit operator Guid(CommentRecordId id) => id.Value;
    public static explicit operator CommentRecordId(Guid value) => new(value);
}