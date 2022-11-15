namespace NEWgIT;

public class GitContextFactory : IDesignTimeDbContextFactory<GitContext>
{
    public GitContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        var connectionString = configuration.GetConnectionString("Newgit");

        var optionsBuilder = new DbContextOptionsBuilder<GitContext>();
        optionsBuilder.UseNpgsql(connectionString!);

        return new GitContext(optionsBuilder.Options);
    }
}
