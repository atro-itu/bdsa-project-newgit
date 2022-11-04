namespace NEWgIT.Infrastructure;

public class GitContext : DbContext
{
    public GitContext(DbContextOptions<GitContext> options) : base(options) { }

    public DbSet<Analysis> Analysis => Set<Analysis>();
    public DbSet<Commit> Commits => Set<Commit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analysis>()
                    .HasIndex(i => i.RepoName)
                    .IsUnique();

    }
}
