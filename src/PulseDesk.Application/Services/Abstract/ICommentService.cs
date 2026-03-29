using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;

namespace PulseDesk.Application.Services.Abstract;

public interface ICommentService
{
    Task<IReadOnlyCollection<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(Guid id);
    Task<CommentDto> CreateAsync(CreateCommentCommand command);
}
