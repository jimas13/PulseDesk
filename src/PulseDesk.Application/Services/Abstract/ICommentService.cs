using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;

namespace PulseDesk.Application.Services.Abstract;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(int id);
    Task<CommentDto?> UpdateAsync(int id, UpdateCommentCommand command);
    Task<List<CommentDto>> BulkUpdateAsync(BulkUpdateCommentsCommand command);
}
