using Microsoft.EntityFrameworkCore;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Domain.Entities;

namespace PulseDesk.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly PulseDeskDbContext _context;

    public CommentRepository(PulseDeskDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Comment>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Comments
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
