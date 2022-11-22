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


    [Fact]
    public void Create_Given_AnalysisCreateDTO_Creates_Analysis()
    {
        // Arrange
        var (commitDTOs, hash) = CommitFetcherService.Instance.GetRepoCommits(_gitRepository);
        ICollection<CommitInfo> expectedCommits = _gitRepository.Commits
                .Select(commit => 
                    new CommitInfo( author: commit.Author.Name, 
                                    date: commit.Committer.When.DateTime, 
                                    hash: commit.Sha)
                ).ToHashSet();

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

    [Fact]
    public void Create_Given_Already_Existing_Repo_Should_Return_Conflict()
    {
        // Arrange
        var commitDTOsHash = new HashSet<CommitCreateDTO>(){new CommitCreateDTO("issa me", new DateTime(1950,03,03), "1234567890")};
        var (commitDTOs, hash) = (commitDTOsHash, "0987654321");
        var expected = (Response.Conflict, 1);

        // Act
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "huhu",
            commits: commitDTOs,
            latestCommitHash: hash
        ));
        var (response, id) = _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "huhu",
            commits: commitDTOs,
            latestCommitHash: hash
        ));

        // Assert
        response.Should().Be(expected.Item1);
        id.Should().Be(expected.Item2);
    }

    [Fact]
    public void Read_Given_No_Analyses_Should_Return_Empty_List()
    {
        // Arrange
        var expected = new List<AnalysisDTO>();

        // Act
        var actual = _analysisRepository.Read();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Read_Given_Analyses_Should_Return_List_Of_AnalysisDTOs()
    {
        // Arrange
        var hash = "1234567890";
        var actualCommitHashSet = new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO(
                    "issa me", 
                    new DateTime(1950,01,10), 
                    hash
                )}; // Part of actual

        var expectedCommitList = new List<CommitDTO>(){
                new CommitDTO(
                    1, 
                    "issa me", 
                    new DateTime(1950,01,10).ToUniversalTime(), 
                    hash
                )}; // Part of expected

        var expected = new List<AnalysisDTO>(){
            new AnalysisDTO(1, "repo", expectedCommitList, hash)
        };

        // Act
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "repo",
            commits: actualCommitHashSet,
            latestCommitHash: hash
        ));
        var actual = _analysisRepository.Read();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Find_Given_Existing_Id_Should_Return_AnalysisDTO()
    {
        // Arrange
        
    }

    public void Dispose()
    {
        _context.Dispose();
        _gitRepository.Dispose();
        Directory.Delete(_path, true);
        GC.SuppressFinalize(this);
    }
}
