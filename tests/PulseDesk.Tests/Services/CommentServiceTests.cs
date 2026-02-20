using PulseDesk.Application.Commands;
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

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesAuthorAndMessage_WhenCommentExists()
    {
        var repository = new FakeCommentRepository();
        var comment = new Comment("old-author", "old-message");
        repository.Seed(comment, id: 1);
        var service = new CommentService(repository);

        var result = await service.UpdateAsync(1, new UpdateCommentCommand("new-author", "new-message"));

        Assert.NotNull(result);
        Assert.Equal("new-author", result!.Author);
        Assert.Equal("new-message", result.Message);
        Assert.True(repository.SaveChangesCalled);
    }

    [Fact]
    public async Task BulkUpdateAsync_ReturnsEmpty_WhenNoIdsProvided()
    {
        var repository = new FakeCommentRepository();
        var service = new CommentService(repository);

        var result = await service.BulkUpdateAsync(new BulkUpdateCommentsCommand([], "a", "m"));

        Assert.Empty(result);
        Assert.False(repository.SaveChangesCalled);
    }

    [Fact]
    public async Task BulkUpdateAsync_UpdatesAllMatchedComments()
    {
        var repository = new FakeCommentRepository();
        repository.Seed(new Comment("a1", "m1"), id: 1);
        repository.Seed(new Comment("a2", "m2"), id: 2);
        repository.Seed(new Comment("a3", "m3"), id: 3);
        var service = new CommentService(repository);

        var result = await service.BulkUpdateAsync(new BulkUpdateCommentsCommand([1, 3], "updated", "bulk"));

        Assert.Equal(2, result.Count);
        Assert.All(result, c =>
        {
            Assert.Equal("updated", c.Author);
            Assert.Equal("bulk", c.Message);
        });
        Assert.True(repository.SaveChangesCalled);
    }

    private sealed class FakeCommentRepository : ICommentRepository
    {
        private readonly List<Comment> _comments = [];
        public bool SaveChangesCalled { get; private set; }

        public Task<List<Comment>> GetAllAsync()
            => Task.FromResult(_comments.OrderByDescending(c => c.CreatedAt).ToList());

        public Task<Comment?> GetByIdAsync(int id)
            => Task.FromResult(_comments.FirstOrDefault(c => c.Id == id));

        public Task<List<Comment>> GetByIdsAsync(List<int> ids)
            => Task.FromResult(_comments.Where(c => ids.Contains(c.Id)).ToList());

        public Task SaveChangesAsync()
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }

        public void Seed(Comment comment, int id)
        {
            // Set private Id property for test setup to simulate persisted entities.
            typeof(Comment).GetProperty(nameof(Comment.Id))!.SetValue(comment, id);
            _comments.Add(comment);
        }
    }
}
