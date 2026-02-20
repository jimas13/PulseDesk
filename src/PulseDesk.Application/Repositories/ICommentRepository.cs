using PulseDesk.Domain.Entities;

namespace PulseDesk.Application.Repositories.Abstract;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<List<Comment>> GetByIdsAsync(List<int> ids);
    Task SaveChangesAsync();
}
