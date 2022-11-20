using Octokit;
using Microsoft.Extensions.Configuration;

namespace NEWgIT.Core.Services;

public class ForkFetcherService : IForkFetcherService
{
    private static ForkFetcherService instance = null!;
    private readonly GitHubClient _client;

    private ForkFetcherService()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<ForkFetcherService>().Build();
        _client = new GitHubClient(new ProductHeaderValue("NEWgIT")) { Credentials = new Octokit.Credentials(config["PAT_NEWGIT"], AuthenticationType.Bearer) };
    }

    public static ForkFetcherService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ForkFetcherService();
            }
            return instance;
        }
    }
    public async Task<ICollection<string>> FetchForks(string repoIdentifier)
    {
        var repoParts = repoIdentifier.Split('/');
        var repoOwner = repoParts[0];
        var repoName = repoParts[1];
        return await FetchForks(repoOwner, repoName);

    }

    public async Task<ICollection<string>> FetchForks(string repoOwner, string repoName)
    {
        var forkRepositories = await _client.Repository.Forks.GetAll(repoOwner, repoName);
        return forkRepositories.Select(repo => repo.FullName).ToHashSet();
    }
}
