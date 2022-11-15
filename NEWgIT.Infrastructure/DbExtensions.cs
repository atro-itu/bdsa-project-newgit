namespace NEWgIT.Infrastructure;

public static class DbExtensions
{
    public static Analysis? FindByIdentifier(this DbSet<Analysis> set, string identifier) =>
        set.FirstOrDefault(a => a.RepoIdentifier == identifier);
}
