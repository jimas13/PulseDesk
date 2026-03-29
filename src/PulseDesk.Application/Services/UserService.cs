using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Application.Services.Abstract;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
            return null;

        return new UserDto(user.Id, user.Name, user.Surname);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto(u.Id, u.Name, u.Surname)).ToList();
    }
}