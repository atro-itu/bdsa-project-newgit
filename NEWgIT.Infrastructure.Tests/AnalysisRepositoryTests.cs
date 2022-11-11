namespace NEWgIT.Infrastructure.Tests;

public class AnalysisRepositoryTests : IDisposable
{
    private readonly GitContext _context;
    private readonly AnalysisRepository _analysisRepository;

    private readonly Repository _gitRepository;

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
        _analysisRepository = new AnalysisRepository(_context);

        _path = "./repo";

        Repository.Init(_path);
        _gitRepository = new Repository(_path).Seed();
    }

    public void Dispose()
    {
        _context.Dispose();
        _gitRepository.Dispose();
        Directory.Delete(_path, true);
    }


    [Fact]
    public void Create_Given_AnalysisCreateDTO_Creates_Analysis()
    {
        // Arrange
        var (commitDTOs, hash) = CommitFetcherService.Instance.GetRepoCommits(_gitRepository);
        ICollection<CommitInfo> expectedCommits = _gitRepository.Commits.Select(commit => new CommitInfo(author: commit.Author.Name, date: commit.Committer.When.DateTime, hash: commit.Sha)).ToHashSet();

        var expectedIdentifier = _path;
        var expectedHash = hash;

        // Act
        var (response, id) = _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: _path,
            commits: commitDTOs,
            latestCommitHash: hash
        ));

        // Assert
        response.Should().Be(Response.Created);
        var analysis = _context.Analysis.Find(id)!;
        analysis.RepoIdentifier.Should().Be(expectedIdentifier);
        analysis.LatestCommitHash.Should().Be(expectedHash);
        analysis.Commits.Count.Should().Be(expectedCommits.Count);
    }


    // [Fact]
    // public void Update_Given_New_Commits()
    // {
    //     // Arrange
    //     AnalysisRepository repo = new AnalysisRepository();


    //     var commitInfo = _gitRepository.Update(new AnalysisDTO())

    //     // Act


    //     // Assert
    // }
}
