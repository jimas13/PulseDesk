using PulseDesk.Application.Commands;
using PulseDesk.Application.DTOs;
using PulseDesk.Application.Repositories.Abstract;
using PulseDesk.Application.Services;
using PulseDesk.Domain.Entities;

namespace PulseDesk.Tests.Services;

public class CommentServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenCommentDoesNotExist()
    {
        var repository = new FakeCommentRepository();
        var service = new CommentService(repository);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    // [Fact]
    // public async Task BulkUpdateAsync_ReturnsEmpty_WhenNoIdsProvided()
    // {
    //     var repository = new FakeCommentRepository();
    //     var service = new CommentService(repository);

    //     var result = await service.BulkUpdateAsync(new BulkUpdateCommentsCommand([], "a", "m"));

    //     Assert.Empty(result);
    //     Assert.False(repository.SaveChangesCalled);
    // }

    // [Fact]
    // public async Task BulkUpdateAsync_UpdatesAllMatchedComments()
    // {
    //     var repository = new FakeCommentRepository();
    //     repository.Seed(new Comment("a1", "m1"), id: 1);
    //     repository.Seed(new Comment("a2", "m2"), id: 2);
    //     repository.Seed(new Comment("a3", "m3"), id: 3);
    //     var service = new CommentService(repository);

    //     var result = await service.BulkUpdateAsync(new BulkUpdateCommentsCommand([1, 3], "updated", "bulk"));

    //     Assert.Equal(2, result.Count);
    //     Assert.All(result, c =>
    //     {
    //         Assert.Equal("updated", c.Author);
    //         Assert.Equal("bulk", c.Message);
    //     });
    //     Assert.True(repository.SaveChangesCalled);
    // }

    private sealed class FakeCommentRepository : ICommentRepository
    {
        private readonly List<CommentDto> _comments = [];
        public bool SaveChangesCalled { get; private set; }

        public Task AddAsync(CommentDto comment)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentDto>> GetAllAsync()
            => Task.FromResult(_comments.AsEnumerable());

        public Task<CommentDto?> GetByIdAsync(Guid id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(comment != null ? new CommentDto(comment.Id, comment.IncidentId, comment.AuthorId, comment.Message, comment.CreatedAt) : null);
        }
        public Task SaveChangesAsync()
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }

        public void Seed(CommentDto comment, int id)
        {
            // Set private Id property for test setup to simulate persisted entities.
            typeof(CommentDto).GetProperty(nameof(CommentDto.Id))!.SetValue(comment, id);
            _comments.Add(comment);
        }
    }
}
