using PulseDesk.Domain.Entities.ValueObjects;

namespace PulseDesk.Domain.Entities.Abstract;
public abstract class Entity
{
    public EntityId Id;
    public DateTime CreatedAt;
    public DateTime ModifiedAt;
}
