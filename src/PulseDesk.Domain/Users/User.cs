using PulseDesk.Domain.Entities.Abstract;
using PulseDesk.Domain.Entities.ValueObjects;
using PulseDesk.Domain.ValueObjects;
namespace PulseDesk.Domain.Users;
public class User : Entity
{
    public ExternalId ExternalId {get; private set;}
    public string Name {get; private set;}
    public string Surname {get; private set;}
    public UserStatus Status {get; private set;}
    private User(){}
    public User(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
    public void ChangeStatus(UserStatus newStatus)
    {
        Status = newStatus;
        ModifiedAt = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"{Name} {Surname}";
    }
}
