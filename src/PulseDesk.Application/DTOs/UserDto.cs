using PulseDesk.Domain.Entities.ValueObjects;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Surname
);
