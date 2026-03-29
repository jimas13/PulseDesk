
using PulseDesk.Application.DTOs;
namespace PulseDesk.Application.Repositories.Abstract;
public interface ICommentRepository
{
    Task<IEnumerable<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(Guid id);
    Task AddAsync(CommentDto comment);
    Task SaveChangesAsync();
}
