using PulseDesk.Application.DTOs;

namespace PulseDesk.Application.Repositories.Abstract;
public interface IUserRepository
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task AddAsync(UserDto user);
    Task SaveChangesAsync();
}
