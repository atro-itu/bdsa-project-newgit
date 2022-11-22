namespace NEWgIT.Core.Tests;

public class ForkFetcherServiceTests
{
    [Fact]
    public async void ForkFetcherService_given_repository_owner_and_name_returns_name_of_forks()
    {
        var forks = await ForkFetcherService.Instance.FetchForks("itu-bdsa", "project-description");
        forks.Should().NotBeEmpty();
        forks.Should().Contain("lucasfth/project-description");
    }

    [Fact]
    public async void ForkFetcherService_given_repository_identifier_returns_name_of_forks()
    {
        var forks = await ForkFetcherService.Instance.FetchForks("itu-bdsa/project-description");
        forks.Should().NotBeEmpty();
    }
}
