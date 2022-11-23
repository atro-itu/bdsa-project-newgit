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
                    new CommitInfo(author: commit.Author.Name,
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
        var commitDTOsHash = new HashSet<CommitCreateDTO>() { new CommitCreateDTO("issa me", new DateTime(1950, 03, 03), "1234567890") };
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
        var expectedCommits = new HashSet<CommitDTO>(){
            new CommitDTO ("duckth", new DateTime(1950,01,10).ToUniversalTime(), "1234567890")
        };
        var expectedAnalysis = new AnalysisDTO(1, "duckth/testing", expectedCommits, "1234567890");

        // Act
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));
        var actualAnalysis = _analysisRepository.Find(1);

        // Assert
        actualAnalysis.Should().BeEquivalentTo(expectedAnalysis);
    }

    [Fact]
    public void Find_Given_None_Existing_Id_Should_Return_Null()
    {
        // Arrange
        var expectedAnalysis = (AnalysisDTO?)null;

        // Act
        var actualAnalysis = _analysisRepository.Find(1);

        // Assert
        actualAnalysis.Should().BeEquivalentTo(expectedAnalysis);
    }

    [Fact]
    public void FindByIdentifier_Should_Return_AnalysisDTO_Given_Found_Analysis()
    {
        // Arrange
        var expectedCommits = new HashSet<CommitDTO>(){
            new CommitDTO ("duckth", new DateTime(1950,01,10).ToUniversalTime(), "1234567890")
        };
        var expectedAnalysis = new AnalysisDTO(1, "duckth/testing", expectedCommits, "1234567890");

        // Act
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));
        var actualAnalysis = _analysisRepository.FindByIdentifier("duckth/testing");

        // Assert
        actualAnalysis.Should().BeEquivalentTo(expectedAnalysis);
    }

    [Fact]
    public void FindByIdentifier_Should_Return_Null_Given_None_Existing_Analysis()
    {
        // Arrange
        var expectedAnalysis = (AnalysisDTO?)null;

        // Act
        var actualAnalysis = _analysisRepository.FindByIdentifier("duckth/testing");

        // Assert
        actualAnalysis.Should().BeEquivalentTo(expectedAnalysis);
    }

    [Fact]
    public void Update_Should_Return_Ok_Given_Existing_And_Already_Updated_Repo()
    {
        // Arrange
        var commits = new HashSet<CommitDTO>(){
            new CommitDTO ("duckth", new DateTime(1950,01,10).ToUniversalTime(), "1234567890")
        };

        // Act
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));
        var actualResponse = _analysisRepository.Update(new AnalysisUpdateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));

        // Assert
        actualResponse.Should().Be(Response.Ok);
    }

    [Fact]
    public void Update_Should_Return_Updated_Given_Existing_And_Outdated_Repo()
    {
        var commits = new HashSet<CommitDTO>(){
            new CommitDTO ("duckth", new DateTime(1950,01,10).ToUniversalTime(), "1234567890")
        };

        // Act
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));
        var actualResponse = _analysisRepository.Update(new AnalysisUpdateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "0987654321")
            },
            latestCommitHash: "0987654321"
        ));

        // Assert
        actualResponse.Should().Be(Response.Updated);
    }

    [Fact]
    public void Update_Should_Return_NotFound_Given_None_Existing_Repo()
    {
        // Arrange
        // Act
        var actualResponse = _analysisRepository.Update(new AnalysisUpdateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));

        // Assert
        actualResponse.Should().Be(Response.NotFound);
    }

    [Fact]
    public void Delete_Should_Return_NotFound_Given_None_Existing_Repo()
    {
        // Arrange
        var analysisDeleteDTO = new AnalysisDeleteDTO("duckth/testing");

        // Act
        var actualResponse = _analysisRepository.Delete(analysisDeleteDTO);

        // Assert
        actualResponse.Should().Be(Response.NotFound);
    }

    [Fact]
    public void Delete_Should_Return_Deleted_Given_Existing_Repo()
    {
        // Arrange
        var analysisDeleteDTO = new AnalysisDeleteDTO("duckth/testing");
        _analysisRepository.Create(new AnalysisCreateDTO(
            repoIdentifier: "duckth/testing",
            commits: new HashSet<CommitCreateDTO>(){
                new CommitCreateDTO("duckth", new DateTime(1950,01,10), "1234567890")
            },
            latestCommitHash: "1234567890"
        ));

        // Act
        var actualResponse = _analysisRepository.Delete(analysisDeleteDTO);

        // Assert
        actualResponse.Should().Be(Response.Deleted);
    }

    public void Dispose()
    {
        _context.Dispose();
        _gitRepository.Dispose();
        Directory.Delete(_path, true);
        GC.SuppressFinalize(this);
    }
}
