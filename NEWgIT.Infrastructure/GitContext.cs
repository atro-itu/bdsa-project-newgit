namespace NEWgIT.Infrastructure;

public class GitContext : DbContext
{
    public GitContext(DbContextOptions<GitContext> options) : base(options) { }

    public DbSet<Analysis> Analysis => Set<Analysis>();
    public DbSet<CommitInfo> Commits => Set<CommitInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analysis>()
                    .HasIndex(i => i.RepoIdentifier)
                    .IsUnique();

        modelBuilder.Entity<CommitInfo>()
                    .HasIndex(i => i.Id)
                    .IsUnique();

    }



}
