using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Application.Services.Abstract;
using PulseDesk.Domain.Entities;

namespace PulseDesk.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        var comments = await _repository.GetAllAsync();
        return comments.Select(Map).ToList();
    }

    public async Task<CommentDto?> GetByIdAsync(int id)
    {
        var comment = await _repository.GetByIdAsync(id);
        return comment is null ? null : Map(comment);
    }

    public async Task<CommentDto?> UpdateAsync(int id, UpdateCommentCommand command)
    {
        var comment = await _repository.GetByIdAsync(id);
        if (comment is null)
            return null;

        comment.Update(command.Author, command.Message);
        await _repository.SaveChangesAsync();

        return Map(comment);
    }

    public async Task<List<CommentDto>> BulkUpdateAsync(BulkUpdateCommentsCommand command)
    {
        if (command.CommentIds is null || command.CommentIds.Count == 0)
            return [];

        var comments = await _repository.GetByIdsAsync(command.CommentIds);

        foreach (var comment in comments)
        {
            comment.Update(command.Author, command.Message);
        }

        await _repository.SaveChangesAsync();
        return comments.Select(Map).ToList();
    }

    private static CommentDto Map(Comment comment) =>
        new(
            comment.Id,
            comment.Author,
            comment.Message,
            comment.CreatedAt
        );
}
