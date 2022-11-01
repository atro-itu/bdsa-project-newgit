using LibGit2Sharp;

namespace NEWgIT.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Repository.Init("repo");
        Repository repo = new Repository("repo");
        repo.Commits.Count().Should().Be(0);
    }
}
