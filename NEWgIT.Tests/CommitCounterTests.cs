namespace NEWgIT.Tests;

public class CommitCounterTests : IDisposable
{
    Repository _repository;
    string _path;

    public CommitCounterTests()
    {
        _path = "./repo";

        Repository.Init(_path);
        _repository = new Repository(_path);

        _repository.Seed();
    }

    [Fact]
    public void FrequencyMode_Should_Return_Sum_Of_Commits_Per_Day()
    {
        _repository.Commits.Count().Should().Be(10);
    }

    [Fact]
    public void AuthorMode_Should_Return_Sum_Of_Commits_Per_Author()
    {
        _repository.Commits.Count().Should().Be(10);
    }

    public void Dispose()
    {
        _repository.Dispose();
        Directory.Delete(_path, recursive: true);
    }
}
