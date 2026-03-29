using Microsoft.EntityFrameworkCore;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Infrastructure;
using PulseDesk.Infrastructure.Persistence;

public sealed class UserRepository : IUserRepository
{
    private readonly PulseDeskDbContext _context;

    public UserRepository(PulseDeskDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        var userdto = new UserDto(user.Id.Value, user.Name, user.Surname);
        return userdto;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return await _context.Users
            .Select(u => new UserDto(u.Id.Value, u.Name, u.Surname))
            .ToListAsync();
    }

    public async Task AddAsync(UserDto user)
    {
        var entity = new UserRecord
        {
            Id = new UserRecordId(Guid.NewGuid()),
            Name = user.Name,
            Surname = user.Surname
        };
        await _context.Users.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}