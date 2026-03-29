using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Domain.ValueObjects;

namespace PulseDesk.Application.Services.Abstract;
public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<List<UserDto>> GetAllAsync();
}
