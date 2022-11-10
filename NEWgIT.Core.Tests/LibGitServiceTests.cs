namespace NEWgIT.Core.Tests;

public class LibGitServiceTests
{
    CommitFetcherService _service;

    public LibGitServiceTests()
    {
        _service = CommitFetcherService.Instance;
    }

    [Fact]
    public void GetRepoCommits_Given_Correct_Source_Url_Returns_Something()
    {
        // Arrange
        var gitUrl = "https://github.com/duckth/bdsa-project-newgit";

        // Act
        var (commits, hash) = _service.GetRepoCommits(gitUrl);

        // Assert
        commits.Should().NotBeNullOrEmpty();
        hash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetRepoCommits_Cleans_Up_Cloned_Repository()
    {
        // Arrange
        var gitUrl = "https://github.com/duckth/bdsa-project-newgit";

        //Act
        var (commits, hash) = _service.GetRepoCommits(gitUrl);

        //Assert
        Directory.Exists("./temp").Should().BeFalse();
    }
}
