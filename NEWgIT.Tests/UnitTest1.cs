using LibGit2Sharp;

namespace NEWgIT.Tests;

[Collection("Sequential")]
public class UnitTest1 : IDisposable
{
    Repository _repo;

    public UnitTest1()
    {
        Repository.Init("repo3");

        _repo = new Repository("repo3");
    }

    public void Dispose()
    {
        _repo.Dispose();
        _repo.ForceDelete();
    }

    [Fact]
    public void Test1()
    {

        _repo.Commit("test", new Signature("test", "test", DateTimeOffset.Now), new Signature("test", "test", DateTimeOffset.Now));

        _repo.Commits.Count().Should().Be(2);
        // true.Should().Be(true);
    }

    // new test
    // they???
}
