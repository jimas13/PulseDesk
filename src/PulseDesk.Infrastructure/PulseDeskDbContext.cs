namespace PulseDesk.Infrastructure;

public class PulseDeskDbContext : DbContext
{
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Comment> Comments => Set<Comment>();

    public PulseDeskDbContext(DbContextOptions<PulseDeskDbContext> options)
        : base(options) { }
}
