namespace PulseDesk.Domain.Entities.ValueObjects;

public readonly struct ExternalId 
{
    private Guid Value {get; init;}
    public ExternalId(Guid guid)
    {
        Value = guid;
    }
    public static ExternalId New()
    {
        return new(Guid.NewGuid());
    }
    public override string ToString()    {
        return Value.ToString();
    }

}