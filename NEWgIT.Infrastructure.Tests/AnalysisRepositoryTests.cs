namespace NEWgIT.Infrastructure.Tests;

public class AnalysisRepositoryTests : IDisposable
{
    private readonly GitContext _context;
    private readonly AnalysisRepository _repository;

    private readonly string _path;

    public AnalysisRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitContext>();
        builder.UseSqlite(connection);
        var context = new GitContext(builder.Options);
        context.Database.EnsureCreated();
        _context = context;
        _repository = new AnalysisRepository(_context);

        _path = "./repo";

        Repository.Init(_path);
        new Repository(_path).Seed();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    // [Fact]
    // public void Test_create()
    // {
    //     _repository.Create(new AnalysisCreateDTO { repoIdentifier: _path})
    // }
}
