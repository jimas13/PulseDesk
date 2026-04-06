namespace PulseDesk.Application.DTOs;

public record UserDto
{
    public Guid Id {get; init;}
    public string Name {get; init;}
    public string Surname {get; init;}
}
