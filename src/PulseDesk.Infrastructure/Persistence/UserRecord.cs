using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace PulseDesk.Infrastructure.Persistence;

public class UserRecord
{
    public UserRecordId Id {get; set;}
    public string Name {get; init;}
    public string Surname {get; init;}
    public UserStatus Status {get; init;}
    public DateTime CreatedAt {get; set;}
    public DateTime ModifiedAt {get; set;}
}

public enum UserStatus
{
    Active,
    Deactivated,
    Deleted

}

public struct UserRecordId : IEquatable<UserRecordId>
{
    public Guid Value;

    public UserRecordId(Guid value)
    {
        Value = value;
    }
    public static UserRecordId New()
    {
        return new(Guid.NewGuid());
    }
    public bool Equals(UserRecordId other)
    {
        return Value.Equals(other);
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UserRecordId other && Equals(other);
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    public static implicit operator Guid(UserRecordId id) => id.Value;
    public static explicit operator UserRecordId(Guid value) => new(value);
}