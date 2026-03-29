using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseDesk.Domain.Entities;
using PulseDesk.Infrastructure.Persistence;

namespace PulseDesk.Infrastructure;

public class PulseDeskDbContext : DbContext
{
    public DbSet<IncidentRecord> Incidents => Set<IncidentRecord>();
    public DbSet<CommentRecord> Comments => Set<CommentRecord>();
    public DbSet<UserRecord> Users => Set<UserRecord>();
    public PulseDeskDbContext(DbContextOptions<PulseDeskDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PulseDeskDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

public class IncidentRecordTypeConfiguration : IEntityTypeConfiguration<IncidentRecord>
{
    public void Configure(EntityTypeBuilder<IncidentRecord> builder)
    {
        builder.ToTable("Incidents");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(i => i.Id).HasConversion(v => v.Value, v => new IncidentRecordId(v));

        builder.Property(i => i.Title).IsRequired().HasMaxLength(200);
        builder.Property(i => i.Description).HasMaxLength(1000);
        builder.Property(i => i.Priority).IsRequired();
        builder.Property(i => i.CreatedAt)
                .HasConversion(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();
        builder.Property(i => i.ModifiedAt)
                .HasConversion(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();
        builder.Property(i => i.AuthorId).HasConversion(v => v.Value, v => new UserRecordId(v));
        builder.Property(i => i.AssigneeId).HasConversion(v => v.Value, v => new UserRecordId(v));
        builder.Property(i => i.EditorId).HasConversion(v => v.Value, v => new UserRecordId(v));

        builder.HasMany(i => i.Comments)
            .WithOne(i => i.Incident)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(i => i.Author)
            .WithMany()
            .HasForeignKey(i => i.AuthorId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(i => i.Assignee)
            .WithMany()
            .HasForeignKey(i => i.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(i => i.Editor)
            .WithMany()
            .HasForeignKey(i => i.EditorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
public class CommentRecordTypeConfiguration : IEntityTypeConfiguration<CommentRecord>
{
    public void Configure(EntityTypeBuilder<CommentRecord> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id)
                .IsUnique();
        builder.Property(c => c.Id)
                .HasConversion(v => v.Value, v => new CommentRecordId(v))
                .ValueGeneratedOnAdd()
                .IsRequired();

        builder.Property(c => c.Message).IsRequired().HasMaxLength(1000);
        builder.Property(c => c.CreatedAt)
                .HasConversion(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();
        builder.Property(c => c.ModifiedAt)
                .HasConversion(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();
        builder.Property(c => c.AuthorId)
                .HasConversion(v => v.Value, v => new UserRecordId(v))
                .IsRequired();

        builder.HasOne(c => c.Incident)
            .WithMany(i => i.Comments)
            .HasForeignKey(c => c.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
public class UserRecordTypeConfiguration : IEntityTypeConfiguration<UserRecord>
{
    public void Configure(EntityTypeBuilder<UserRecord> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Id).IsUnique();
        builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(u => u.Id).HasConversion(v => v.Value, v => new UserRecordId(v));

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Surname).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Status).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.CreatedAt).HasConversion(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(u => u.ModifiedAt).IsRequired();
        builder.Property(u => u.ModifiedAt).HasConversion(v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}